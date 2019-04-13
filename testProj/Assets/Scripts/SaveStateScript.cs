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
	public float maxHealth;
	public int maxStrength;
	public float maxStamina;
	public int maxSpeed;
	public int totalRenown;
	public bool[] upgradesArray = new bool[6];
	public Vector3Int lastTown;

}

public class SaveStateScript : MonoBehaviour {

	public static SaveStateScript saveControl;
	/*public float maxHp;
	public int maxStr;
	public float maxStm;
	public int maxSpd;
	public int renown;
	public bool[] upgrades;
	public Vector3Int currTown;
*/
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


	public void Save(float hp, float stm, int str, int spd, int ren, Vector3Int pos, bool[] upg) //Saves data out to file
	{
		//string filePath = Path.Combine(Application.persistentDataPath, jsonFilename);

		PlayerSaveData data = new PlayerSaveData ();
		data.maxHealth = hp;//PlayerScript.savePlayerControl.getMaxHealth();//maxHp;
		data.maxStrength = str;//PlayerScript.savePlayerControl.getStrength();//maxStr;
		data.maxStamina = stm;//PlayerScript.savePlayerControl.getMaxStamina();//maxStm;
		data.maxSpeed = spd;//PlayerScript.savePlayerControl.getSpeed();//maxSpd;
		data.totalRenown = ren;//PlayerScript.savePlayerControl.getRenown();//renown;
		data.lastTown = pos;//PlayerScript.savePlayerControl.currPos;//currTown;

		for (int i=0; i < 6; i++) 
		{ data.upgradesArray[i] = upg[i]; }

		//Json saving
		PlayerPrefs.SetString ("saveInfo", JsonUtility.ToJson(data, true)); // TESTING
		PlayerPrefs.Save();
		Debug.Log ("Save Complete at " + Application.persistentDataPath);
	}

	public void Load(PlayerScript plr)//float hp, float stm, int str, int spd, int ren, Vector3Int pos, bool[] upg)//json loading
	{

		//string filePath = Path.Combine(Application.persistentDataPath, jsonFilename);

		string test = PlayerPrefs.GetString ("saveInfo");
		PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData> (test);

		if (PlayerPrefs.HasKey("saveInfo")) { //File.Exists (filePath)

			plr.setMaxHealth(data.maxHealth);
			plr.setStrength(data.maxStrength);
			plr.setMaxStamina(data.maxStamina);
			plr.setSpeed(data.maxSpeed);
			plr.setRenown(data.totalRenown);
			plr.currPos = data.lastTown;

			for (int i=0; i < 6; i++) 
			{ plr.upgrades[i] = data.upgradesArray[i]; }

			Debug.Log ("Load was successful");
		} 
		else {
			Debug.LogError ("Cant load game data or First Time Playing!");
			Vector3Int temp = new Vector3Int (0, 0, 0);
			bool[] temp2 = new bool[6];
			Save (100,100,5,5,0,temp,temp2);
		}


	}


		


	public void ResetSavedData()
	{

		/*
		currSkinIndex = 0;
		for (int i = 1; i < GlobalVariables.skinCount; i++) {
			skinInfo [i].purchased = false;
		}
		*/

		Vector3Int temp = new Vector3Int (0, 0, 0);
		bool[] temp2 = new bool[6];
		Save (100,100,5,5,0,temp,temp2);
	}

}

//serialized class location
