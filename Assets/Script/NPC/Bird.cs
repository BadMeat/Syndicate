using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

	//pergerakan
	public float speed;
	private Rigidbody2D body;

	//posisi hilang dan muncul
	public float minX;
	public float maxX;

	//kiri kenana atau sebaliknya
	public enum Types{
		left,
		right
	}

	public Types type;


	void Start () {
		body = GetComponent<Rigidbody2D>();
		if(type == Types.left){
			transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
		}
	}

	void Update () {
		switch(type){
		case(Types.left):
			body.velocity = new Vector2(-speed,body.velocity.y);
			if(transform.position.x < minX){
				transform.position = new Vector3(maxX,transform.position.y,transform.position.z);
			}
			break;
		case(Types.right):
			body.velocity = new Vector2(speed,body.velocity.y);
			if(transform.position.x > maxX){
				transform.position = new Vector3(minX,transform.position.y,transform.position.z);
			}
			break;
		}
	}
}
