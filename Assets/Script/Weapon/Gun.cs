using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public float roro = 180f;
	public float rangeUp = 7f;
	public float rangePlayer = 5f;

	private Vector3 diff;
	private float rot_z;
	private float rangeToPlayer;
	private Enemy enemy;
	private float startScale;
	private float rorot;

	void Start () {
		startScale = transform.localScale.y;
		enemy = GetComponentInParent<Enemy>();
	}

	void Update () {
		rangeToPlayer = Vector3.Distance(Controller.controller.transform.position, transform.position);
		if(rangeToPlayer < enemy.enemyBaseState.rangeActive && Controller.controller.transform.position.y <= transform.position.y + rangeUp && Controller.controller.transform.position.y >= transform.position.y - rangeUp && rangeToPlayer > rangePlayer){
			diff = Controller.controller.transform.position - transform.position;
			diff.Normalize();
			rot_z = Mathf.Atan2(diff.y,diff.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f,rorot,rot_z - roro);
			if(enemy.transform.localScale.x < 0){
				rorot = 180f;
				transform.localScale = new Vector3(transform.localScale.x,startScale,transform.localScale.z);
			}
			else if(enemy.transform.localScale.x > 0){
				rorot = 0f;
				transform.localScale = new Vector3(transform.localScale.x,-startScale,transform.localScale.z);
			}
		}
	}
}
