using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinsStore : MonoBehaviour {
	public Store store;
	private void Update() {
		if(GameObject.Find("SkinsScroll") != null){
			Store.FixText(PlayerData.USA, "Poland");	
			Store.FixText(PlayerData.USA, "USA");	
			Store.FixText(PlayerData.Russia, "Russia");
			Store.FixText(PlayerData.Germany, "Germany");
			Store.FixText(PlayerData.France, "France");
			Store.FixText(PlayerData.Japan, "Japan");
		}
	}
	
	public void BuyPolandball(){
		store.BuyBall(0, ref PlayerData.Poland, "Poland");
	}
	public void BuyUSAball(){
		store.BuyBall(50, ref PlayerData.USA, "USA");
	}
	public void BuyRussiaball(){
		store.BuyBall(75, ref PlayerData.Russia, "Russia");
	}
	public void BuyGermanyball(){
		store.BuyBall(100, ref PlayerData.Germany, "Germany");
	}
	public void BuyFranceball(){
		store.BuyBall(150, ref PlayerData.France, "France");
	}
	public void BuyJapanball(){
		store.BuyBall(175, ref PlayerData.Japan, "Japan");
	}
}

