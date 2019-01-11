using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameMenu : MonoBehaviour {

	public static GameMenu gm;

    public Image gameMenu;
    public GameObject yesNoButton;
    public GameObject goScreen;
	// screen saat menang
	public GameObject winScreen;
	public Text winText;

	//screen saat tamat
	public Text endText;

	//darah
	public Image healthImage;
	private float healthPlayer;

	// Bonus senjata machine gun
	public Text timeMachineText;
	public Image machineGunImage;

	//event trigger untuk tombol player
	public EventTrigger leftButton;
	public EventTrigger rightButton;
	public EventTrigger jumpButton;
	public EventTrigger shootButton;
	public GameObject[] allCharacter;
	public Transform startPosition;
	public BorderCamera borderCamera;

	//sembunyikan tombol
	public GameObject[] allButton;

    private float[] health;
    private bool onMenu;
	private float timeColor;

	//pas masuk pintu masih ada musuh, musuh tidak akan bisa menyerang
	private bool onWin;

	//winning sound;
	private AudioSource winningGame;

	//kepala karakter
	public Image characterImage;
	public Sprite[] allCharacterImage;

	void Awake(){
		if(gm == null){
			gm = this;
		}
		else if(gm != null){
			Destroy(gameObject);
		}
	}

    void Start() {
		if(!Game.game.StartedGame){
			GameManager.gm.CountEnemy();
		}
			
		healthPlayer = Game.game.Health + Game.game.healthUpgrade;
		if(Game.game.StartedGame){
			SetPlayer(GameManager.gm.SavePosition);
		}else{
			SetPlayer(startPosition.position);
		}
		healthImage.transform.localScale = new Vector3(Mathf.Clamp(healthPlayer,0,1),healthImage.transform.localScale.y,healthImage.transform.localScale.z);
		winningGame = GetComponent<AudioSource>();
		SetBonusBulletOff();
    }

	private void SetPlayer(Vector3 spawnPos){
		for(int a=0;a<allCharacter.Length;a++){
			if(Game.game.CharacterSelected == a){
				characterImage.sprite = allCharacterImage[a];
				GameObject playerObject = (GameObject)Instantiate(allCharacter[a],spawnPos,Quaternion.identity);

				EventTrigger.Entry entryLeft = new EventTrigger.Entry();
				entryLeft.eventID = EventTriggerType.PointerDown;
				entryLeft.callback.AddListener((eventData) => {playerObject.GetComponent<Controller>().MoveLeft();});
				leftButton.triggers.Add(entryLeft);

				EventTrigger.Entry entryRight = new EventTrigger.Entry();
				entryRight.eventID = EventTriggerType.PointerDown;
				entryRight.callback.AddListener((eventData) => {playerObject.GetComponent<Controller>().MoveRight();});
				rightButton.triggers.Add(entryRight);

				EventTrigger.Entry entryJump = new EventTrigger.Entry();
				entryJump.eventID = EventTriggerType.PointerDown;
				entryJump.callback.AddListener((eventData) => {playerObject.GetComponent<Controller>().Jump();});
				jumpButton.triggers.Add(entryJump);

				EventTrigger.Entry entryShoot = new EventTrigger.Entry();
				entryShoot.eventID = EventTriggerType.PointerDown;
				entryShoot.callback.AddListener((eventData) => {playerObject.GetComponent<Controller>().Fire();});
				shootButton.triggers.Add(entryShoot);

				EventTrigger.Entry entryLeftUp = new EventTrigger.Entry();
				entryLeftUp.eventID = EventTriggerType.PointerUp;
				entryLeftUp.callback.AddListener((eventData) => {playerObject.GetComponent<Controller>().MoveStop();});
				leftButton.triggers.Add(entryLeftUp);

				EventTrigger.Entry entryRighttUp = new EventTrigger.Entry();
				entryRighttUp.eventID = EventTriggerType.PointerUp;
				entryRighttUp.callback.AddListener((eventData) => {playerObject.GetComponent<Controller>().MoveStop();});
				rightButton.triggers.Add(entryRighttUp);

				playerObject.GetComponent<FollowingCamera>().minX = borderCamera.minX;
				playerObject.GetComponent<FollowingCamera>().maxX = borderCamera.maxX;
				playerObject.GetComponent<FollowingCamera>().minY = borderCamera.minY;
				playerObject.GetComponent<FollowingCamera>().maxY = borderCamera.maxY;
			}
		}
	}

    void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)){
			onMenu = true;
			Time.timeScale = 0f;
			gameMenu.gameObject.SetActive(true);
		}
    }

	public void SetBonusBulletOn(){
		timeMachineText.text = ""+(int)(Controller.controller.TimeMachine);
		machineGunImage.gameObject.SetActive(true);
	}

	public void SetBonusBulletOff(){
		timeMachineText.text = "";
		machineGunImage.gameObject.SetActive(false);
	}
		
    public void MenuPause() {
        onMenu = !onMenu;
		if (onMenu) {
			Time.timeScale = 0f;
			gameMenu.gameObject.SetActive(true);
		} else {
			Time.timeScale = 1f;
			gameMenu.gameObject.SetActive(false);
		}
    }

    public void Resume() {
		Time.timeScale = 1f;
		onMenu = false;
		gameMenu.gameObject.SetActive(false);
    }

    public void Exit() {
		yesNoButton.SetActive(true);
		gameMenu.gameObject.SetActive(false);
    }

    public void ExitYes() {
		Time.timeScale = 1f;
		Game.game.StartedGame = false;
        SceneManager.LoadScene("LoadingBack");
    }

    public void ExitNo() {
		yesNoButton.SetActive(false);
		Time.timeScale = 1f;
		onMenu = false;
    }

	public float GetHealth{
		get{return healthPlayer;} 
	}

	public void SetHealth(float damage) {
        healthPlayer -= damage;
		if(!Controller.controller.GetBlink()){
			StartCoroutine(Controller.controller.Blink());
		}
		Controller.controller.Dead();
		if (healthPlayer <= 0) {
			goScreen.SetActive(true);
		}
		float health;
		health = healthPlayer/ (Game.game.Health + Game.game.healthUpgrade);
		healthImage.transform.localScale = new Vector3(Mathf.Clamp(health,0,1),healthImage.transform.localScale.y,healthImage.transform.localScale.z);
    }

    public void CobaLagi() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	public IEnumerator WinScreen(){
		onWin = true;
		winScreen.SetActive(true);
		Controller.controller.enabled = false;
		Controller.controller.MoveStop();
		Controller.controller.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		while(IncreaseNumber() < 1){
			yield return null;
		}
		Game.game.StartedGame = false;
		SceneManager.LoadScene("LoadingMap");
	}

	public IEnumerator EndScreen(){
		if(!winningGame.isPlaying){
			winningGame.Play();
		}

		endText.gameObject.SetActive(true);
		float timeEnd = 0f;
		while (timeEnd < 1){
			timeEnd += 0.1f * Time.deltaTime;
			endText.color = new Color(endText.color.r,endText.color.g,endText.color.b,timeEnd);
			yield return null;
		}
		Game.game.StartedGame = false;
		SceneManager.LoadScene("TAMAT");
	}

	private float IncreaseNumber(){
		timeColor += 0.5f * Time.deltaTime;
		winText.color = new Color(winText.color.r,winText.color.g,winText.color.b,timeColor);
		return timeColor;
	}

	void OnApplicationPause(bool onPause){
		if(onPause){
			onMenu = true;
		}
	}

	public void HideButton(){
		for(int a=0;a<allButton.Length;a++){
			allButton[a].SetActive(false);
		}
	}

	public void ShowButton(){
		for(int a=0;a<allButton.Length;a++){
			allButton[a].SetActive(true);
		}
	}

	public bool WinState{
		get{
			return onWin;
		}
	}
}
