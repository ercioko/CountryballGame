using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	float speed=5;
	GameObject parent, coin, spring, clone, enemy;
	bool movingRight;
	void Start () {
		coin = Resources.Load<GameObject>("Prefabs/Coin");
		spring = Resources.Load<GameObject>("Prefabs/Spring");
		parent = GameObject.Find("Gameplay");
		Vector2 temp = transform.position;

		transform.SetParent(parent.transform);
		temp.x = Random.Range(-4.5f,4.5f);
		transform.position = temp;	

		if(tag==Global.platformTag&& Global.ChanceInPercent(10))
			CreateCoins(1);
		else if(tag==Global.platformMovingTag&& Global.ChanceInPercent(30))
			CreateCoins(1);
		else if(tag==Global.platformFragileTag&& Global.ChanceInPercent(40))
			CreateCoins(2);
		else if(tag==Global.platformFragileMovingTag&& Global.ChanceInPercent(50))
			CreateCoins(3);
	}
	void Update () {
		Global.OptimizeObjectRender(this.gameObject);
		TransformMovingPlatform();
		if(enemy !=null)
			enemy.GetComponent<Enemy>().followedPlatform = this.transform;
	}
	void TransformMovingPlatform(){
		if(tag == Global.platformMovingTag ||tag == Global.platformFragileMovingTag){
			if(transform.position.x>5&&!movingRight){
				speed = -speed;
				movingRight=true;
				
			}else if(transform.position.x<-5&&movingRight){
				speed = -speed;
				movingRight=false;
			}
		transform.Translate(speed*Time.deltaTime, 0, 0);	
		}
		if(transform.position.y > 600)
			speed=7;
	}
	public void AddEnemy(){
		enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy"), new Vector2(transform.position.x, transform.position.y+0.5f), transform.rotation, GameObject.Find("Enemies").transform);
	}
	public void CreateCoins(int quantity){
		for(int i=quantity; i>1; i-=2){
			AddCoin(-quantity/2);
			AddCoin(quantity/2);
		}
		if(quantity!=0)
			AddCoin(0);
	}
	void AddCoin(int offsetX){
		InstantiateObj(coin, offsetX, 0.7f);
	}
	public void AddSpring(){
		spring = Resources.Load<GameObject>("Prefabs/Spring");
		InstantiateObj(spring, 0, 0.325f, -1);
	}
	void InstantiateObj(GameObject obj, float offsetX, float offsetY, int z=0){
		clone = Instantiate(obj, new Vector3(transform.position.x+offsetX,transform.position.y+offsetY, z), transform.rotation, transform);
		clone.transform.localScale = new Vector3(clone.transform.localScale.x/transform.localScale.x, clone.transform.localScale.y/transform.localScale.y, z);
	}
}
