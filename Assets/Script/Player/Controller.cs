using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public static Controller controller;

	public float speed;
	public float jumpHeight;
	public GameObject bullet;
	public Transform bulletOut;
	public float delay;
	public GameObject[] allPart;
	public float ketinggianMati;
	public GameObject deadPlayer;

	private Rigidbody2D body;
	private float currentXScale;
	private float time;
    private bool moveLeft;
    private bool moveRight;
    private bool onDie;
	private bool onBlink;
	private bool invisible;

	[System.Serializable]
	public class DeteckGround{
		public float radius;
		public LayerMask layer;
		public Transform pos;
	}

	public DeteckGround dg;
	private bool onGround;
	private Animator anim;

	//change delay gun
	private float startDelay;
	private float timeMachine;

	//semua suara
	private AudioSource soundPlay;
	public AudioClip gunFire;
	public AudioClip machineFire;

	//saat ngilang
	private CircleCollider2D circle;
	private BoxCollider2D box;

	void Awake(){
		if(controller == null){
			controller = this;
		}
		else if(controller != null){
			Destroy(gameObject);
		}
	}

	void Start(){
		circle = GetComponent<CircleCollider2D>();
		box = GetComponent<BoxCollider2D>();
		anim = GetComponent<Animator>();
		body = GetComponent<Rigidbody2D>();
		soundPlay = GetComponent<AudioSource>();
		currentXScale = transform.localScale.x;
		delay -= Game.game.delayUpgrade;
		speed = Game.game.Speed + Game.game.speedUpgrade;
		if(delay <=0){
			delay = 0f;
		}
		startDelay = delay;
	}

	void FixedUpdate(){
		onGround = Physics2D.OverlapCircle(dg.pos.position,dg.radius,dg.layer);
	}

	void Update () {
		Move();
		HandleAnimation();
		if(transform.position.y <= ketinggianMati){
			onDie = true;
			GameMenu.gm.SetHealth(GameMenu.gm.GetHealth);
		}
		//body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed,body.velocity.y);
	}

	private void FlipPlayer(){
		if(body.velocity.x > 0){
			transform.localScale = new Vector3(currentXScale,transform.localScale.y,transform.localScale.z);
		}
		if(body.velocity.x <0){
			transform.localScale = new Vector3(-currentXScale,transform.localScale.y,transform.localScale.z);
		}
	}

	public void Dead(){
		if(GameMenu.gm.GetHealth <=0 || onDie){
			GameObject instant = (GameObject) Instantiate(deadPlayer,transform.position,Quaternion.identity);
			instant.transform.localScale = transform.localScale;
			onDie = true;
			gameObject.SetActive(false);
		}
	}


	public void Fire(){
		if(Time.timeSinceLevelLoad >= time){
			if(timeMachine > 0){
				GameMenu.gm.SetBonusBulletOn();
				timeMachine -=1f;
			}else{
				GameMenu.gm.SetBonusBulletOff();
				delay = startDelay;
			}
			GameObject bull = (GameObject) Instantiate(bullet,bulletOut.position,Quaternion.identity);
			bull.GetComponent<Bullet>().Damage = Game.game.Damage;
			if(transform.localScale.x < 0){
				bull.transform.localScale = new Vector3(-bull.transform.localScale.x,bull.transform.localScale.y,bull.transform.localScale.z);
			}
			time = Time.timeSinceLevelLoad + delay;
			StartCoroutine("Attacking");
		}
	}

	private void HandleAnimation(){
		anim.SetBool("onGround",onGround);
		anim.SetFloat("speed",Mathf.Abs(body.velocity.x));
	}

	private void Move(){
		FlipPlayer();
		if (moveLeft){
            body.velocity = new Vector2(-speed, body.velocity.y);
        }
        else if (moveRight){
            body.velocity = new Vector2(speed, body.velocity.y);
        }else {
            body.velocity = new Vector2(0f,body.velocity.y);
        }
	}

	public void MoveRight(){
        moveLeft = false;
        moveRight = true;
	}

	public void MoveLeft(){
        moveLeft = true;
        moveRight = false;
	}

	public void Jump(){
		if(onGround){
			body.velocity = new Vector2(body.velocity.x,jumpHeight);
		}
	}

	public void MoveStop(){
        moveLeft = false;
        moveRight = false;
    }

	private void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere(dg.pos.position,dg.radius);
	}

	private IEnumerator Attacking(){
		if(timeMachine > 0){
			soundPlay.PlayOneShot(machineFire);
		}else{
			soundPlay.PlayOneShot(gunFire);
		}
		anim.SetBool("onAttack",true);
		yield return new WaitForSeconds(0.2f);
		anim.SetBool("onAttack",false);
	}

    private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Hurt"){
			GameMenu.gm.SetHealth(1);
		}

		if(other.tag == "Gun"){
			GameMenu.gm.SetBonusBulletOn();
			delay = 0f;
		}
    }

    public bool OnDie() {
        return onDie;
    }

	public IEnumerator Blink(){
		onBlink = true;
		invisible = true;
		for(int b=0;b<3;b++){
			for(int a=0;a<allPart.Length;a++){
				allPart[a].SetActive(false);
			}
			yield return new WaitForSeconds(0.2f);
			for(int a=0;a<allPart.Length;a++){
				allPart[a].SetActive(true);
			}
			yield return new WaitForSeconds(0.2f);
		}
		onBlink = false;
		invisible = false;
	}

	public bool GetBlink(){
		return onBlink;
	}

	public float TimeMachine{
		get{return timeMachine;}
		set{timeMachine = value;}
	}

	public bool Invisible{
		get{return invisible;}
	}

	public void HidePlayer(){
		for(int a=0;a<allPart.Length;a++){
			allPart[a].SetActive(false);
		}
		circle.enabled = false;
		box.enabled = false;
	}

	public void ShowPlayer(){
		for(int a=0;a<allPart.Length;a++){
			allPart[a].SetActive(true);
		}
		circle.enabled = true;
		box.enabled = true;
	}

	public float SetDelay{
		set{startDelay = value;}
	}
}
