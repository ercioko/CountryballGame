using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class HP : MonoBehaviour {
	public GameObject healthObj;
	public Menu menu;
	public static int healthpoints;
	float startTime;
	bool colorLerp;
	AudioSource[] audioSrc;
	public Image overlay;
	private void Start() {
		Reset();
		PlayerData.health = 100;
		healthpoints = PlayerData.health;
		audioSrc = GetComponents<AudioSource>();
	}
	private void Update() {
		SetHearts(5);
		if(healthpoints<10) {
			menu.ExitToMenu();
			//if(Random.Range(0,5)==0)
				//Advertisement.Show();
		}
		if(colorLerp)
			ColorLerp();
	}
	void SetHearts(int hearts) {	
		for(int i=0; i<hearts; i++) {
			if(healthpoints == PlayerData.health - i*20 - 10)
				LoadHeart("HeartHalf", i);
			else if(healthpoints == PlayerData.health - i*20 - 20)
				LoadHeart("HeartEmpty", i);
			else if(healthpoints > PlayerData.health - i*20 - 10)
				LoadHeart("HeartFull", i);
		}
	}
	void LoadHeart(string name, int number){
		healthObj.transform.GetChild(number).GetComponent<Image>().sprite = Resources.Load<Sprite>(name);
	}
	public void Damage(int value){
		startTime = Time.time;
		healthpoints-=value;
		colorLerp = true;
		audioSrc[Random.Range(0,5) ].Play();
	}
	public void Heal(int value) {
		healthpoints+=value;
	}
	public void Reset() {
		healthpoints = PlayerData.health;
	}
	void ColorLerp(){
		if(Time.time < startTime+0.1f) {
			overlay.color = new Color(1f,0f,0f,0.1f);
		}			
		else if(Time.time < startTime+1) {
			overlay.color = Color.Lerp(new Color(1f,0f,0f,0.1f),new Color(1f,1f,1f,0f), Time.time - startTime);
		}else 
			colorLerp=false;
	}
}	