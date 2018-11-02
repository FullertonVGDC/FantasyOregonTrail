using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	[SerializeField]
	private float fillAmount;

	//[SerializeField]
	public Image bar;

	public GameObject player;
	private PlayerScript playerStatus;

	void Start()
	{
		playerStatus = player.GetComponent<PlayerScript> ();
	}

	void Update() {
		HandleBar ();
	}

	private void HandleBar()
	{
		bar.fillAmount = changeAmt (playerStatus.getHealth(), 100); //fillAmount;
	}

	private float changeAmt(float val, float max)
	{
		return(val / max);
	}
}
