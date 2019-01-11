using UnityEngine;
using System.Collections;

public class BoxoxMen : MonoBehaviour {

	public GameObject target;

	private Rigidbody2D body;
	private bool onWall;
	private bool onGround;

	public enum Condition{
		stop,
		follow,
		jump
	}

	public Condition condition;

	public LayerMask wallLayer;
	public float wallRadius;
	public Transform wallDetection;

	public LayerMask groundLayer;
	public float groundRadius;
	public Transform groundDetection;

	private float startXScale;

	private float rangeToPlayer;

	private Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
		body = GetComponent<Rigidbody2D>();
		startXScale = transform.localScale.x;
	}

	void FixedUpdate(){
		rangeToPlayer = Vector3.Distance(transform.position,target.transform.position);
		onGround = Physics2D.OverlapCircle(groundDetection.position,groundRadius,groundLayer);
		onWall = Physics2D.OverlapCircle(wallDetection.position,wallRadius,wallLayer);
		switch(condition){
		case(Condition.stop):
			//body.velocity = Vector2.zero;
			if(rangeToPlayer < 10f){
				condition = Condition.follow;
			}
			break;
		case(Condition.follow):
			if(transform.position.x < target.transform.position.x - 0.2f){
				transform.localScale = new Vector3(startXScale,transform.localScale.y,transform.localScale.z);
				body.velocity = new Vector2(2f,body.velocity.y);
			}
			else if(transform.position.x > target.transform.position.x + 0.2f){
				transform.localScale = new Vector3(-startXScale,transform.localScale.y,transform.localScale.z);
				body.velocity = new Vector2(-2f,body.velocity.y);
			}
			if(onWall){
				condition = Condition.jump;
			}
			break;
		case(Condition.jump):
			if(onGround){
				if(body.velocity.x > 0){
					body.velocity = new Vector2(4f,10f);
				}else{
					body.velocity = new Vector2(-4f,10f);
				}
			}
			break;
		}

		anim.SetFloat("speed",Mathf.Abs(body.velocity.x));
		anim.SetBool("onGround",onGround);

		if(rangeToPlayer > 10f){
			condition = Condition.stop;
		}
	}

	private void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere(groundDetection.position,groundRadius);
		Gizmos.DrawWireSphere(wallDetection.position,wallRadius);
	}

	private void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Ground"){
			condition = Condition.follow;
		}
	}
}
