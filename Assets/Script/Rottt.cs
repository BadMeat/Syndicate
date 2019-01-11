using UnityEngine;
using System.Collections;

public class Rottt : MonoBehaviour {

	public GameObject pelor;
	public GameObject player;
	private float range;
	private float titik;
	public float lala;
	public float lili;

	private bool onAttack;
	private float startScale;

	void Start () {
		startScale = transform.localScale.x;
	}

	void Update () {
		StartCoroutine(Attacing());
		if(player.transform.position.x < transform.position.x){
			transform.localScale = new Vector3(-startScale,transform.localScale.y,transform.localScale.z);
		}
		else if(player.transform.position.x > transform.position.x){
			transform.localScale = new Vector3(startScale,transform.localScale.y,transform.localScale.z);
		}
	}

	private IEnumerator Attacing(){
		if(onAttack){
			yield break;
		}
		onAttack = true;
		range = Vector3.Distance(player.transform.position,transform.position);
		if(player.transform.position.x > transform.position.x){
			titik = transform.position.x + (range/2) + lala;
		}
		else if(player.transform.position.x < transform.position.x){
			titik = transform.position.x - (range/2) - lala;
		}
		//GameObject pel = (GameObject) Instantiate(pelor,new Vector3(titik,transform.position.y,transform.position.z),pelor.transform.rotation);
		GameObject pel = (GameObject) Instantiate(pelor,transform.position,Quaternion.identity);
		/*if(player.transform.position.x < transform.position.x){
			pel.transform.localScale = new Vector3(-pel.transform.localScale.x,pel.transform.localScale.y,pel.transform.localScale.z);
		}
		pel.GetComponent<Plur>().bullet.transform.position = transform.position;*/
		pel.GetComponent<Plur>().xRig = range;
		pel.GetComponent<Plur>().yRig = 10f;
		//pel.GetComponent<Plur>().Attack(titik,lala);
		yield return new WaitForSeconds(2f);
		onAttack = false;
	}
}
