using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUtama : MonoBehaviour {

    public GameObject buttonHelp;
	public Button buttonHelpBack;

    public GameObject buttonYesNo;
    public GameObject buttonYesNoStart;

	//semua tombol untuk animasi
	public GameObject[] allButton;
	public GameObject[] allButtonHelp;
	public RectTransform[] allButtonPosition;

	public Image loadButton;

    private bool onHelp;
    private bool onExit;
	private bool readyPress;

	//suara
	private AudioSource buttonSound;
	public AudioSource backGroundMusic;

	//iklan
	private PlayerAd advertisment;

	void Start(){
		if (PlayerPrefs.GetFloat("gameBaru") <= 0){
			loadButton.color = new Color(loadButton.color.r,loadButton.color.g,loadButton.color.b,0.2f);
		}
		buttonSound = GetComponent<AudioSource>();
		StartCoroutine(AnimationButton());
		advertisment = GetComponent<PlayerAd>();
		buttonHelpBack.onClick.AddListener(()=> GameHelp());
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			onExit = true;
		}
	}

	public void GameStart(){
		if(readyPress){
			if (PlayerPrefs.GetFloat("gameBaru") <= 0){
				GameNew();
			}else{
				//onHelp = false;
				//onExit = false;
				for(int a=0;a<allButton.Length;a++){
					allButton[a].SetActive(false);
				}
				buttonYesNoStart.SetActive(true);
			}
		}
	}

    public void GameStartYes() {
        GameNew();
    }

    public void GameStartnO() {
		buttonYesNoStart.SetActive(false);
		for(int a=0;a<allButton.Length;a++){
			allButton[a].SetActive(true);
		}
    }

	private void HideButton(){
		for(int a=0;a<allButton.Length;a++){
			allButton[a].SetActive(false);
		}
	}

	private void ShowButton(){
		for(int a=0;a<allButton.Length;a++){
			allButton[a].SetActive(true);
		}
	}

    private void GameNew() {
		Game.game = new Game();
		Game.game.Health = 5;
		Game.game.DoorPass = 0;
		Game.game.LevelEnter = 0;
		Game.game.Damage = 1;
		Game.game.Gold = 1000000;
		Game.game.healthUpgrade = 0;
		Game.game.delayUpgrade = 0;
		Game.game.speedUpgrade = 0;
		Game.game.damageUpgrade = 0;
		SaveLoad.Save();
		PlayerPrefs.SetFloat("gameBaru", 1);
		SceneManager.LoadScene("LoadingCharacter");
    }

	public void GameLoad(){
		if(readyPress){
			if (PlayerPrefs.GetFloat("gameBaru") > 0){
				loadButton.color = new Color(loadButton.color.r,loadButton.color.g,loadButton.color.b,1f);
				SaveLoad.Load();
				Game.game = SaveLoad.saveGame;
				SceneManager.LoadScene("LoadingCharacter");
			}
		}
	}

	public void GameHelp(){
		if(readyPress){
			onHelp = !onHelp;
			if (!onHelp){
				ShowButton();
				buttonHelp.SetActive(false);
			}else{
				HideButton();
				StartCoroutine(AnimationHelp());
				buttonHelp.SetActive(true);
			}
		}
    }

	public void GameExit(){
		if(readyPress){
			advertisment.ShowAd();
			onExit = !onExit;
			onHelp = false;
			if (onExit){
				HideButton();
				buttonYesNo.SetActive(true);
			}else{
				ShowButton();
				buttonYesNo.SetActive(false);
			}
		}
    }

    public void GameYes() {
        Application.Quit();
    }

    public void GameNo() {
        onExit = false;
		ShowButton();
		buttonYesNo.SetActive(false);
    }

	private IEnumerator AnimationButton(){
		float scale1 = 5f;
		for(int a=0;a<allButton.Length;a++){
			allButton[a].gameObject.SetActive(true);
			while(scale1 > 1){
				scale1 -= 0.1f;
				allButton[a].transform.localScale = new Vector3(scale1,scale1,scale1);
				yield return null;
			}
			allButton[a].transform.localScale = new Vector3(1f,1f,1f);
			buttonSound.Play();
			scale1 = 5f;
		}
		readyPress = true;
		if(!backGroundMusic.isPlaying){
			backGroundMusic.Play();
		}
	}

	private IEnumerator AnimationHelp(){
		while(allButtonHelp[0].transform.position != allButtonPosition[0].position){
			for(int a=0;a<allButtonHelp.Length;a++){
				allButtonHelp[a].transform.position = Vector3.MoveTowards(allButtonHelp[a].transform.position,allButtonPosition[a].position,5f);
			}
			yield return null;
		}
	}
}
