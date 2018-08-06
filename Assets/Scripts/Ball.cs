using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Ball : MonoBehaviour {

	public static float fallHeight = 10.5f, topHeight = 7.65f, horizontalSpeed;
	public static Vector3 pos;
	public static Rigidbody2D rb2d;
	public static bool onAccelerometer;
	public static AudioSource[] aSources;
	public GameObject leftArrow, rightArrow;
	public static GameObject ball;
	public Menu menu;
	public HP hp;
	bool leftArrowPressed, rightArrowPressed, platformTouched;
	Animator animator;
	void Awake(){
		ball = this.gameObject;
	}
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		aSources = GetComponents<AudioSource>();
		Reset();
	}
	void FixedUpdate () {
		GetArrowInput();
		GetAccelerometerInput();
		CheckHeight();
		rb2d.velocity = new Vector3(horizontalSpeed, rb2d.velocity.y,0);
	}
	void Update() {
		pos = transform.position;
	}
	public void OnCollisionEnter2D(Collision2D other) {
		const float ballRadius = 0.3f;
		bool isPlatform = other.gameObject.name == "Platform(Clone)";
		int jumpStrength = 430;
		if(other.transform.position.y + ballRadius < pos.y && isPlatform){	
			SetAnimatorTrigger();
			DetectPlatform(other);
			rb2d.AddForce(Vector2.up*jumpStrength, ForceMode2D.Impulse);
			aSources[0].Play();
		}	
	}
	void GetArrowInput(){
		if(!onAccelerometer){
			if(Input.GetKey(KeyCode.RightArrow) || rightArrowPressed)
			{
				if(horizontalSpeed<0)
					horizontalSpeed+=1f;
				if(horizontalSpeed<9)
					horizontalSpeed+=0.5f;
			}
			if(Input.GetKey(KeyCode.LeftArrow) || leftArrowPressed)
			{
				if(horizontalSpeed>0)
					horizontalSpeed-=1f;
				if(horizontalSpeed>-9)
					horizontalSpeed-=0.5f;
			}
		}
	}
	void GetAccelerometerInput(){
		if(onAccelerometer)
		{
			horizontalSpeed = Input.acceleration.x * 30;
		}
	}
	void CheckHeight(){
		if(transform.position.y<topHeight-fallHeight){
			menu.ExitToMenu();
			//if(Random.Range(0,5)==0)
				//Advertisement.Show();
		}else if(transform.position.y>topHeight)
			topHeight=transform.position.y;
	}
	void SetAnimatorTrigger(){
		if(animator.enabled){
				if(horizontalSpeed<-2f)
					animator.SetTrigger("JumpLeft");
				else if(horizontalSpeed>2f)
					animator.SetTrigger("JumpRight");
				else
					animator.SetTrigger("Jump");
		}
	}
	public static void DetectPlatform(Collision2D other){
		if(other.gameObject.CompareTag(Global.platformFragileTag)||other.gameObject.CompareTag(Global.platformFragileMovingTag))
				Destroy(other.gameObject);
		if(other.gameObject.CompareTag(Global.platformSpringTag))
			other.gameObject.transform.Find("Spring(Clone)").GetComponent<Spring>().UseSpring();
	}
	public void ChangeInput(){
		onAccelerometer=!onAccelerometer;
		if(onAccelerometer)
		{
			GameObject.Find("TiltText").GetComponent<Text>().text = "Tilt";
			leftArrow.SetActive(false);
			rightArrow.SetActive(false);
		}else{
			GameObject.Find("TiltText").GetComponent<Text>().text = "Arrows";
			leftArrow.SetActive(true);
			rightArrow.SetActive(true);
		}
	}
	public void Reset(){
		transform.position = new Vector3(0,0.65f,-1);
		transform.rotation = Quaternion.identity;
		transform.Find("Weapon").localPosition = new Vector3(1.8f, 0.3f, -1f);
		transform.Find("Weapon").localRotation = Quaternion.Euler(0, 0, -30);
		topHeight = 7.65f;
		horizontalSpeed = 0;
		pos.y = 0.65f;	
		hp.Reset();	
	}

	public void LeftArrowDown(){
		leftArrowPressed = true;
	}
	public void RightArrowDown(){
		rightArrowPressed = true;
	}
	public void LeftArrowUp(){
		leftArrowPressed = false;
	}
	public void RightArrowUp(){
		rightArrowPressed = false;
	}
}
