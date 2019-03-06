﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour {

	public void LoadScene(string name) {
		SceneManager.LoadScene (sceneName: name);
	}


		

	public void QuitGame() {
		Application.Quit ();
	}


}
