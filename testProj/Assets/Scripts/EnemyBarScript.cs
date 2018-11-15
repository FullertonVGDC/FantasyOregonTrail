using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBarScript : MonoBehaviour {

	[SerializeField]
	private float fillAmount;

	//[SerializeField]
	public Image bar;
	public int barType;
	public Text barText;

	public GameObject enemy;
	[SerializeField]
	private EnemiesScipt enemyStatus;

	void Start()
	{
		
	}

	void Update() {
		HandleBar ();
		//this.transform.position = Camera.main.WorldToScreenPoint (enemy.transform.position);

	}

	public void setEnemyBar(GameObject newEnemy){
		enemyStatus = newEnemy.GetComponent<EnemiesScipt> ();
		this.gameObject.SetActive (true);
	}

	private void HandleBar()
	{
		if(barType == 1) // Enemy 1
		{
			bar.fillAmount = changeAmt (enemyStatus.health, enemyStatus.maxHealth); //fillAmount;
			barText.text = enemyStatus.health.ToString() + " / " + enemyStatus.maxHealth.ToString();
		}

	}

	private float changeAmt(float val, float max)
	{
		return(val / max);
	}
}
