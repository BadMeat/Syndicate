using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public EnemyBase enemyBaseState;
	public float unitMove;
	public float rangeUp = 7f;

	//bagian badan
	public GameObject deadEnemy;
	public GameObject[] bodyPart;
	public GameObject explotionEffect;

	//all sound
	public AudioClip gunFire;
	private AudioSource soundPlay;

	private Rigidbody2D body;
	private bool onLeft;
	private float rangePlayer;
	private float time;
	private float startPos;
	private float startScale;
	private Animator anim;
	private bool onHit;


	//senjata
	public Transform handGun;

	//sembunyi bos tank
	private bool onHide;

	//menghindari peluru
	public bool menghindar;
	public float radius;
	public LayerMask layer;
	private bool onBullet;

	//deteksi tanah
	private bool onGround;
	public float groundRadius;
	public LayerMask groundLayer;
	public Transform groundDeteck;

	//darah
	private float maxHealth;
	public Image healthBar;
	public Image healthBorder;
	private float healthScale;

	void Start () {
		healthScale = healthBorder.transform.localScale.x;
		maxHealth = enemyBaseState.health;
		body = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		unitMove += transform.position.x;
		startPos = transform.position.x;
		startScale = transform.localScale.x;
		soundPlay = GetComponent<AudioSource>();

		if(Game.game.StartedGame){
			for(int a=0;a<GameManager.gm.enemyDie.Count;a++){
				if(gameObject.name == GameManager.gm.enemyDie[a]){
					Destroy(gameObject);
				}
			}
		}
	}

	void FixedUpdate(){
		onBullet = Physics2D.OverlapCircle(transform.position,radius,layer);
		onGround = Physics2D.OverlapCircle(groundDeteck.position,groundRadius,groundLayer);
	}

	void Update () {
		if(Controller.controller.OnDie() || GameMenu.gm.WinState){
			anim.SetFloat("speed",0f);
			anim.SetBool("onAttack",false);
			body.velocity = Vector2.zero;
		}
		else if(onBullet && onGround && menghindar){
			body.velocity = new Vector2(body.velocity.x,20f);
		}
		else if(onGround){
			rangePlayer = Vector3.Distance(transform.position,Controller.controller.transform.position);
			anim.SetFloat("speed",Mathf.Abs(body.velocity.x));
			FlipPlayer();
			if(enemyBaseState.type == EnemyBase.Type.biasa){
				if(rangePlayer <= enemyBaseState.rangeActive){
					body.velocity = Vector2.zero;
					if(Controller.controller.transform.position.y <= transform.position.y + rangeUp && Controller.controller.transform.position.y >= transform.position.y - rangeUp){
						FacingPlayer();
						anim.SetBool("onAttack",true);
						NormalShoot();
					}
				}else{
					Patrol();
					anim.SetBool("onAttack",false);
				}
			}
			else if(enemyBaseState.type == EnemyBase.Type.waiting){
				if(rangePlayer <= enemyBaseState.rangeActive){
					body.velocity = Vector2.zero;
					if(Controller.controller.transform.position.y <= transform.position.y + rangeUp && Controller.controller.transform.position.y >= transform.position.y - rangeUp){
						FacingPlayer();
						anim.SetBool("onAttack",true);
						NormalShoot();
					}
				}else{
					anim.SetBool("onAttack",false);
				}
			}

			else if(enemyBaseState.type == EnemyBase.Type.brem){
				if(rangePlayer <= enemyBaseState.rangeActive){
					body.velocity = Vector2.zero;
					if(Controller.controller.transform.position.y <= transform.position.y + rangeUp && Controller.controller.transform.position.y >= transform.position.y - rangeUp){
						anim.SetBool("onAttack",true);
						FacingPlayer();
						body.velocity = Vector2.zero;
						if(Time.timeSinceLevelLoad >= time){
							StartCoroutine(MachineGunAttack());
							time = Time.timeSinceLevelLoad + enemyBaseState.delay;
						}
					}
				}else{
					Patrol();
					anim.SetBool("onAttack",false);
				}
			}

			else if(enemyBaseState.type == EnemyBase.Type.waitingBrem){
				if(rangePlayer <= enemyBaseState.rangeActive){
					body.velocity = Vector2.zero;
					if(Controller.controller.transform.position.y <= transform.position.y + rangeUp && Controller.controller.transform.position.y >= transform.position.y - rangeUp){
						anim.SetBool("onAttack",true);
						FacingPlayer();
						body.velocity = Vector2.zero;
						if(Time.timeSinceLevelLoad >= time){
							StartCoroutine(MachineGunAttack());
							time = Time.timeSinceLevelLoad + enemyBaseState.delay;
						}
					}
				}else{
					anim.SetBool("onAttack",false);
				}
			}
			else if(enemyBaseState.type == EnemyBase.Type.boss){
				if(GameMenu.gm.GetHealth <=0){
					gameObject.GetComponent<CircleCollider2D>().enabled = false;
				}
				if(rangePlayer > enemyBaseState.rangeActive){
					if(Controller.controller.transform.position.x > transform.position.x){
						body.velocity = new Vector2(enemyBaseState.speed,body.velocity.y);
					}
					else if(Controller.controller.transform.position.x < transform.position.x){
						body.velocity = new Vector2(-enemyBaseState.speed,body.velocity.y);
					}
				}else{
					anim.SetBool("onAttack",true);
					FacingPlayer();
					body.velocity = Vector2.zero;
					if(Time.timeSinceLevelLoad >= time){
						NormalShoot();
						time = Time.timeSinceLevelLoad + enemyBaseState.delay;
					}

					if(!onHide){
						StartCoroutine(Hiding());
					}
				}
			}
		}
	}
		
	private IEnumerator Hiding(){
		onHide = true;
		yield return new WaitForSeconds(0.2f);
		anim.SetBool("onHide",true);
		gameObject.tag = "Wall";
		yield return new WaitForSeconds(2f);
		anim.SetBool("onHide",false);
		gameObject.tag = "Hurt";
		onHide = false;
	}

	private void Patrol(){
		if(!onLeft){
			body.velocity = new Vector2(enemyBaseState.speed, body.velocity.y);
		}else{
			body.velocity = new Vector2(-enemyBaseState.speed, body.velocity.y);
		}

		if(transform.position.x >= unitMove){
			onLeft = true;
		}
		else if(transform.position.x <= startPos){
			onLeft = false;
		}
		anim.SetBool("onAttack",false);
	}

	private void FlipPlayer(){
		if(body.velocity.x > 0){
			transform.localScale = new Vector3(startScale,transform.localScale.y,transform.localScale.z);
		}
		else if(body.velocity.x < 0){
			transform.localScale = new Vector3(-startScale,transform.localScale.y,transform.localScale.z);
		}
	}

	private void NormalShoot(){
		if(Time.timeSinceLevelLoad >= time){
			soundPlay.PlayOneShot(gunFire);
			GameObject instant = (GameObject) Instantiate(enemyBaseState.bullet,enemyBaseState.bulletOut.position,handGun.rotation);
			if(transform.localScale.x < 0){
				instant.transform.eulerAngles = new Vector3(instant.transform.eulerAngles.x,instant.transform.eulerAngles.y,-instant.transform.eulerAngles.z);
			}
			else if(transform.localScale.x > 0){
				instant.transform.localScale = new Vector3(-instant.transform.localScale.x,instant.transform.localScale.y,instant.transform.localScale.z);
			}
			time = Time.timeSinceLevelLoad + enemyBaseState.delay;
		}
	}
		
	//nembak machinegun
	private IEnumerator MachineGunAttack(){
		soundPlay.PlayOneShot(gunFire);
		for(int a=0;a<3;a++){
			GameObject instant = (GameObject) Instantiate(enemyBaseState.bullet,enemyBaseState.bulletOut.position,handGun.rotation);
			if(transform.localScale.x < 0){
				instant.transform.eulerAngles = new Vector3(instant.transform.eulerAngles.x,instant.transform.eulerAngles.y,-instant.transform.eulerAngles.z);
			}
			else if(transform.localScale.x > 0){
				instant.transform.localScale = new Vector3(-instant.transform.localScale.x,instant.transform.localScale.y,instant.transform.localScale.z);
			}
			yield return new WaitForSeconds(0.1f);
		}
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
		Gizmos.DrawWireSphere(transform.position,enemyBaseState.rangeActive);
		Gizmos.DrawWireSphere(transform.position,radius);
		Gizmos.DrawWireSphere(groundDeteck.position,groundRadius);
	}

	IEnumerator Blink(){
		if(onHit){
			yield break;
		}
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

	public void GetHitted(float damage){
		enemyBaseState.health -= damage;
		float calctHealth;
		calctHealth = enemyBaseState.health / maxHealth;
		healthBar.transform.localScale = new Vector3(Mathf.Clamp(calctHealth,0f,1),healthBar.transform.localScale.y,healthBar.transform.localScale.z);
		if(transform.localScale.x > 0){
			healthBorder.transform.localScale = new Vector3(healthScale,healthBorder.transform.localScale.y,healthBorder.transform.localScale.z);
		}
		else if(transform.localScale.x < 0){
			healthBorder.transform.localScale = new Vector3(-healthScale,healthBorder.transform.localScale.y,healthBorder.transform.localScale.z);
		}

		if(enemyBaseState.health <=0){
			GameManager.gm.EnemyDie(gameObject);
			GameManager.gm.AllEnemy -= 1;
			if(enemyBaseState.type == EnemyBase.Type.boss){
				BossPosition.bossPosition.SetDefeated(true);
			}
			Instantiate(explotionEffect,transform.position,Quaternion.identity);
			GameObject go = (GameObject) Instantiate(deadEnemy,transform.position,Quaternion.identity);
			go.transform.localScale = transform.localScale;
			Destroy(gameObject);
		}else{
			StartCoroutine(Blink());
		}
	}
}
