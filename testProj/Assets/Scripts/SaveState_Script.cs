using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 	//Used for C# saving/Loading
using System.Runtime.Serialization.Formatters.Binary;	//Used for C# saving/Loading
using System.IO;	//Used for C# saving/loading
using UnityEngine.SceneManagement;

public static class GlobalVariables {
	//public static int upgadeCount = 6;

}

//clean class without MonoBehavior
[Serializable] // Lets the class be saved to a file
class PlayerSaveData
{
	public int maxHealth;
	public int maxStrength;
	public int maxStamina;
	public int maxSpeed;
	public int totalRenown;
	public bool[] upgradesArray = new bool[6];
	public Vector3Int lastTown;

}

public class SaveState : MonoBehaviour {

	public static SaveState saveControl;
	public int maxHp;
	public int maxStr;
	public int maxStm;
	public int maxSpd;
	public int renown;
	public bool[] upgrades;
	public Vector3Int currTown;

	void Awake () {
		if (saveControl == null) {
			DontDestroyOnLoad (gameObject);
			saveControl = this;
		} else if (saveControl != this) {
			Destroy (gameObject);
		}

		//Testing features below: uncomment below to reset all info
		//ResetSavedData();
	}


	public void Save() //Saves data out to file
	{
		//string filePath = Path.Combine(Application.persistentDataPath, jsonFilename);

		PlayerSaveData data = new PlayerSaveData ();

		data.maxHealth = maxHp;
		data.maxStrength = maxStr;
		data.maxStamina = maxStm;
		data.maxSpeed = maxSpd;
		data.totalRenown = renown;
		data.lastTown = currTown;

		for (int i=0; i < 6; i++) 
		{ data.upgradesArray[i] = upgrades[i]; }

		//Json saving
		PlayerPrefs.SetString ("saveInfo", JsonUtility.ToJson(data, true)); // TESTING
		PlayerPrefs.Save();
		Debug.Log ("Save Complete at" + Application.persistentDataPath);
	}

	public void Load()//json loading
	{

		//string filePath = Path.Combine(Application.persistentDataPath, jsonFilename);

		string test = PlayerPrefs.GetString ("saveInfo");
		PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData> (test);

		if (PlayerPrefs.HasKey("saveInfo")) { //File.Exists (filePath)

			maxHp = data.maxHealth;
			maxStr = data.maxStrength;
			maxStm = data.maxStamina;
			maxSpd = data.maxSpeed;
			renown = data.totalRenown;
			currTown = data.lastTown;

			for (int i=0; i < 6; i++) 
			{ upgrades[i] = data.upgradesArray[i]; }
		} 
		else {
			Debug.LogError ("Cant load game data!");
			Save ();
		}


	}


	void ResetSavedData()
	{

		/*
		currSkinIndex = 0;
		for (int i = 1; i < GlobalVariables.skinCount; i++) {
			skinInfo [i].purchased = false;
		}
		*/


		Save ();
	}

}

//serialized class location
