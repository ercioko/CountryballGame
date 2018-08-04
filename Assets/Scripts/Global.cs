using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour{
	public static bool chanceInPercent(int b){
		int randomShot = Random.Range(0,100);
		if(randomShot>=0 && randomShot<=b)
			return true;
		else
			return false;
	}
	public static void OptimizeObjectRender(GameObject obj){
		if(obj.transform.position.y > Ball.pos.y - 2*Ball.fallHeight && obj.transform.position.y < Ball.pos.y + 2*Ball.fallHeight)
			obj.GetComponent<SpriteRenderer>().enabled = true;
		else if(obj.transform.position.y < Ball.pos.y - 2*Ball.fallHeight && Ball.pos.y > 2*Ball.fallHeight)
			Destroy(obj);
		else
			obj.GetComponent<SpriteRenderer>().enabled = false;
	}
}
