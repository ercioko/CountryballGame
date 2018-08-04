using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {

	bool used;
	static float boost = 400;
	public Animator animator;
	void Update(){
		Global.OptimizeObjectRender(this.gameObject);
	}
	public void UseSpring(){
		if(!used){
			Ball.rb2d.AddForce(new Vector2(0,boost), ForceMode2D.Impulse);
			animator.SetTrigger("springTrigger");
			used=true;
		}
	}
}
