using UnityEngine;
using System.Collections;

public class Plur : MonoBehaviour {

	public GameObject bullet;
	public float speed = 10f;
	private float rot;

	private Vector3 tempcEul;
	private Rigidbody2D body;

	public float xRig;
	public float yRig;

	void Start () {
		body = GetComponent<Rigidbody2D>();
		if(transform.localScale.x < 0){
			speed = -speed;
		}
		tempcEul = transform.eulerAngles;
		body.velocity = new Vector2(xRig,yRig);
	}

	/*void Update () {
		rot -= speed;
		tempcEul.z = rot;
		transform.eulerAngles = tempcEul;
		bullet.transform.eulerAngles = new Vector3(0f,0f,0f);
		Destroy(gameObject,5f);
	}*/

	void Update(){
		
	}

	private void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			Destroy(gameObject);
		}
	}

	public void Attack(float x, float y){
		body.velocity = new Vector2(x,y);
	}
}
