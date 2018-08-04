using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class EnemyHP : MonoBehaviour {

	public int healthpoints, maxHealth=40;
	AudioSource[] audioSrc;
	public int hearts = 2;
	private void Start() {
		Reset();
		audioSrc = GetComponents<AudioSource>();
	}
	public void Damage(int value){
		healthpoints-=value;
		audioSrc[Random.Range(0,5) ].Play();
	}
	public void Heal(int value) {
		healthpoints+=value;
	}
	public void Reset() {
		healthpoints = maxHealth;
	}
	private void Update() {
		SetHearts(hearts);
	}
	void SetHearts(int hearts) {	
		for(int i=0; i<hearts; i++) {
			if(healthpoints == maxHealth - i*20 - 10) {
				transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("HeartHalf");
			}else if(healthpoints == maxHealth - i*20 - 20) {
				transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("HeartEmpty");
			}else if(healthpoints > maxHealth - i*20 - 10) {
				transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("HeartFull");
			}
		}
	}
}