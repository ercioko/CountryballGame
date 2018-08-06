using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;

public class StoreTests {

	[Test]
	public void FixText_isBoughtTrue_ChangeTextToSelect(){
		//ARRANGE
		bool isBought = true;
		string name = "Item";
		GameObject testObject;
		testObject = new GameObject("ItemBuy");
        testObject.AddComponent<Text>();
		//ACT
		Store.FixText(isBought, name);
		//ASSERT
		Assert.AreSame(GameObject.Find("ItemBuy").GetComponent<Text>().text, "Select");
	}
	[Test]
	public void FixText_isBoughtFalse_LeaveTextSame(){
		//ARRANGE
		bool isBought = false;
		string name = "Item";
		GameObject testObject;
		testObject = new GameObject("ItemBuy");
        testObject.AddComponent<Text>();
		//ACT
		Store.FixText(isBought, name);
		//ASSERT
		Assert.AreNotSame(GameObject.Find("ItemBuy").GetComponent<Text>().text, "Select");
	}
	[Test]
	public void Select_differentGameobjects_changeSelection(){
		//ARRANGE
		GameObject lastObj = new GameObject("text_1");
		lastObj.AddComponent<Text>();
		lastObj.GetComponent<Text>().text = "Select";
		lastObj.GetComponent<Text>().color = Color.green;
		GameObject current = new GameObject("text_2");
		current.AddComponent<Text>();
		current.GetComponent<Text>().text = "Select";
		current.GetComponent<Text>().color = Color.white;
		//ACT
		Store.Select(GameObject.Find("text_2"), ref lastObj);
		//ASSERT
		Assert.IsTrue(current.GetComponent<Text>().color == Color.green);
		Assert.IsTrue(GameObject.Find("text_1").GetComponent<Text>().color == Color.white);
		Assert.AreSame(lastObj, current);
	}
	[Test]
	public void Select_oneGameobject_noChange(){
		//ARRANGE
		GameObject current = new GameObject("text_1");
		current.AddComponent<Text>();
		current.GetComponent<Text>().text = "Select";
		current.GetComponent<Text>().color = Color.white;
		//ACT
		Store.Select(GameObject.Find("text_1"), ref current);
		//ASSERT
		Assert.IsTrue(current.GetComponent<Text>().color == Color.green);
	}
	[Test]
	public void ScrollActive_differentScrollClicked_changeActiveScroll(){
		//ARRANGE
		GameObject oldScroll = new GameObject("FirstScroll");
		GameObject newScroll = new GameObject("SecondScroll");
		//ACT
		Store.ScrollActive(ref oldScroll, newScroll);
		//ASSERT
		Assert.IsTrue(GameObject.Find("SecondScroll").activeSelf);
		Assert.IsNull(GameObject.Find("FirstScroll"));
		Assert.AreSame(oldScroll, newScroll);
	}
	[Test]
	public void ScrollActive_sameScrollClicked_noChange(){
		//ARRANGE
		GameObject oldScroll = new GameObject("FirstScroll");
		//ACT
		Store.ScrollActive(ref oldScroll, oldScroll);
		//ASSERT
		Assert.IsTrue(GameObject.Find("FirstScroll").activeSelf);
	}
	[Test]
	public void Pay_isNotBoughtNotEnoughMoney_doNothing(){
		//ARRANGE
		PlayerData.money = 5;
		int price = 10;
		bool isBought = false;
		//ACT
		Store.Pay(price, ref isBought);
		//ASSERT
		Assert.AreEqual(PlayerData.money, 5);
		Assert.IsFalse(isBought);
	}
	[Test]
	public void Pay_isBoughtEnoughMoney_doNothing(){
		//ARRANGE
		PlayerData.money = 10;
		int price = 10;
		bool isBought = true;
		//ACT
		Store.Pay(price, ref isBought);
		//ASSERT
		Assert.AreEqual(PlayerData.money, 10);
		Assert.IsTrue(isBought);
	}
	[Test]
	public void Pay_isNotBoughtEnoughMoney_takeMoneyAndsetBought(){
		//ARRANGE
		PlayerData.money = 10;
		int price = 10;
		bool isBought = false;
		//ACT
		Store.Pay(price, ref isBought);
		//ASSERT
		Assert.AreEqual(PlayerData.money, 0);
		Assert.IsTrue(isBought);
	}
	[Test]
	public void SetBall_basicName_changeBallSpriteANDAnimator(){
		//ARRANGE
		string name = "USA";
		GameObject testBall = new GameObject("Ball");
		testBall.AddComponent<SpriteRenderer>();
		testBall.AddComponent<Animator>();
		Ball.ball = testBall;
		//ACT
		Store.SetBall(name);
		//ASSERT	
		Assert.IsTrue(Ball.ball.GetComponent<SpriteRenderer>().sprite.name == name+"ball");
		Assert.IsTrue(Ball.ball.GetComponent<Animator>().runtimeAnimatorController.name == name+"ball");
	}
	[Test]
	public void SetWeapon_basicSettings_ChangeComponents(){
		//ARRANGE
		string name = "DemonAxe";
		Vector2 colliderSize = Vector2.one;
		Vector2 colliderOffset = Vector2.zero;
		GameObject testWeapon = new GameObject("Weapon");
		testWeapon.AddComponent<SpriteRenderer>();
		testWeapon.AddComponent<BoxCollider2D>();
		Weapon.weapon = testWeapon;
		//ACT
		Store.SetWeapon(name, colliderSize, colliderOffset);
		//ASSERT
		Assert.IsTrue(Weapon.weapon.GetComponent<SpriteRenderer>().sprite.name == name);
		Assert.IsTrue(Weapon.weapon.GetComponent<BoxCollider2D>().size == colliderSize);
		Assert.IsTrue(Weapon.weapon.GetComponent<BoxCollider2D>().offset == colliderOffset);
	}
}
