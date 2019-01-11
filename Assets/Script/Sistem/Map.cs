using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour {

	public Button[] level;
	public Button enterLevel;
	public Button backLevel;
	public Text alertText;

	private int levelChoosen;
	private bool onSelect;
	private bool onAlert;

	void Start () {
		for(int a=0;a<3;a++){
			if(Game.game.DoorPass < a){
				level[a].gameObject.SetActive(false);
			}
		}

		level[0].onClick.AddListener(()=> Level1());
		level[1].onClick.AddListener(()=> Level2());
		level[2].onClick.AddListener(()=> Level3());
		enterLevel.onClick.AddListener(()=> EnterLevel());
		backLevel.onClick.AddListener(()=> BackLevel());
	}
		
	public void Level1(){
		if(Game.game.DoorPass >= 0){
			levelChoosen = 0;
			ChangeColorButton();
			onSelect = true;
		}
	}

	public void Level2(){
		if(Game.game.DoorPass >= 1){
			levelChoosen = 1;
			ChangeColorButton();
			onSelect = true;
		}
	}

	public void Level3(){
		if(Game.game.DoorPass >= 2){
			levelChoosen = 2;
			ChangeColorButton();
			onSelect = true;
		}
	}

	private void ChangeColorButton(){
		for(int a=0;a<level.Length;a++){
			if(levelChoosen == a){
				ColorBlock cb = level[a].colors;
				cb.normalColor = Color.white;
				level[a].colors = cb;
			}else{
				ColorBlock cb = level[a].colors;
				cb.normalColor = new Color(0.2f,0.2f,0.2f);
				level[a].colors = cb;
			}
		}
	}

	public void EnterLevel(){
		if(onSelect){
			Game.game.LevelEnter = levelChoosen;
			SceneManager.LoadScene("Loading");
		}else{
			if(!onAlert){
				StartCoroutine(Alert());
			}
		}
	}

	private IEnumerator Alert(){
		onAlert = true;
		for(int a=0;a<3;a++){
			alertText.gameObject.SetActive(true);
			yield return new WaitForSeconds(0.3f);
			alertText.gameObject.SetActive(false);
			yield return new WaitForSeconds(0.3f);
		}
		onAlert = false;
	}

	private void BackLevel(){
		SceneManager.LoadScene("LoadingCharacter");
	}
}
