using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour {
	GameObject platform, window, torch;
	GameObject[] normal, move, destroy, destroyMove, spring;
	static bool prize, start;
	static float platformLastHeight, windowLastHeight, torchLastHeight, basicDistance = 15;
	static bool[] reachedPlatformH = new bool[4], reachedWindowH = new bool[4], reachedTorchH = new bool[4];

	void Start(){
		Reset();
	}
	public static void  Reset() {
		DestroyObj(Global.platformTag);
		DestroyObj(Global.platformMovingTag);
		DestroyObj(Global.platformFragileMovingTag);
		DestroyObj(Global.platformFragileTag);
		DestroyObj(Global.platformCoinTag);
		DestroyObj(Global.platformSpringTag);
		DestroyObj(Global.platformEnemyTag);
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
	
	#region Platform Creation
	void CreatePlatform(int quantity, int lvl){
		if(Ball.pos.y > platformLastHeight-basicDistance && !reachedPlatformH[lvl]){
			for(int i=0; i < quantity; i++){
				CreateObject(ref platform, "Platform");
				UpdateHeight(ref platformLastHeight, 2, 5);
				platform.transform.position = NextPos(4,platformLastHeight);

				if(lvl==1)
					platform.transform.localScale = new Vector2(0.8f,1f);
				if(lvl>1)	
					platform.transform.localScale = new Vector2(0.5f,1f);
				
				PickRandomTag(lvl);
		}
		reachedPlatformH[lvl]=true;
		}
	}
	void PickRandomTag(int lvl){ 
		if(Global.chanceInPercent(60) && lvl>=1)
			SetTag(platform, Global.platformMovingTag, new Color(0,128,192,255));
		else if(Global.chanceInPercent(40) && lvl>=2)
			SetTag(platform, Global.platformFragileTag, new Color(128,0,0,255));
		else if(Global.chanceInPercent(30) && lvl>=3)
			SetTag(platform, Global.platformFragileMovingTag, Color.black);
		else if(Global.chanceInPercent(20) && lvl>=0)
			SetTag(platform, Global.platformSpringTag, Color.white);
		else if(Global.chanceInPercent(20*lvl) && lvl>=1 ) 
			SetTag(platform, Global.platformEnemyTag, Color.white);
	}
	void SetTag(GameObject obj, string tag, Color color){
		obj.tag = tag;
		obj.GetComponent<SpriteRenderer>().color = color;
	}
	void CreatePrize(){
		if(Ball.pos.y > platformLastHeight-basicDistance && !prize){
			CreateObject(ref platform, "Platform");
			UpdateHeight(ref platformLastHeight, 2, 5);
			platform.transform.position = NextPos(0,platformLastHeight,0);
			platform.transform.localScale = new Vector2(4f,1f);
			for(int i=0;i<50;i++){
				Instantiate(Resources.Load<GameObject>("Prefabs/Coin"), new Vector2(Random.Range(-2f,2f),platformLastHeight+6), transform.rotation);
			}
			prize=true;
		}
	}
	#endregion
	void CreateWindows(int quantity, int sequence_num){
		if(Ball.pos.y > windowLastHeight-basicDistance && !reachedWindowH[sequence_num]){
			for(int i=0;i<quantity;i++){
				CreateObject(ref window,"GothicWindow");
				UpdateHeight(ref windowLastHeight, 50, 80);
				window.transform.position = NextPos(4.1f, windowLastHeight, 1);
			}
			reachedWindowH[sequence_num] = true;
		}
	}
	void CreateTorches(int quantity, int sequence_num){
		if(Ball.pos.y > torchLastHeight-basicDistance && reachedTorchH[sequence_num]){
			for(int i=0;i<quantity;i++){
				CreateObject(ref torch,"Torch");
				UpdateHeight(ref torchLastHeight, 8, 16);
				torch.transform.position = NextPos(4, torchLastHeight, 1);
			}
			reachedTorchH[sequence_num] = false;
		}
	}
	void CreateObject(ref GameObject cloneObj, string prefabName){
		GameObject obj = Resources.Load<GameObject>("Prefabs/"+prefabName);
		cloneObj  = Instantiate(obj, Vector2.zero, transform.rotation, transform);
	}
	Vector3 NextPos(float maxXPos, float y, int z=0){
		Vector3 newPos = new Vector3(Random.Range(-maxXPos, maxXPos), y, z);
		return newPos;
	}
	void UpdateHeight(ref float lastHeight, int minHChange, int maxHChange){
		int heightChange = Random.Range(minHChange, maxHChange);
		lastHeight += heightChange;
	}
}
