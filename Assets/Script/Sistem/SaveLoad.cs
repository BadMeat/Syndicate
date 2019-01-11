using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad{

	public static Game saveGame = new Game();

	public static void Save(){
		SaveLoad.saveGame = Game.game;
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/saveGame.gd");
		bf.Serialize(file,SaveLoad.saveGame);
		file.Close();
	}

	public static void Load(){
		if(File.Exists(Application.persistentDataPath + "/saveGame.gd")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/saveGame.gd",FileMode.Open);
			SaveLoad.saveGame = (Game)bf.Deserialize(file);
			file.Close();
		}
	}
}
