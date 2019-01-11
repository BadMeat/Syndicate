using UnityEngine;
using System.Collections;

public class BossPosition : MonoBehaviour {

	public static BossPosition bossPosition;

	//posisi bos keluar
	public float boosAppearPosotion;

	//kamera naik ke atas
	public float cameraMoveUpPosition;

	//kecepatan naik
	public float speed;

	//batas kamera bawah
	public float minY;
	public GameObject lastBoss;

	private bool readyToCount;
	private bool stopCamera;
	private Camera mainCam;

	//suara saat bos muncul dan menang
	public AudioSource bossAppear;
	private float increaseSound;
	private bool defeated;

	//teks saat boss muncul
	public GameObject lastBostWord;


	//backsound hilang
	public AudioSource backSound;

	void Awake(){
		if(bossPosition == null){
			bossPosition = this;
		}
		else if(bossPosition != null){
			Destroy(gameObject);
		}
	}

	void Start(){
		mainCam = Camera.main;
		bossAppear.volume = increaseSound;;
	}

	void Update(){
		if(readyToCount){
			StartCoroutine(BorderMove());
			bossAppear.volume = increaseSound;

			if(mainCam.fieldOfView < 120){
				mainCam.fieldOfView += 0.2f;
			}

			if(defeated){
				StartCoroutine(DecreaseVolume());
				StartCoroutine(GameMenu.gm.EndScreen());
			}else{
				StartCoroutine(IncreaseVolume());
			}
		}
	}

	private IEnumerator IncreaseVolume(){
		while(increaseSound < 1){
			increaseSound += 0.05f * Time.deltaTime;
			yield return null;
		}
	}

	private IEnumerator DecreaseVolume(){
		while(increaseSound > 0){
			increaseSound -= 0.01f * Time.deltaTime;
			yield return null;
		}

	}

	public void SetDefeated(bool defeat){
		defeated = defeat;
	}

	private void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player" && !readyToCount){
			lastBoss.SetActive(true);
			bossAppear.Play();
			readyToCount = true;
			lastBostWord.SetActive(true);
			StartCoroutine(DecreaseBackSound());
		}
	}

	//kamera menuju batas bawah
	private IEnumerator BorderMove(){
		Controller.controller.GetComponent<FollowingCamera>().enabled = false;
		while(mainCam.transform.position.y < minY){
			mainCam.transform.Translate(Vector3.up * speed * Time.deltaTime);
			yield return null;
		}
		Controller.controller.GetComponent<FollowingCamera>().minY = minY;
		Controller.controller.GetComponent<FollowingCamera>().enabled = true;
	}

	private IEnumerator DecreaseBackSound(){
		float volumeTime = 1f;
		while(volumeTime > 0){
			volumeTime -= 0.1f * Time.deltaTime;
			backSound.volume = volumeTime;
			yield return null;
		}
		backSound.gameObject.SetActive(false);
	}
}
