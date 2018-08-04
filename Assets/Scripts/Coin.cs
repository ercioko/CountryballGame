using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	public PlayerData playerData; 
	void Update () {
		Global.OptimizeObjectRender(this.gameObject);
	}
	private void OnTriggerEnter2D(Collider2D other){
		CollectBy(other);
	}
	void CollectBy(Collider2D other){
		if(other.gameObject.name=="Player"){
			PlayerData.money += 1;
			Ball.aSources[1].Play();
			Destroy(this.gameObject);
		}
	}
}
