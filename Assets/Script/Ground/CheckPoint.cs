using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	private bool count;

	private void OnTriggerEnter2D(Collider2D other){
		if(other.GetComponent<Controller>() != null){
			Game.game.StartedGame = true;
			GameManager.gm.SavePosition = other.GetComponent<Controller>().transform.position;
		}
	}

	private void OnTriggerStay2D(Collider2D other){
		if(other.tag == "Player"){
			if(!count){
				GameManager.gm.CountEnemy();
				count = true;
			}
		}
	}
}
