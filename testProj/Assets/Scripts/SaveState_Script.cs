using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 	//Used for C# saving/Loading
using System.Runtime.Serialization.Formatters.Binary;	//Used for C# saving/Loading
using System.IO;	//Used for C# saving/loading
using UnityEngine.SceneManagement;

public static class GlobalVariables {
	//public static int skinCount = 23;

}

//clean class without MonoBehavior
[Serializable] // Lets the class be saved to a file
class PlayerSaveData
{
	//public int currentSkinIndex;
	//public bool[] purchaseArray = new bool[GlobalVariables.skinCount]; // number should be same as skinsCount from above


}

public class SaveState : MonoBehaviour {

	public static SaveState saveControl;

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


		/*data.currentSkinIndex = currSkinIndex;
		for (int i=0; i < GlobalVariables.skinCount; i++) 
		{
			data.purchaseArray[i] = skinInfo[i].purchased;
		}
		*/

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

			/*currSkinIndex = data.currentSkinIndex;
			for (int i = 0; i < GlobalVariables.skinCount; i++) { 
				skinInfo [i].purchased = data.purchaseArray [i];
			}
			*/
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
