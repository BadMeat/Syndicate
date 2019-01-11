using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager gm;

	public Vector3 SavePosition{get;set;}
	public int AllEnemy{set;get;}

	public List<string> enemyDie;

	void Awake(){
		if(gm == null){
			gm = this;
			DontDestroyOnLoad(gameObject);
		}
		else if(gm != null){
			Destroy(gameObject);
		}
	}

	public void EnemyDie(GameObject enemy){
		enemyDie.Add(enemy.name);
	}

	public void CountEnemy(){
		AllEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
	}
}
