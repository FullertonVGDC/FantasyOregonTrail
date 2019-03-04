using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover_Script : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public GameObject panel;

	public void OnPointerEnter(PointerEventData eventData){
		//GameObject panel = GetComponentInChildren<GameObject> ();
		panel.SetActive (true);
	}

	public void OnPointerExit(PointerEventData eventData){
		//GameObject panel = GetComponentInChildren<GameObject> ();
		panel.SetActive (false);
	}

}
