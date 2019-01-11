using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {

	public GameObject target;
	public GameObject explotion;
	public float maxSpeed = 10f;
	public float minSpeed = 5f;
	private float speed;
	private bool readyMove;
	public float moveUp;

	private Vector3 diff;
	private float rot_z;

	void Start () {
		speed = Random.Range(minSpeed,maxSpeed); 
		moveUp += transform.position.y;		
		StartCoroutine(Attacking());
	}

	void FixedUpdate () {
		if(readyMove){
			transform.position = Vector3.MoveTowards(transform.position,target.transform.position,speed * Time.deltaTime);
			diff = transform.position - target.transform.position;
			diff.Normalize();
			rot_z = Mathf.Atan2(diff.y,diff.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f,0f,rot_z - 180f);
		}
	}

	private IEnumerator Attacking(){
		while(transform.position.y < moveUp){
			transform.Translate(Vector3.right * speed * Time.deltaTime);
			yield return null;
		}
		float rot = 90f;
		while(rot > 0){
			rot -= 5f;
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,rot);
			yield return null;
		}
		readyMove = true;
	}

	private void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			Instantiate(explotion,transform.position,Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
