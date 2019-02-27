using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover_Script : MonoBehaviour, IPointerEnterHandler {

	public string msg = "";

	public void OnPointerEnter(PointerEventData eventData){
		Debug.Log (msg);
	}

	public void OnPointerExit(PointerEventData eventData){
		Debug.Log ("leaving item");
	}

}
