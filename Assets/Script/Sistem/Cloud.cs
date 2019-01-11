using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	public float speed;
	public float minValue;
	public float maxValue;

	private Rigidbody2D body;

	void Start () {
		body = GetComponent<Rigidbody2D>();
	}

	void Update () {
		body.velocity = new Vector2(-speed,body.velocity.y);
		if(transform.position.x <= minValue){
			transform.position = new Vector3(maxValue,transform.position.y,transform.position.z);
		}
	}
}
