using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	[SerializeField]
	private float fillAmount;

	//[SerializeField]
	public Image bar;
	public int barType;
	public Text barText;

	public GameObject player;
	private PlayerScript playerStatus;

	public GameObject mainGameOBJ;
	private GameManager_1 gameMGR;

	void Start()
	{
		playerStatus = player.GetComponent<PlayerScript> ();
		gameMGR = mainGameOBJ.GetComponent<GameManager_1> ();
	}

	void Update() {
		HandleBar ();
	}

	private void HandleBar()
	{
		if(barType == 1) // Health
		{
			bar.fillAmount = changeAmt (playerStatus.getHealth(), playerStatus.getMaxHealth()); //fillAmount;
			barText.text = playerStatus.getHealth().ToString() + " / " + playerStatus.getMaxHealth().ToString();
		}
		if(barType == 2) // Stamina
		{
			bar.fillAmount = changeAmt (playerStatus.getStamina(), playerStatus.getMaxStamina()); //fillAmount;
			barText.text = playerStatus.getStamina().ToString() + " / " + playerStatus.getMaxStamina().ToString();
		}
		if (barType == 3) // Time
		{
			bar.fillAmount = changeAmt (gameMGR.gameTime, 24);
			barText.text = gameMGR.gameTime.ToString() + " / 24";
		}
	}

	private float changeAmt(float val, float max)
	{
		return(val / max);
	}
}
