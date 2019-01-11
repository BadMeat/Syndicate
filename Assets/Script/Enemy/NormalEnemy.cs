using UnityEngine;
using System.Collections;

public class NormalEnemy : BaseEnemy {
	public float unitMove;
	private bool onLeft;

	public void Patrol(){
		if(transform.position.x> unitMove){
			onLeft = true;
		}
		else if(transform.position.x < StartPos){
			onLeft = false;
		}

		if(onLeft){
			body.velocity = new Vector2(-speed,body.velocity.y);
		}else{
			body.velocity = new Vector2(speed,body.velocity.y);
		}
	}
}
