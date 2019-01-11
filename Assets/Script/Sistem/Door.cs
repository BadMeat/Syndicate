using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public int nextLevel;
	public Sprite closeDoor;

	private SpriteRenderer render;

	void Start(){
		render = GetComponent<SpriteRenderer>();
	}

	private void OnTriggerEnter2D(Collider2D other){
		if(other.GetComponent<Controller>() != null){
			if(nextLevel != 3 && GameManager.gm.AllEnemy <= 0){
				StartCoroutine(EnterLevel());
			}
		}
	}

	private IEnumerator EnterLevel(){
		render.sprite = closeDoor;
		Controller.controller.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
		Controller.controller.GetComponent<Animator>().SetBool("onGround",true);
		Controller.controller.enabled = false;
		yield return new WaitForSeconds(1f);
		Controller.controller.HidePlayer();
		yield return new WaitForSeconds(1f);
		if(Game.game.DoorPass < nextLevel){
			Game.game.DoorPass = nextLevel;
		}
		GameManager.gm.enemyDie.RemoveRange(0,GameManager.gm.enemyDie.Count);
		SaveLoad.Save();
		StartCoroutine(GameMenu.gm.WinScreen());	
	}
}