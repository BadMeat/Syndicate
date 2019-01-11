using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour {

	public string theName;
	public float effect;
	public int price;

	public enum Types{
		speed,
		damage,
		armor,
		delay
	}
	public Types type;

	private bool alreadyHave;

	void Start(){
		if(Game.game.weaponGet.Count > 0){
			for(int a=0;a<Game.game.weaponGet.Count;a++){
				if(theName == Game.game.weaponGet[a]){
					Text txt = transform.FindChild("Button").transform.FindChild("Text").GetComponent<Text>();
					txt.text = "Equipt";
					gameObject.GetComponent<Image>().color = new Color(1f,1f,1f,0.1f);
					alreadyHave = true;
				}
			}
		}
			
		if(theName == Game.game.WeaponUse){
			Text txt = transform.FindChild("Button").transform.FindChild("Text").GetComponent<Text>();
			txt.text = "Equiped";
			gameObject.GetComponent<Image>().color = new Color(0f,0f,0f,0.1f);
		}
	}

	public void BuyItem(){
		if(Game.game.weaponGet.Count > 0){
			for(int a=0;a<Game.game.weaponGet.Count;a++){
				if(theName == Game.game.weaponGet[a]){
					alreadyHave = true;
					break;
				}
			}
			if(alreadyHave){
				SetEffect();
			}
			else if(Shop.shop.Money >= price){
				alreadyHave = true;
				Shop.shop.Money -= price;
				Shop.shop.SetTextGold();
				Game.game.weaponGet.Add(theName);
				SetEffect();
			}else{
				StartCoroutine(Shop.shop.NeedMoney());
			}
		}else{
			if(Shop.shop.Money >= price){
				alreadyHave = true;
				Shop.shop.Money -= price;
				Shop.shop.SetTextGold();
				Game.game.weaponGet.Add(theName);
				SetEffect();
			}else{
				StartCoroutine(Shop.shop.NeedMoney());
			}
		}
			
		for(int a=0;a<Shop.shop.allWeapon.Length;a++){
			GameObject[] weap = GameObject.FindGameObjectsWithTag("Weapon");
			if(gameObject == weap[a]){
				weap[a].GetComponent<Image>().color = new Color(0f,0f,0f,0.1f);
				weap[a].transform.FindChild("Button").FindChild("Text").GetComponent<Text>().text = "Equiped";
			}else if(weap[a].GetComponent<WeaponShop>().alreadyHave){
				weap[a].GetComponent<Image>().color = new Color(1f,1f,1f,0.1f);
				weap[a].transform.FindChild("Button").FindChild("Text").GetComponent<Text>().text = "Equipt";
			}
		}
	}

	private void SetEffect(){
		Game.game.WeaponUse = theName;
		Text txt = transform.FindChild("Button").transform.FindChild("Text").GetComponent<Text>();
		txt.text = "Equiped";
		switch(type){
		case(Types.speed):
			Game.game.speedUpgrade = effect;
			Shop.shop.healthText.text = Game.game.Speed +" + "+"("+Game.game.speedUpgrade+")";
			break;
		case(Types.damage):
			Game.game.damageUpgrade = effect;
			//Shop.shop..text = "+ "+effect;
			break;
		case(Types.armor):
			Game.game.healthUpgrade = effect;
			Shop.shop.healthText.text = Game.game.Health +" + "+"("+Game.game.healthUpgrade+")";
			break;
		case(Types.delay):
			Game.game.delayUpgrade = effect;
			Shop.shop.healthText.text = Game.game.Delay +" + "+"("+Game.game.delayUpgrade+")";
			break;
		}
	}
}
