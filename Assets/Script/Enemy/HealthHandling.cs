using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthHandling : MonoBehaviour {
	public Image health;
	private Enemy enemy;
	private float calHealth;

	void Start(){
		enemy = GetComponentInParent<Enemy>();
	}

	private void HandleHealth(){
		calHealth = (enemy.enemyBaseState.health - 0) / (10f - 0);
		health.transform.localScale = new Vector3(Mathf.Clamp( calHealth,0,1),health.transform.localScale.y,health.transform.localScale.z);
	}

	void Update(){
		HandleHealth();
	}
}
