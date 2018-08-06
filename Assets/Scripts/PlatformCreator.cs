using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour {
	GameObject platform, window, torch;
	GameObject[] normal, move, destroy, destroyMove, spring;
	static bool prize, start;
	static float lastHeight, basicDistance = 15;
	static bool[] reachedPlatformH = new bool[4];
	public static float minH = 2, maxH = 6, hChange;

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
				CreateWindow(10, lastHeight, hChange, 4);
				CreateTorch(40, lastHeight, hChange, 2);

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
			CreatePlatform(ref lastHeight, 5, 5);
			platform.transform.localScale = new Vector2(4f,1f);

			for(int i=0;i<50;i++){
				Instantiate(Resources.Load<GameObject>("Prefabs/Coin"), new Vector2(Random.Range(-3,3), lastHeight+6), transform.rotation);
			}
			prize=true;
		}
	}
	void CreatePlatform(ref float lastHeight, float minH, float maxH){
		CreateObject(ref platform, "Platform");
		hChange = HeightChange(minH, maxH);
		lastHeight += hChange;
		platform.transform.position = NextPos(4, lastHeight);
	}
	void CreateWindow(int chance, float lastHeight, float platformHChange, float objWidth){
		if(Global.ChanceInPercent(chance)){
			CreateObject(ref window, "GothicWindow");
			float newH = lastHeight + HeightChange(0, platformHChange, objWidth);
			window.transform.position = NextPos(4.1f, newH, 1);
		}
	}
	void CreateTorch(int chance, float lastHeight, float platformHChange, float objWidth){
		if(Global.ChanceInPercent(chance)){
			CreateObject(ref torch, "Torch");
			float newH = lastHeight + HeightChange(0, platformHChange, objWidth);
			torch.transform.position = NextPos(4.1f, newH, 1);
		}
	}
	#endregion
	#region Platform Tagging
	void PickRandomTag(int lvl){ 
		if(Global.ChanceInPercent(60) && lvl>=1)
			SetTag(platform, Global.platformMovingTag, new Color(0,128,192,255));
		else if(Global.ChanceInPercent(40) && lvl>=2)
			SetTag(platform, Global.platformFragileTag, new Color(128,0,0,255));
		else if(Global.ChanceInPercent(30) && lvl>=3)
			SetTag(platform, Global.platformFragileMovingTag, Color.black);
		else if(Global.ChanceInPercent(20) && lvl>=0)
			SetTag(platform, Global.platformSpringTag, Color.white);
		else if(Global.ChanceInPercent(20*lvl) && lvl>=1 ) 
			SetTag(platform, Global.platformEnemyTag, Color.white);
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
	float HeightChange(float minHChange, float maxHChange, float objWidth=0){
		return Random.Range(minHChange + objWidth/2, maxHChange - objWidth/2);
	}
}
