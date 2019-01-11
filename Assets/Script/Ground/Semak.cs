using UnityEngine;
using System.Collections;

public class Semak : MonoBehaviour {

	private SpriteRenderer sr;

	void Start () {
		sr = GetComponent<SpriteRenderer>();
	}

	void Update () {
		if(transform.position.y > Controller.controller.transform.position.y){
			sr.sortingOrder = -10;
		}
		else if(transform.position.y < Controller.controller.transform.position.y){
			sr.sortingOrder = 10;
		}
	}
}
