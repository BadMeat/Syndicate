using UnityEngine;
using System.Collections;

public class UpGround : MonoBehaviour {

	public float speed = 10f;

	private Rigidbody2D body;

	void Start () {
		//body = GetComponent<Rigidbody2D>();	
	}

	void Update () {
		//body.velocity = new Vector2(body.velocity.x,-speed);
		transform.Translate(Vector3.down * speed * Time.deltaTime);
	}
}
