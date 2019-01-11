using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour {

	public Image char1;
	public Image char2;
	public Image char3;
	public Sprite[] allCharater;

	public Vector3[] startPos;

	//nama karakter
	public string[] theName;
	public Text nameText;
	public Text healthText;
	public Text speedText;
	public Text delayText;

	//status karakter
	public string[] health;
	public string[] speed;
	public string[] delay;

	private int charSelected;
	private Vector3 touchPertama;
	private Vector3 touchTerakhir;

	//shop
	public Button shop;

	void Start(){
		charSelected = 1;
	}

	void Update () {
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began){
				touchPertama = touch.position;
				touchTerakhir = touch.position;
			}

			if (touch.phase == TouchPhase.Ended){
				touchTerakhir = touch.position;
			}

			if (touchPertama.x - touchTerakhir.x > 20f){
				if(charSelected < allCharater.Length - 1){
					charSelected++;
				}
				touchPertama = new Vector3(0f, 0f, 0f);
				touchTerakhir = new Vector3(0f, 0f, 0f);
			}

			else if (touchPertama.x - touchTerakhir.x < -20f){
				if(charSelected > 0){
					charSelected--;
				}
				touchPertama = new Vector3(0f, 0f, 0f);
				touchTerakhir = new Vector3(0f, 0f, 0f);
			}
		}
		if(charSelected == 0){
			char1.color = new Color(char1.color.r,char1.color.g,char1.color.b,0f);
			char2.sprite = allCharater[0];
			char3.sprite = allCharater[1];
			nameText.text = ": "+theName[0];
			healthText.text = ": "+health[0]+" + "+Game.game.healthUpgrade;
			speedText.text = ": "+speed[0]+" + "+Game.game.speedUpgrade;
			delayText.text = ": "+delay[0]+" + "+Game.game.delayUpgrade;
		}
		else if(charSelected == allCharater.Length - 1){
			char1.sprite = allCharater[allCharater.Length - 2];
			char2.sprite = allCharater[allCharater.Length - 1];
			nameText.text =  ": "+theName[allCharater.Length - 1];
			healthText.text = ": "+health[allCharater.Length - 1]+" + "+Game.game.healthUpgrade;
			speedText.text = ": "+speed[allCharater.Length - 1]+" + "+Game.game.speedUpgrade;
			delayText.text = ": "+delay[allCharater.Length - 1]+" + "+Game.game.delayUpgrade;
			char3.color = new Color(char3.color.r,char3.color.g,char3.color.b,0f);
		}else{
			for(int a=1;a<allCharater.Length;a++){
				if(charSelected == a){
					char1.sprite = allCharater[a-1];
					char2.sprite = allCharater[a];
					char3.sprite = allCharater[a+1];

					nameText.text =  ": "+theName[a];
					healthText.text = ": "+health[a]+" + "+Game.game.healthUpgrade;
					speedText.text = ": "+speed[a]+" + "+Game.game.speedUpgrade;
					delayText.text = ": "+delay[a]+" + "+Game.game.delayUpgrade;
				}
			}
			char1.color = new Color(char1.color.r,char1.color.g,char1.color.b,1f);
			char3.color = new Color(char3.color.r,char3.color.g,char3.color.b,1f);
		}

		if(charSelected == 0){
			
		}
			
		if(Input.GetKeyDown(KeyCode.A)){
			if(charSelected > 0){
				charSelected--;
			}
		}

		if(Input.GetKeyDown(KeyCode.D)){
			if(charSelected < allCharater.Length - 1){
				charSelected++;
			}
		}
	}

	public void Oke(){
		Game.game.CharacterSelected = charSelected;
		if(charSelected == 0){
			Game.game.Health = 3;
			Game.game.Damage = 1f;
			Game.game.Speed = 20f;
			Game.game.Delay = 0.5f;
		}
		else if(charSelected == 1){
			Game.game.Health = 7;
			Game.game.Damage = 1f;
			Game.game.Speed = 10f;
			Game.game.Delay = 1f;
		}
		else if(charSelected == 2){
			Game.game.Health = 5;
			Game.game.Damage = 1f;
			Game.game.Speed = 15f;
			Game.game.Delay = 0.7f;
		}
		SceneManager.LoadScene("LoadingMap");
	}

	public void Back(){
		SceneManager.LoadScene("LoadingBack");
	}

	public void Shop(){
		if(charSelected == 0){
			Game.game.Health = 3;
			Game.game.Damage = 1f;
			Game.game.Speed = 20f;
			Game.game.Delay = 0.5f;
		}
		else if(charSelected == 1){
			Game.game.Health = 7;
			Game.game.Damage = 1f;
			Game.game.Speed = 10f;
			Game.game.Delay = 1f;
		}
		else if(charSelected == 2){
			Game.game.Health = 5;
			Game.game.Damage = 1f;
			Game.game.Speed = 15f;
			Game.game.Delay = 0.7f;
		}
		Game.game.CharacterSelected = charSelected;
		SceneManager.LoadScene("Shop");
	}
}
