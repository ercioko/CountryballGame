using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public Rigidbody2D rb2d;
	public Animator anim;
	public EnemyHP hp;
	public Transform followedPlatform;
	float horizontalSpeed=150;
	void Update () {
		Global.OptimizeObjectRender(this.gameObject);

		if(hp.healthpoints<=0)
			Destroy(this.gameObject);

		if(followedPlatform!=null) {
			if(followedPlatform.position.x>transform.position.x+0.1f)
				rb2d.AddForce(new Vector2(horizontalSpeed,0));
			else if(followedPlatform.position.x<transform.position.x-0.1f)
				rb2d.AddForce(new Vector2(-horizontalSpeed,0));
		}
	}
	void OnCollisionEnter2D(Collision2D other) {
		bool isPlatform = other.gameObject.name == "Platform(Clone)";
		Vector2 jump = new Vector2(0,1);

		if(other.gameObject.transform.position.y < transform.position.y && isPlatform){
			rb2d.AddForce(jump*Random.Range(300,500), ForceMode2D.Impulse);
		}
	}
	void SetAnimatorState(){
		if(anim.enabled){
			if(Random.Range(0,2) == 0)
				anim.SetTrigger("JumpLeft");
			else
				anim.SetTrigger("JumpRight");
		}
	}
}
