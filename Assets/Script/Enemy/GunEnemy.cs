using UnityEngine;
using System.Collections;

public class GunEnemy : NormalEnemy {

	public GameObject bullet;
	public Transform bulletOut;
	public int numberBullet;

	private bool onAttack;

	void Start(){
		BaseStart();
	}

	void Update () {
		Patrol();
		StartCoroutine(Attacking(numberBullet));
	}

	public IEnumerator Attacking(int number){
		if(onAttack){
			yield break;
		}
		onAttack = true;
		for(int a=0;a<number;a++){
			Instantiate(bullet,bulletOut.position,bullet.transform.rotation);
			yield return new WaitForSeconds(0.2f);
		}
		onAttack = false;
	}
}
