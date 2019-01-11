using UnityEngine;
using System.Collections;

public class KnifeEnemy : NormalEnemy {

	public GameObject player;
	public enum Types{
		patrol,
		waiting
	}

	public Types type;

	void Start () {
		BaseStart();
	}

	void Update () {
		if(player == null){
			player = GameObject.FindGameObjectWithTag("Player");
		}
		rangeToPlayer = Vector3.Distance(transform.position,player.transform.position);
		DeteckGround();
		switch(type){
		case(Types.patrol):
			if(rangeToPlayer < rangeActive && !onBack && transform.position.y < player.transform.position.y + 1.5f && transform.position.y > player.transform.position.y - 1.5f){
				MoveTowardPlayer();
			}
			else if(!onBack){
				Patrol();
			}
			break;
		case(Types.waiting):
			if(rangeToPlayer < rangeActive && !onBack){
				MoveTowardPlayer();
			}
			break;
		}
		if(!onGround){
			onBack = true;
		}

		if(onBack){
			StartCoroutine(BackPosition());
		}
		SetAnimation();
		FlipPlayer();
	}

	public void MoveTowardPlayer(){
		if(transform.position.x < player.transform.position.x - 0.3f){
			body.velocity = new Vector2(speed,body.velocity.y);
		}
		else if(transform.position.x > player.transform.position.x + 0.3f){
			body.velocity = new Vector2(-speed,body.velocity.y);
		}
		else{
			body.velocity = Vector2.zero;
		}
	}

	private void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			anim.SetBool("onAttack",true);
		}
	}

	private void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			anim.SetBool("onAttack",false);
		}
	}
}
