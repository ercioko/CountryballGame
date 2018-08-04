using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsStore : MonoBehaviour {
	public Store store;
	private void Update () {
		if(GameObject.Find("WeaponsScroll") != null){
			Store.FixText(PlayerData.Sword, "Sword");
			Store.FixText(PlayerData.DemonAxe, "DemonAxe");
			Store.FixText(PlayerData.WitchAxe, "WitchAxe");
			Store.FixText(PlayerData.AbyssKnightAxe, "AbyssKnightSword");
		}
	}
	public void BuySword(){
		store.BuyWeapon(0, ref PlayerData.Sword, "Sword", new Vector2(0.2f, 2f), Vector2.zero );
	}
	public void BuyDemonAxe(){
		store.BuyWeapon(100, ref PlayerData.DemonAxe, "DemonAxe", new Vector2(1.8f, 2.5f), new Vector2(0, 0.7f) );
	}
	public void BuyWitchAxe(){
		store.BuyWeapon(200, ref PlayerData.WitchAxe, "WitchAxe", new Vector2(1.8f, 2.5f), new Vector2(0, 0.7f) );
	}
	public void BuyAbyssKnightSword(){
		store.BuyWeapon(300, ref PlayerData.AbyssKnightAxe, "AbyssKnightSword", new Vector2(0.25f, 3.9f), new Vector2(0, 2f) );
	}
}
