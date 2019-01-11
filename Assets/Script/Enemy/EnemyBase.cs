using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemyBase{
	public float health;
	public float speed;
	public float delay;
	public float damage;
	public GameObject bullet;
	public Transform bulletOut;
	public float rangeActive;
	public int goldDrop; 

	public enum Type{
		biasa,
		waiting,
		brem,
		waitingBrem,
		boss
	}

	public Type type;
}
