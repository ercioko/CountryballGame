using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour {
	GameObject clone;
	GameObject[] normal, move, destroy, destroyMove, spring;
	static bool prize, start;
	static float platformLastHeight, windowLastHeight, torchLastHeight, basicDistance = 15;
	static bool[] reachedPlatformH = new bool[4], reachedWindowH = new bool[4], reachedTorchH = new bool[4];

	void Start(){
		Reset();
	}
	public static void  Reset() {
		DestroyObj("platform");
		DestroyObj("platformMoving");
		DestroyObj("platformFragileMoving");
		DestroyObj("platformFragile");
		DestroyObj("platformCoin");
		DestroyObj("platformSpring");
		DestroyObj("platformEnemy");
		reachedPlatformH = reachedWindowH = reachedTorchH = new bool[4];
		platformLastHeight=windowLastHeight=torchLastHeight=0;
		start=true;
	}
	static void DestroyObj(string name){
		GameObject[] obj = GameObject.FindGameObjectsWithTag(name);
		foreach(GameObject t in obj)
			Destroy(t);
	}
	void Update () {
		if(start){
			CreatePlatform(10, 0);
			CreatePlatform(40, 1);
			CreatePlatform(50, 2);
			CreatePlatform(50, 3);	
			CreatePrize();

			CreateWindows(50, 0);

			CreateTorches(50, 0);
			CreateTorches(50, 1);
		}
	}
	void CreatePrize(){
		if(Ball.pos.y > platformLastHeight-basicDistance && !prize){
			clone = Instantiate(Resources.Load<GameObject>("Prefabs/Platform"), new Vector2(0, platformLastHeight+5), transform.rotation);
			clone.transform.localScale = new Vector2(4f,1f);
			for(int i=0;i<50;i++){
				Instantiate(Resources.Load<GameObject>("Prefabs/Coin"), new Vector2(Random.Range(-2f,2f),platformLastHeight+6), transform.rotation);
			}
			prize=true;
		}
	}
	void CreatePlatform(int quantity, int lvl){
		if(Ball.pos.y > platformLastHeight-basicDistance && !reachedPlatformH[lvl]){
			for(int i=0; i < quantity; i++){
				CreateObject("Platform", ref platformLastHeight,0,2,5,0);
				if(lvl==1)
					clone.transform.localScale = new Vector2(0.8f,1f);
				if(lvl>1)	
					clone.transform.localScale = new Vector2(0.5f,1f);
				PickRandomTag(lvl);
		}
		reachedPlatformH[lvl]=true;
		}
	}
	void PickRandomTag(int lvl){ 
		if(Global.chanceInPercent(60) && lvl>=1){
			clone.tag = "platformMoving";
			clone.GetComponent<SpriteRenderer>().color = new Color(0,128,192,255);
		}
		else if(Global.chanceInPercent(40) && lvl>=2){
			clone.tag = "platformFragile";
			clone.GetComponent<SpriteRenderer>().color = new Color(128,0,0,255);
		}
		else if(Global.chanceInPercent(30) && lvl>=3){
			clone.tag = "platformFragileMoving";
			clone.GetComponent<SpriteRenderer>().color = new Color(0,0,0,1);
		}
		else if(Global.chanceInPercent(20) && lvl>=0){
			clone.tag = "platformSpring";
		}
		else if(Global.chanceInPercent(20*lvl) && lvl>=1 ) {
			clone.tag="platformEnemy";
		}
	}
	void CreateWindows(int quantity, int sequence_num){
		if(Ball.pos.y > windowLastHeight-basicDistance && !reachedWindowH[sequence_num]){
			for(int i=0;i<quantity;i++){
				CreateObject("GothicWindow", ref windowLastHeight, 4.1f, 50, 80, 1);
			}
			reachedWindowH[sequence_num] = true;
		}
	}
	void CreateTorches(int quantity, int sequence_num){
		if(Ball.pos.y > torchLastHeight-basicDistance && reachedTorchH[sequence_num]){
			for(int i=0;i<quantity;i++){
				CreateObject("Torch", ref torchLastHeight, 4, 8, 16, 1);
			}
			reachedTorchH[sequence_num] = false;
		}
	}
	void CreateObject(string prefabName, ref float lastHeight, float maxXPos, float minHChange, float maxHChange, int z){
		GameObject obj = Resources.Load<GameObject>("Prefabs/"+prefabName);
		float heightChange = Random.Range(minHChange, maxHChange);
		clone  = Instantiate(obj, nextPos(ref lastHeight, maxXPos, heightChange, z), transform.rotation, transform);
		lastHeight += heightChange;
	}
	Vector3 nextPos(ref float lastHeight, float maxXPos, float heightChange, int z){
		Vector3 newPos = new Vector3(Random.Range(-maxXPos, maxXPos), lastHeight + heightChange, z);
		return newPos;
	}
}
