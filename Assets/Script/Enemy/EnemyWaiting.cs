using UnityEngine;
using System.Collections;

public class EnemyWaiting : MonoBehaviour {

	public EnemyBase enemy;

	private float rangToPlayer;
	private float time;
	private float startScale;

	//untuk blink ketika diserang
	private bool onHit;
	public GameObject[] bodyPart;

	//efek ketika mati
	public GameObject explotionEffect;
	public GameObject deadEnemy;

	//animasi
	private Animator anim;
	public float waktuSembunyi = 1f;

	//suara nyerang
	private AudioSource gunShoot; 

	void Start () {
		if(Game.game.StartedGame){
			for(int a=0;a<GameManager.gm.enemyDie.Count;a++){
				if(gameObject.name == GameManager.gm.enemyDie[a]){
					Destroy(gameObject);
				}
			}
		}
		anim = GetComponent<Animator>();
		startScale = transform.localScale.x;
		gunShoot = GetComponent<AudioSource>();
	}

	void Update () {
		if(Controller.controller.OnDie() || GameMenu.gm.WinState){
			anim.SetBool("onAttack",false);
			anim.SetBool("onHide",false);
		}else{
			rangToPlayer = Vector3.Distance(Controller.controller.transform.position,transform.position);
			if(rangToPlayer <= enemy.rangeActive && transform.position.y >= Controller.controller.transform.position.y - 5f && transform.position.y <= Controller.controller.transform.position.y + 5f){
				FacingPlayer();
				if(Time.timeSinceLevelLoad >= time){
					StartCoroutine(Attacking());
					time = Time.timeSinceLevelLoad + enemy.delay;
				}
			}
		}
	}

	private IEnumerator Attacking(){
		anim.SetBool("onHide",false);
		yield return new WaitForSeconds(0.2f);
		anim.SetBool("onAttack",true);
		yield return new WaitForSeconds(0.2f);
		GameObject inst = (GameObject) Instantiate(enemy.bullet,enemy.bulletOut.position,Quaternion.identity);
		if(transform.localScale.x < 0){
			inst.transform.localScale = new Vector3(-inst.transform.localScale.x,inst.transform.localScale.y,inst.transform.localScale.z);
		}
		if(!gunShoot.isPlaying){
			gunShoot.Play();
		}
		anim.SetBool("onHide",true);
		anim.SetBool("onAttack",false);
		yield return new WaitForSeconds(waktuSembunyi);
	}

	private void FacingPlayer(){
		if(Controller.controller.transform.position.x < transform.position.x){
			transform.localScale = new Vector3(-startScale,transform.localScale.y,transform.localScale.z);
		}
		else if(Controller.controller.transform.position.x > transform.position.x){
			transform.localScale = new Vector3(startScale,transform.localScale.y,transform.localScale.z);
		}
	}

	private void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere(transform.position,enemy.rangeActive);
	}

	IEnumerator Blink(){
		onHit = true;
		for(int a=0;a<4;a++){
			for(int b=0;b<bodyPart.Length;b++){
				bodyPart[b].SetActive(false);
			}
			yield return new WaitForSeconds(0.2f);
			for(int c=0;c<bodyPart.Length;c++){
				bodyPart[c].SetActive(true);
			}
			yield return new WaitForSeconds(0.2f);
		}
		onHit = false;
	}

	private void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Bullet"){
			enemy.health -= 1f;
			if(enemy.health <=0){
				GameManager.gm.EnemyDie(gameObject);
				GameManager.gm.AllEnemy -= 1;
				Instantiate(explotionEffect,transform.position,Quaternion.identity);
				GameObject go = (GameObject) Instantiate(deadEnemy,transform.position,Quaternion.identity);
				go.transform.localScale = transform.localScale;
				Destroy(gameObject);
			}else{
				if(!onHit){
					StartCoroutine(Blink());
				}
			}
		}
	}

	private IEnumerator Destroyed(){
		while(gunShoot.isPlaying){
			for(int b=0;b<bodyPart.Length;b++){
				bodyPart[b].SetActive(false);
			}
			yield return null;
		}
		GameObject go = (GameObject) Instantiate(deadEnemy,transform.position,Quaternion.identity);
		go.transform.localScale = transform.localScale;
		Destroy(gameObject);
	}
}
