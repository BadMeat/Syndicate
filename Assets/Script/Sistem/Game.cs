using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Game {

	public static Game game;
	private int characterSelected;
	private int doorPass;

	public int LevelEnter{set;get;}
	public bool StartedGame{set;get;}

	//senjata upgrade
	public float damageUpgrade{set;get;}
	public float healthUpgrade{set;get;}
	public float speedUpgrade{set;get;}
	public float delayUpgrade{set;get;}

	//status
	private float health;
	public float Damage{set;get;}
	public int Gold{set;get;}
	public float Delay{set;get;}
	public float Speed{set;get;}
	public string WeaponUse{set;get;}

	public List<string> weaponGet = new List<string>();

	public int CharacterSelected{
		get{return characterSelected;}
		set{characterSelected = value;}
	}

	public int DoorPass{
		get{return doorPass;}
		set{doorPass = value;}
	}

	public float Health{
		get{return health;}
		set{health = value;}
	}
}
