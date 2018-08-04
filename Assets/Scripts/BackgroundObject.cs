using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObject : MonoBehaviour {
	private void Update() {
		Global.OptimizeObjectRender(this.gameObject);	
	}
}
