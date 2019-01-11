using UnityEngine;
using System.Collections;

public class IroniMan : MonoBehaviour {

	public float unitUp;
	public float unitMove;
	private float speed;
	private bool terbang;

	private Rigidbody2D body;
	private bool mendarat;
	private int direction;
	private float scaleX;

	private Animator anim;
	public GameObject ledakan;

	IEnumerator Start () {
		scaleX = transform.localScale.x;
		anim = GetComponent<Animator>();
		body = GetComponent<Rigidbody2D>();
		while(true){
			yield return StartCoroutine(UnitTerbang());
		}
	}

	private IEnumerator UnitTerbang(){
		speed = Random.Range(2f,4f);
		direction = Random.Range(0,10);
		unitUp = transform.position.y + Random.Range(2f,6f);
		Instantiate(ledakan,transform.position,Quaternion.identity);
		while(transform.position.y < unitUp){
			anim.SetBool("onTerbang",true);
			body.velocity = new Vector2(body.velocity.x,speed);
			yield return null;
		}
		body.velocity = Vector2.zero;
		if(direction <5){
			unitMove = transform.position.x + Random.Range(4f,10f);
			while(transform.position.x < unitMove && !mendarat){
				transform.localScale = new Vector3(scaleX,transform.localScale.y,transform.localScale.z);
				anim.SetBool("onTerbang",false);
				anim.SetBool("onMelayang",true);
				body.velocity = new Vector2(speed,body.velocity.y);
				yield return null;
			}
		}else{
			transform.localScale = new Vector3(-scaleX,transform.localScale.y,transform.localScale.z);
			unitMove = transform.position.x - Random.Range(4f,10f);
			while(transform.position.x > unitMove && !mendarat){
				anim.SetBool("onTerbang",false);
				anim.SetBool("onMelayang",true);
				body.velocity = new Vector2(-speed,body.velocity.y);
				yield return null;
			}
		}

		body.velocity = Vector2.zero;
		while(!mendarat){
			anim.SetBool("onMelayang",false);
			anim.SetBool("onTurun",true);
			body.velocity = new Vector2(body.velocity.x,-speed);
			yield return null;
		}
		anim.SetBool("onTurun",false);
		body.velocity = Vector2.zero;
		yield return new WaitForSeconds(2f);
	}

	private void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Ground"){
			mendarat = true;
		}
	}

	private void OnCollisionExit2D(Collision2D other){
		if(other.gameObject.tag == "Ground"){
			mendarat = false;
		}
	}
}
