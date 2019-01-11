using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

	public float speed;
	public float damage; 

	private Rigidbody2D body;

	void Start(){
		body = GetComponent<Rigidbody2D>();
		if(transform.localScale.x < 0){
			speed = -speed;
		}
	}

	void FixedUpdate(){
		body.velocity = new Vector2(speed,body.velocity.y);
		Destroy(gameObject,3f);
	}

	private void OnTriggerEnter2D(Collider2D other){
		if(other.GetComponent<Controller>() != null){
			GameMenu.gm.SetHealth(1);
			Destroy(gameObject);
		}
		if(other.tag=="Wall"){
			Destroy(gameObject);
		}
	}
}
