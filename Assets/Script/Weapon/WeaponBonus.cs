using UnityEngine;
using System.Collections;

public class WeaponBonus : MonoBehaviour {
	public float isiPeluru;
	private AudioSource pickSound;
	private SpriteRenderer spriteRender;
	private bool alreadyPicked;

	void Start(){
		pickSound = GetComponent<AudioSource>();
		spriteRender = GetComponent<SpriteRenderer>();
	}

	private void OnTriggerEnter2D(Collider2D other){
		if(other.GetComponent<Controller>() != null && !alreadyPicked){
			other.GetComponent<Controller>().TimeMachine +=isiPeluru;
			pickSound.Play();
			StartCoroutine(Destroyed());
		}
	}

	IEnumerator Destroyed(){
		if(alreadyPicked){
			yield break;
		}
		alreadyPicked = true;
		while(pickSound.isPlaying){
			spriteRender.color = new Color(0f,0f,0f,0f);
			yield return null;
		}
		Destroy(gameObject);
	}
}