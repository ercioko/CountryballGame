using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerData : MonoBehaviour {
	public static int money, score, record, health, strength, atkSpeed, mana;
	public static bool Poland, USA, Russia, Germany, France, Japan;
	public static bool Sword, DemonAxe, WitchAxe, AbyssKnightAxe;
	public void Update () {
		SetScore();
		UpdateScore();
	}
	void SetScore(){
		if(score<Mathf.Round(Ball.pos.y))
			score = Mathf.RoundToInt(Mathf.Round(Ball.pos.y));
		if(score>record)
			record = score;
	}
	void UpdateScore(){
		if(GameObject.Find("Score")!=null)
			GameObject.Find("Score").GetComponent<Text>().text = score.ToString();
		if(GameObject.Find("Money")!=null)
			GameObject.Find("Money").GetComponent<Text>().text = money.ToString();
		if(GameObject.Find("Record")!=null)
			GameObject.Find("Record").GetComponent<Text>().text=record.ToString();
	}
	public static void Save(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/score.dat");
		MyData data = new MyData(
			money, 
			record, 
			health, 
			strength, 
			atkSpeed, 
			mana,
			Poland, 
			USA, 
			Russia, 
			Germany, 
			France, 
			Japan, 
			Sword, 
			DemonAxe, 
			WitchAxe, 
			AbyssKnightAxe
		);
		
		data.money = money;
		data.record = record;
		data.health = health;
		data.strength = strength;
		data.atkSpeed = atkSpeed;
		data.mana = mana;
		data.Poland = Poland;
		data.USA = USA;
		data.Russia = Russia;
		data.Germany = Germany;
		data.France = France;
		data.Japan = Japan;
		data.Sword = Sword;
		data.DemonAxe = DemonAxe;
		data.WitchAxe = WitchAxe;
		data.AbyssKnightAxe = AbyssKnightAxe;

		bf.Serialize(file,data);
		file.Close();
	}
	public static void Load(){
		if(File.Exists(Application.persistentDataPath + "/score.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/score.dat", FileMode.Open);
			MyData data = (MyData)bf.Deserialize(file);
			file.Close();
			money = data.money;
			record = data.record;
			health = data.health;
			strength = data.strength;
			atkSpeed = data.atkSpeed;
			mana = data.mana;
			Poland = data.Poland;
			USA = data.USA;
			Russia = data.Russia;
			Germany = data.Germany;
			France = data.France;
			Japan = data.Japan;
			Sword = data.Sword;
			DemonAxe = data.DemonAxe;
			WitchAxe = data.WitchAxe;
			AbyssKnightAxe = data.AbyssKnightAxe;
		}
	}
}

[System.Serializable]
public class MyData{
	public int money, record, health, strength, atkSpeed, mana;
	public bool Poland, USA, Russia, Germany, France, Japan;
	public bool Sword, DemonAxe, WitchAxe, AbyssKnightAxe;

	public MyData(
				int moneyTemp, 
				int recordTemp, 
				int healthTemp, 
				int strengthTemp, 
				int atkSpeedTemp, 
				int manaTemp,
				bool PolandTemp,
				bool USATemp, 
				bool RussiaTemp, 
				bool GermanyTemp, 
				bool FranceTemp, 
				bool JapanTemp,
				bool SwordTemp,
				bool DemonAxeTemp,
				bool WitchAxeTemp,
				bool AbyssKnightSwordTemp
				){
		money = moneyTemp;
		record = recordTemp;
		health = healthTemp;
		strength = strengthTemp;
		atkSpeed = atkSpeedTemp;
		mana = manaTemp;

		Poland = PolandTemp;
		USA = USATemp; 
		Russia = RussiaTemp;
		Germany = GermanyTemp;
		France = FranceTemp;
		Japan = JapanTemp;
		
		Sword = SwordTemp;
		DemonAxe = DemonAxeTemp;
		WitchAxe = WitchAxeTemp;
		AbyssKnightAxe = AbyssKnightSwordTemp;
	}
	
}