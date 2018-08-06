using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObject : MonoBehaviour {
	private void Update() {
		Global.OptimizeObjectRender(this.gameObject);	
	}
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.name=="Platform(Clone)" || other.name=="GothicWindow(Clone)")
			Destroy(this.gameObject);
	}
}
