using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : GameManager_1 {

	public Camera battleCam;
	public GameObject battlePanel;
	public bool isPlayerTurn;
	public bool battleInProgress;
	//Player 1 (possibly add companions)
	//Enemy 1 (possibly add more than 1)

	private float turnDelay = 2f;
	private WaitForSeconds m_turnWait;


	// Use this for initialization
	void Start () {
		m_turnWait = new WaitForSeconds (turnDelay);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		

	#region Prepare/End Battle
	// Setup Battle
	public IEnumerator SetupBattle(){
		//Place enemies
		yield return StartCoroutine(SetupUI());
		yield return StartCoroutine(StartBattle());
		yield return StartCoroutine (EndBattle ());
	}

	//Turn on Proper camera & UI
	private IEnumerator SetupUI(){
		battleCam.gameObject.SetActive (true);
		battlePanel.gameObject.SetActive (true);
		yield return null;
	}

	//End Battle
	private IEnumerator EndBattle(){
		battleCam.gameObject.SetActive (false);
		battlePanel.gameObject.SetActive (false);
		yield return null;
	}
	#endregion

	#region TurnControl
	public IEnumerator StartBattle(){
		isPlayerTurn = true;
		battleInProgress = true;
		yield return m_turnWait;
		//Loop the Battle Here
		while(battleInProgress){
			if (isPlayerTurn) {
				Debug.Log ("Players Turn");
				yield return StartCoroutine (PlayerTurn());
			} 
			else { // Enemies Turn
				Debug.Log("Enemy Goes");
				yield return StartCoroutine (EnemyTurn());
				battleInProgress = false;
			}

		}
	}

	public IEnumerator PlayerTurn() {
		while(isPlayerTurn){
			yield return null;
		}
		yield return null;
	}
	public IEnumerator EnemyTurn() {
		// wait for enemy to finish
		yield return m_turnWait;
		playerinfo.addHealth(-20); // example attack
		yield return m_turnWait;
		isPlayerTurn = true;
	}
	#endregion


	#region Button Control
	//Button interactions
	public void Attack1_OnClick(){
		//deal attack 1 dmg
		Debug.Log("You attack1");
		isPlayerTurn = false;
	}
	public void Attack2_OnClick(){
		//deal attack 2 dmg
		Debug.Log("You attack2");
		isPlayerTurn = false;
	}
	public void Attack3_OnClick(){
		//deal attack 3 dmg
		Debug.Log("You attack3");
		isPlayerTurn = false;
	}
	public void Flee_OnClick(){
		//you attempt to run
		Debug.Log("You run away");
		isPlayerTurn = false;
		battleInProgress = false;
	}
	#endregion

}
