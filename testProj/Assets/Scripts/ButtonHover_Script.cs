using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover_Script : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public GameObject panel;
	public int buttonNum;
	public GameObject playerObj;

	public void OnPointerEnter(PointerEventData eventData){
		//GameObject panel = GetComponentInChildren<GameObject> ();
		if (Input.GetMouseButton(1)) {
			setButtonText ();
			panel.SetActive (true);
		}
		else
			panel.SetActive (false);
	}

	public void OnPointerExit(PointerEventData eventData){
		//GameObject panel = GetComponentInChildren<GameObject> ();
		panel.SetActive (false);
	}


	public void setButtonText(){

		Text infoTxt = panel.GetComponentInChildren<Text> ();

		switch (buttonNum) {
		case 1:
			int str = playerObj.GetComponent<PlayerScript> ().getStrength ();
			infoTxt.text = "Light Attack\n\nDmg: " + str  + "\nGain: 5 stamina";
			break;
		case 2:
			int dbl_str = 2 * playerObj.GetComponent<PlayerScript> ().getStrength ();
			infoTxt.text = "Bash Attack\n\nCost: 20 stamina\nDmg: " + dbl_str;
			break;
		case 3:
			infoTxt.text = "Heal\n\nCost: 30 stamina\nGain: 30 health";
			break;
		case 4:
			infoTxt.text = "Evade\n\nEach enemy has a 50% chance to miss their next attack and you regain 20 stamina";
			break;
		case 9:
			infoTxt.text = "Change Target\n\nChange targeted enemy for attack";
			break;
		default:
			break;
		}
	}
}
