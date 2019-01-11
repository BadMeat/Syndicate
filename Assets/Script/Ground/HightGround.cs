using UnityEngine;
using System.Collections;

public class HightGround : MonoBehaviour {

	public float rangePlayer;

	void Update () {
		if(transform.position.y <= Controller.controller.transform.position.y + rangePlayer){
			gameObject.layer = 8;
		}
		if(transform.position.y >= Controller.controller.transform.position.y -rangePlayer){
			gameObject.layer = 16;
		}
	}

	private void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere(transform.position,rangePlayer);
	}
}
