using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backround : MonoBehaviour {

	public Transform backround1, backround2;
	public GameObject cam, ball, border;
	static float currentHeight = 24, swapH= 24f;
	bool whichOne = true;

	void FixedUpdate() {
		CamPos();
	}
	void Update () {
		SwapBackground();
	}
	void SwapBackground(){
		if(currentHeight < cam.transform.position.y){
			if(whichOne){
				backround1.localPosition = new Vector3(0, backround1.localPosition.y + 2*swapH, 0);
			}else{
				backround2.localPosition = new Vector3(0, backround2.localPosition.y + 2*swapH, 0);
			}
			currentHeight += swapH;
			whichOne = !whichOne;
		}
		if(currentHeight > cam.transform.position.y+swapH){
			if(whichOne){
				backround2.localPosition = new Vector3(0, backround2.localPosition.y - 2*swapH, 0);
			}else{
				backround1.localPosition = new Vector3(0, backround1.localPosition.y - 2*swapH, 0);
			}
			currentHeight -= swapH;
			whichOne = !whichOne;
		}
	}
	public void Reset() {
		backround1.localPosition = new Vector3(0,0,0);
		backround2.localPosition = new Vector3(0,swapH,0);
		currentHeight = swapH;
		whichOne =true;

		Vector3 camPos = cam.transform.position;
		camPos.y = 7.65f;
		cam.transform.position = camPos;
	}
	void CamPos(){
		Vector3 camPos, ballPos, borderPos;
		camPos = cam.transform.position;
		ballPos = ball.transform.position;
		borderPos = border.transform.position;
		
		camPos.y = Vector2.Lerp(camPos, new Vector2(0, Ball.topHeight), 0.5f).y;
		borderPos.y = camPos.y;

		cam.transform.position = camPos;
		border.transform.position = borderPos;
	}
}
