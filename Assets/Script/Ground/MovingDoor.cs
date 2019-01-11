using UnityEngine;
using System.Collections;

public class MovingDoor : MonoBehaviour {

	public GameObject nextDoor;

	//tipe pintu
	public enum Types{
		door,
		doorGoal
	}

	public Types type;

	private bool onMove;

	//animasi
	private Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
	}

	void Update () {
		if(type == Types.door){
			if(onMove){
				StartCoroutine(MovingToTarget());
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other){
		if(other.GetComponent<Controller>() != null){
			onMove = true;
		}
	}

	private IEnumerator MovingToTarget(){
		GameMenu.gm.HideButton();
		anim.SetBool("onOpen",true);
		Controller.controller.MoveStop();
		yield return new WaitForSeconds(0.5f);

		while(Controller.controller.transform.position.y < nextDoor.transform.position.y){
			Controller.controller.HidePlayer();
			Controller.controller.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,10f);
			yield return null;
		}
		Controller.controller.MoveStop();
		nextDoor.GetComponent<Animator>().SetBool("onOpen",true);
		yield return new WaitForSeconds(0.5f);
		Controller.controller.ShowPlayer();
		GameMenu.gm.ShowButton();
		onMove = false;
	}
}
