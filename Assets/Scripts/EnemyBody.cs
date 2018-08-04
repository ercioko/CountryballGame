using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour {

	public EnemyHP hp;
	public Rigidbody2D rb2d;
	
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.name=="Weapon") {
			hp.Damage(Weapon.damage);
			transform.parent.GetComponent<SpriteRenderer>().color *= new Color(1f,0.5f,0.5f,1f);
			ApplyHitForce();
		}
	}
	void ApplyHitForce(){
		if(Ball.pos.x > transform.position.x)
			rb2d.AddForce(new Vector2(1,0) * -Weapon.force, ForceMode2D.Impulse);
		else if(Ball.pos.x < transform.position.x)
			rb2d.AddForce(new Vector2(1,0) * Weapon.force, ForceMode2D.Impulse);
	}
}
