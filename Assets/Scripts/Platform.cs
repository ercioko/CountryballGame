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

		if(tag=="platform"&& Global.ChanceInPercent(10))
			CreateCoins(1);
		else if(tag=="platformMoving"&& Global.ChanceInPercent(30))
			CreateCoins(1);
		else if(tag=="platformFragile"&& Global.ChanceInPercent(40))
			CreateCoins(2);
		else if(tag=="platformFragileMoving"&& Global.ChanceInPercent(50))
			CreateCoins(3);
		else if(tag=="platformSpring")
			AddSpring();
		else if(tag=="platformEnemy")
			AddEnemy();
	}
	void Update () {
		Global.OptimizeObjectRender(this.gameObject);
		TransformMovingPlatform();
		if(enemy !=null)
			enemy.GetComponent<Enemy>().followedPlatform = this.transform;
	}
	void TransformMovingPlatform(){
		if(tag == "platformMoving" ||tag == "platformFragile"){
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
	void AddEnemy(){
		enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy"), new Vector2(transform.position.x, transform.position.y+0.5f), transform.rotation, GameObject.Find("Enemies").transform);
	}
	void CreateCoins(int quantity){
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
	void AddSpring(){
		InstantiateObj(spring, 0, 0.2f);
	}
	void InstantiateObj(GameObject obj, float offsetX, float offsetY){
		clone = Instantiate(obj, new Vector2(transform.position.x+offsetX,transform.position.y+offsetY), transform.rotation, transform);
		clone.transform.localScale = new Vector2(clone.transform.localScale.x/transform.localScale.x, clone.transform.localScale.y/transform.localScale.y);
	}
}
