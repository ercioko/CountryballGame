using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour {
	GameObject platform, window, torch;
	GameObject[] normal, move, destroy, destroyMove, spring;
	static bool prize, start;
	static float lastHeight, basicDistance = 15;
	static bool[] reachedPlatformH = new bool[4];
	public static float minH = 2, maxH = 6;

	void Start(){
		Reset();
	}
	public static void  Reset() {
		DestroyObj(Global.platformTag);
		DestroyObj(Global.platformMovingTag);
		DestroyObj(Global.platformFragileMovingTag);
		DestroyObj(Global.platformFragileTag);
		DestroyObj(Global.platformCoinTag);
		reachedPlatformH = new bool[4];
		lastHeight=0;
		start=true;
	}
	static void DestroyObj(string name){
		GameObject[] obj = GameObject.FindGameObjectsWithTag(name);
		foreach(GameObject t in obj)
			Destroy(t);
	}
	void Update () {
		if(start){
			CreateSegments(10, 0);
			CreateSegments(40, 1);
			CreateSegments(50, 2);
			CreateSegments(50, 3);	
			CreatePrize();
		}
	}
	void CreateSegments(int quantity, int lvl){
		if(Ball.pos.y > lastHeight-basicDistance && !reachedPlatformH[lvl]){
			for(int i=0; i < quantity; i++){

				CreatePlatform(ref lastHeight, minH, maxH);
				CreateWindow(10, lastHeight, minH, maxH);
				CreateTorch(40, lastHeight, minH, maxH);

				if(lvl==1)
					platform.transform.localScale = new Vector2(0.8f,1f);
				if(lvl>1)	
					platform.transform.localScale = new Vector2(0.5f,1f);
				
				PickRandomTag(lvl);
		}
		reachedPlatformH[lvl]=true;
		}
	}
	#region Object Creation Methods
	void CreatePrize(){
		if(Ball.pos.y > lastHeight-basicDistance && !prize){
			CreatePlatform(ref lastHeight, 2, 4);
			platform.transform.localScale = new Vector2(4f,1f);
			platform.GetComponent<Platform>().CreateCoins(50);

			prize=true;
		}
	}
	void CreatePlatform(ref float lastHeight, float minH, float maxH){
		CreateObject(ref platform, "Platform");
		lastHeight = NewHeight(minH, maxH);
		platform.transform.position = NextPos(4, lastHeight);
	}
	void CreateWindow(int chance, float lastHeight, float minH, float maxH){
		if(Global.ChanceInPercent(chance)){
			CreateObject(ref window, "GothicWindow");
			window.transform.position = NextPos(4f, NewHeight(minH, maxH), 1);
		}
	}
	void CreateTorch(int chance, float lastHeight, float minH, float maxH){
		if(Global.ChanceInPercent(chance)){
			CreateObject(ref torch, "Torch");
			torch.transform.position = NextPos(4.1f, NewHeight(minH, maxH), 1);
		}
	}
	#endregion
	#region Platform Tagging
	void PickRandomTag(int lvl){ 
		if(Global.ChanceInPercent(30) && lvl>=1)
			SetTag(platform, Global.platformMovingTag, new Color(0,128,192,255));
		if(Global.ChanceInPercent(20) && lvl>=2)
			SetTag(platform, Global.platformFragileTag, new Color(128,0,0,255));
		if(Global.ChanceInPercent(20) && lvl>=3)
			SetTag(platform, Global.platformFragileMovingTag, Color.black);
		if(Global.ChanceInPercent(20) && lvl>=0)
			platform.GetComponent<Platform>().AddSpring();
		if(Global.ChanceInPercent(10*lvl) && lvl>=1 ) 
			platform.GetComponent<Platform>().AddEnemy();
	}
	void SetTag(GameObject obj, string tag, Color color){
		obj.tag = tag;
		obj.GetComponent<SpriteRenderer>().color = color;
	}
	#endregion
	void CreateObject(ref GameObject cloneObj, string prefabName){
		GameObject obj = Resources.Load<GameObject>("Prefabs/"+prefabName);
		cloneObj  = Instantiate(obj, Vector3.zero, transform.rotation, transform);
	}
	Vector3 NextPos(float maxXPos, float y, int z=0){
		Vector3 newPos = new Vector3(Random.Range(-maxXPos, maxXPos), y, z);
		return newPos;
	}
	float NewHeight(float minHChange, float maxHChange){
		return lastHeight + Random.Range(minHChange, maxHChange);
	}
}