using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	public static GameObject weapon;
	Animator sword;
	AudioSource[] audiosrc;
	float startTime;
	public static int force=200, damage=10;
	private void Awake() {
		weapon = this.gameObject;
	}
	private void Start() {
		sword = GetComponent<Animator>();
		audiosrc=GetComponents<AudioSource>();
	}
	public void Attack(){
		if(Ball.horizontalSpeed<0) {
			sword.SetTrigger("attackLeft");
			if(startTime<Time.time-0.1f){
				audiosrc[Random.Range(0,3)].Play();
				startTime=Time.time;
			}
		}else if(Ball.horizontalSpeed>=0) {
			sword.SetTrigger("attackRight");
			if(startTime<Time.time-0.1f){
				audiosrc[Random.Range(0,3)].Play();
				startTime=Time.time;
			}
		}	
	}
}
