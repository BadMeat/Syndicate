using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour{
	public float speed = 10f;
	public float damage = 1f;
	public float maxHealth = 10f;
	protected float health;

	protected bool onGround;
	public LayerMask groundLayer;
	public Transform groundPos;
	public float groundRadius;

	protected Rigidbody2D body;
	protected float StartPos;

	protected float rangeToPlayer;
	public float rangeActive;
	protected bool onBack;
	protected Vector3 startScale;

	//animasi
	protected Animator anim;

	public void BaseStart(){
		StartPos = transform.position.x;
		body = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		startScale = transform.localScale;
	}

	public BaseEnemy(){
		health = maxHealth;
	}

	public void GetHitted(float damage){
		health -= damage;
		if(health <=0){
			anim.SetBool("onDie",true);
			health = 0f;
		}
	}

	public bool DeteckGround(){
		onGround = Physics2D.OverlapCircle(groundPos.position,groundRadius,groundLayer);
		return onGround;
	}

	public IEnumerator BackPosition(){
		while(transform.position.x > StartPos){
			body.velocity = new Vector2(-speed,body.velocity.y);
			yield return null;
		}

		while(transform.position.x < StartPos){
			body.velocity = new Vector2(speed,body.velocity.y);	
			yield return null;
		}
		body.velocity = Vector2.zero;
		onBack = false;
	}
		
	private void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere(groundPos.position,groundRadius);
		Gizmos.DrawWireSphere(transform.position,rangeActive);
	}

	protected void SetAnimation(){
		anim.SetFloat("speed",Mathf.Abs(body.velocity.x));
		anim.SetBool("onGround",onGround);
	}

	protected void FlipPlayer(){
		if(body.velocity.x > 0){
			transform.localScale = new Vector3(startScale.x,transform.localScale.y,transform.localScale.z);
		}
		else if(body.velocity.x < 0){
			transform.localScale = new Vector3(-startScale.x,transform.localScale.y,transform.localScale.z);			
		}
	}
}
