using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed;
	public float damage;

	private Rigidbody2D body;

	void Start () {
		body = GetComponent<Rigidbody2D>();

		if(transform.localScale.x < 0){
			speed =-speed;
		}
	}

	void Update () {
		body.velocity = new Vector2(speed,body.velocity.y);
		Destroy(gameObject,1.5f);
	}

	private void OnTriggerEnter2D(Collider2D other){
		if(other.GetComponent<Enemy>() != null){
			other.GetComponent<Enemy>().GetHitted(damage);
			Destroy(gameObject);
		}

		if(other.tag == "Wall"){
			Destroy(gameObject);
		}
	}

	public float Damage{
		set{damage = value;}
	}
}
