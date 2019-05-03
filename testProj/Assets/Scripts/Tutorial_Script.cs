using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Script : MonoBehaviour {

	public Sprite mapTut;
	public Sprite battleTut;
	public Sprite townTut;
	public Sprite campTut;
	public GameObject currentTutObj; // Panel holding img and button
	public GameObject currentTutImg; //Tutorial Image

	public int currentTutInt;

	public void toggleTutImg(){
		if (currentTutObj.activeSelf)
			currentTutObj.SetActive (false);
		else {
			setTut ();
			currentTutObj.SetActive (true);
		}
	}

	void setTut(int val = 1){
		if (val < 1 || val > 4)
			currentTutInt = val = 1;
		
		switch (val) {
		case 1:
			currentTutImg.GetComponent<Image>().sprite = mapTut;
			break;
		case 2:
			currentTutImg.GetComponent<Image>().sprite = battleTut;
			break;
		case 3:
			currentTutImg.GetComponent<Image>().sprite = townTut;
			break;
		case 4:
			currentTutImg.GetComponent<Image>().sprite = campTut;
			break;
		default:
			break;
		}

	}

	public void incrementTutInt(){
		currentTutInt++;
		setTut (currentTutInt);
	}
}
