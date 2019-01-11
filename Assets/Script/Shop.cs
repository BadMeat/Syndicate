using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour {

	public static Shop shop;

	public GameObject[] allWeapon;
	public GameObject[] allArmor;
	public GameObject[] allBoot;
	public Sprite[] allCharacter;
	public Image character;

	public int Money{set;get;}

	//semua status player
	public Text healthText;
	public Text delayText;
	public Text speedText;
	public Text goldPlayer;

	//pilihan item
	public GameObject weaponContent;
	public GameObject armorContent;
	public GameObject bootContent;

	//tombol pilih item
	public Button weapon;
	public Button armor;
	public Button boot;

	//warning duit abis
	public Text warning;
	private bool onWarning;

	private ScrollRect scroll;

	//tombol oke
	public Button map;
	public Button charSelection;

	void Awake(){
		if(shop == null){
			shop = this;
		}
		else if(shop != null){
			Destroy(gameObject);
		}
	}

	void Start () {
		Money = Game.game.Gold;
		healthText.text = Game.game.Health +" + "+"("+Game.game.healthUpgrade+")";
		delayText.text = Game.game.Delay +" + "+"("+Game.game.delayUpgrade+")";
		speedText.text = Game.game.Speed +" + "+"("+Game.game.speedUpgrade+")";

		goldPlayer.text = "RP "+Money;
		scroll = transform.FindChild("Scroll View").GetComponent<ScrollRect>();
		for(int a=0;a<allWeapon.Length;a++){
			GameObject wp = Instantiate(allWeapon[a]) as GameObject;
			wp.transform.SetParent(weaponContent.transform,false);
		}
		for(int a=0;a<allArmor.Length;a++){
			GameObject wp = Instantiate(allArmor[a]) as GameObject;
			wp.transform.SetParent(armorContent.transform,false);
		}
		for(int a=0;a<allBoot.Length;a++){
			GameObject wp = Instantiate(allBoot[a]) as GameObject;
			wp.transform.SetParent(bootContent.transform,false);
		}
		character.sprite = allCharacter[Game.game.CharacterSelected];
		armorContent.SetActive(false);
		bootContent.SetActive(false);
	}

	public void Weapon(){
		scroll.content = weaponContent.GetComponent<RectTransform>();
		weaponContent.SetActive(true);
		armorContent.SetActive(false);
		bootContent.SetActive(false);
	}

	public void Armor(){
		scroll.content = armorContent.GetComponent<RectTransform>();;
		weaponContent.SetActive(false);
		armorContent.SetActive(true);
		bootContent.SetActive(false);
	}

	public void Boot(){
		scroll.content = bootContent.GetComponent<RectTransform>();;
		weaponContent.SetActive(false);
		armorContent.SetActive(false);
		bootContent.SetActive(true);
	}

	public IEnumerator NeedMoney(){
		if(onWarning){
			yield break;
		}

		onWarning = true;
		for(int a=0;a<3;a++){
			warning.gameObject.SetActive(true);
			yield return new WaitForSeconds(0.2f);
			warning.gameObject.SetActive(false);
			yield return new WaitForSeconds(0.2f);
		}
		onWarning = false;
	}

	//setGold
	public void SetTextGold(){
		goldPlayer.text = "RP "+Money;
	}

	//save 
	public void Char(){
		Game.game.Gold = Money;
		SaveLoad.Save();
		SceneManager.LoadScene("CharacterSelection");
	}

	public void Map(){
		Game.game.Gold = Money;
		SaveLoad.Save();
		SceneManager.LoadScene("Map");
	}
}
