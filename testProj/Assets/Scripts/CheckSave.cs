using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckSave : MonoBehaviour {

	public Button cont_BTN;

	// Use this for initialization
	void Awake () {
		if (PlayerPrefs.HasKey ("saveInfo"))
			cont_BTN.interactable = true;
		else
			cont_BTN.interactable = false;
	}

}
