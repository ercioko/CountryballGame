using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour {
	public GameObject weapon, skinsScroll, weaponsScroll, armorScroll, potionsScroll;
	public GameObject lastBallObj, lastWpnObj;
	GameObject actualScroll;
	Animator animator;
	private void Start() {
		actualScroll = skinsScroll;
		ShowSkins();
	}
	public void BuyBall(int price, ref bool isBought, string name){
		Pay(price,ref isBought);
		if(isBought){
			SetBall(name);
			Select(GameObject.Find(name+"Buy"), ref lastBallObj);
		}
	}
	public void BuyWeapon(int price, ref bool isBought, string name, Vector2 colliderSize, Vector2 colliderOffset){
		Pay(price,ref isBought);
		if(isBought){
			SetWeapon(name, colliderSize, colliderOffset);
			Select(GameObject.Find(name+"Buy"), ref lastWpnObj);
		}
	}
	public static void SetWeapon(string name, Vector2 colliderSize, Vector2 colliderOffset){
		Weapon.weapon.GetComponent<SpriteRenderer>().sprite
		= Resources.Load<Sprite>("Weapons/"+name);
		Weapon.weapon.GetComponent<BoxCollider2D>().size = colliderSize;
		Weapon.weapon.GetComponent<BoxCollider2D>().offset = colliderOffset;
	}
	public static void SetBall(string name){
		Ball.ball.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(name+"ball");
		Ball.ball.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<AnimatorOverrideController>("Animations/BallAnim/"+ name+"ball"+"/"+name+"ball");
	}
	public static void Pay(int price,ref bool isBought){
		if(PlayerData.money>=price&&!isBought){
			PlayerData.money -= price;
			isBought = true;
		}
	}
	public static void ScrollActive(ref GameObject oldScroll, GameObject newScroll){
		oldScroll.SetActive(false);
		oldScroll = newScroll;
		newScroll.SetActive(true);
	}
	public static void Select(GameObject current, ref GameObject lastObj) {
		lastObj.GetComponent<Text>().color = Color.white;
		current.GetComponent<Text>().color = Color.green;
		lastObj = current;
	}
	public static void FixText(bool isBought, string name){
		if(isBought){
			GameObject.Find(name+"Buy").GetComponent<Text>().text = "Select";
		}
	}
	public void ShowSkins(){
		ScrollActive(ref actualScroll, skinsScroll);
	}
	public void ShowWeapons(){
		ScrollActive(ref actualScroll, weaponsScroll);
	}
	public void ShowArmor(){
		ScrollActive(ref actualScroll, armorScroll);
	}
	public void ShowPotions(){
		ScrollActive(ref actualScroll, potionsScroll);
	}
}
