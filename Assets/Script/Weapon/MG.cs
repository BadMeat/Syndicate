using UnityEngine;
using System.Collections;

public class MG : MonoBehaviour {

	public float speed = 100f;
	public enum Types{
		normal,
		cannon
	}

	public Types type;
	public GameObject explotion;

	void Start () {
		if(transform.localScale.x < 0){
			speed = -speed;
		}
	}

	void FixedUpdate () {
		transform.Translate(Vector3.right * speed * Time.deltaTime);
		Destroy(gameObject,3f);
	}

	private void OnTriggerEnter2D(Collider2D other){
		if(other.GetComponent<Controller>() != null && !other.GetComponent<Controller>().Invisible || other.tag == "Ground"){
			if(type == Types.cannon){
				Instantiate(explotion,transform.position,Quaternion.identity);
			}
			GameMenu.gm.SetHealth(1);
			Destroy(gameObject);
		}
		if(other.tag=="Wall"){
			Destroy(gameObject);
		}
	}
}
