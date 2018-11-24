using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BattleManager : GameManager_1 {

	public Camera battleCam;
	public GameObject battlePanel;
	public GameObject Fighter_Space;
	public bool isPlayerTurn;
	public bool isMovement;
	public bool battleInProgress;
	public Text battleInfo_Text;

	private int enemyTotal = 0;
	public GameObject targetEnemy;
	public int targetIndex;
	public GameObject targetIndicator;
	[SerializeField] //testing purposes
	List<GameObject> enemyList;
	[SerializeField] //testing purposes
	List<GameObject> turnOrder;

	//Vector3 posStart;

	public GameObject playerObj_Battle;
	//(possibly add companions)

	public GameObject enemyHpBar_1;
	public GameObject enemyHpBar_2;
	private EnemyBarScript enemyBar_1;
	private EnemyBarScript enemyBar_2;

	//Enemy 1 (possibly add more than 1)
	public GameObject slimeMonster;
	public GameObject wolfMonster;
	private GameObject enemy_1;
	private GameObject enemy_2;
	//private SlimeScript slimeInfo;

	private float turnDelay = 1f;
	//private float moveDelay = 0.5f;
	private WaitForSeconds m_turnWait;
	//private WaitForSeconds m_moveWait;


	// Use this for initialization
	void Start () {
		enemyBar_1 = enemyHpBar_1.GetComponent<EnemyBarScript> ();
		enemyBar_2 = enemyHpBar_2.GetComponent<EnemyBarScript> ();
		m_turnWait = new WaitForSeconds (turnDelay);
		//m_moveWait = new WaitForSeconds (moveDelay);
	}
	
	// Update is called once per frame
	void Update () {	// to use with Battle Manager make sure battle is happening
		//if(battleInProgress){}
	}
		

	#region Prepare/End Battle
	// Setup Battle
	public IEnumerator SetupBattle(string battleLoc){
		enemyList = new List<GameObject>(); // refresh List
		yield return StartCoroutine (SetupEnemies(battleLoc, enemyList));
		yield return StartCoroutine(SetupUI());
		yield return StartCoroutine(StartBattle(enemyList));
		yield return StartCoroutine (EndBattle ());
	}

	private IEnumerator SetupEnemies(string battleLoc, List<GameObject> enemyList){
		//choose # of enemies first
		enemyTotal = Random.Range(1,3); // 1 or 2
		//enemyTotal = 2;

		//Enemy_1
		//enemy_1 = Instantiate (slimeMonster); //Choose Monster
		enemy_1 = Instantiate(ChooseMonster(battleLoc));
		enemyBar_1.setEnemyBar(enemy_1);
		enemy_1.transform.parent = Fighter_Space.transform;
		enemy_1.transform.localPosition = new Vector3 (-8, -10, 0);
		enemy_1.GetComponent<EnemiesScipt> ().setEnemyNum (1);
		targetEnemy = enemy_1;
		targetIndex = 0;
		targetIndicator.transform.localPosition = targetEnemy.transform.localPosition + new Vector3 (0,2,0);
		enemyList.Add (enemy_1);

		if (enemyTotal == 2) {
			//Enemy_2
			enemy_2 = Instantiate(ChooseMonster(battleLoc));
			enemyBar_2.setEnemyBar (enemy_2);
			enemy_2.transform.parent = Fighter_Space.transform;
			enemy_2.transform.localPosition = new Vector3 (-6, -11, 0);
			enemy_2.GetComponent<EnemiesScipt> ().setEnemyNum (2);
			enemyList.Add (enemy_2);
		}

		turnOrder = new List<GameObject>();
		StartCoroutine(SetupTurnOrder(turnOrder));

		yield return null;
	}

	GameObject ChooseMonster(string location) {
		float rand = Random.Range(1,3); // (inclusive, exclusive)

		switch (location) 
		{
		case "hexart_1_9": //forests
			if (rand == 1)
			{	return slimeMonster;  }
			else if (rand == 2)
			{	return wolfMonster;   }
			break;
		default:
			break;
		}
		return slimeMonster;
	}

	//Turn on Proper camera & UI
	private IEnumerator SetupUI(){
		battleInfo_Text.text = "";
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
	public IEnumerator StartBattle(List<GameObject> enemyList){
		//turnOrder = new List<GameObject>();
		//StartCoroutine(SetupTurnOrder(turnOrder));

		battleInProgress = true;
		//Loop the Battle Here
		int turnIndex = 0;
		while(battleInProgress){
			
			if (turnIndex >= turnOrder.Count)
				turnIndex = 0;
			GameObject fighter = turnOrder [turnIndex];
			Debug.Log ("Up to plate: " + fighter);
			turnIndex++;

			if (!fighter) {
				Debug.Log ("Enemy can't go [dead]");
				yield return null;
			}
			else if (fighter.tag == "Player") {
				isPlayerTurn = true;
				yield return StartCoroutine (PlayerTurn ());
				yield return StartCoroutine (checkBattleOver ());
				if (!battleInProgress)
					break;
			} else {
				yield return StartCoroutine (EnemyTurn(fighter)); // for enemy 1
				yield return StartCoroutine (checkBattleOver ());
				if (!battleInProgress)
					break;
			}
		} // end while loop
	}

	public IEnumerator SetupTurnOrder(List<GameObject> battleOrder){
		//Put in order of Speed Stat later
		battleOrder.Add (playerObj);
		foreach(GameObject enemy in enemyList){
			battleOrder.Add(enemy);
		}
			
		yield return null;
	}

	public IEnumerator PlayerTurn() {
		Debug.Log ("Players Turn");
		battleInfo_Text.text = "Vigil's Turn";
		while(isPlayerTurn){
			yield return null;
		}
		yield return null;
	}
	// For 1 enemies Turn
	public IEnumerator EnemyTurn(GameObject currentEnemy) {
		battleInfo_Text.text = "Enemy Turn";
		Debug.Log ("Enemy Attacks");
		EnemiesScipt enemyInfo = currentEnemy.GetComponent<EnemiesScipt> ();
		// Attack_1
		float damage = enemyInfo.Attack1(); // choose attack
		yield return m_turnWait;
		StartCoroutine (MovingAnim(currentEnemy, new Vector3(-2,0,0)));
		playerinfo.addHealth(damage); // example attack

		//or other Attacks...

	}

	public IEnumerator checkBattleOver(){
		if (enemyTotal <= 0 || playerinfo.getHealth () <= 0) {
			battleInProgress = false;
			battleInfo_Text.text = "Battle Over";
			Debug.Log ("You win!");
			yield return m_turnWait;
		}
	}
	#endregion

	#region Moving in Battle
	private IEnumerator MovingAnim(GameObject obj, Vector3 endPosAmt){
		yield return StartCoroutine (MoveObj(obj, obj.transform.position, obj.transform.position+endPosAmt, 0.1f)); //forward
		yield return StartCoroutine (MoveObj(obj, obj.transform.position, obj.transform.position-endPosAmt, 0.1f)); //back
	}

	private IEnumerator MoveObj(GameObject obj, Vector3 startPos, Vector3 endPos, float time){
		//int counter = 0;
		float i = 0.0f;
		float rate = 1.0f / time;
		while (i < 1.0f)
		{
			i += Time.deltaTime * rate;
			obj.transform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null;
		}
		Debug.Log ("done moving");
		yield return null;
	}
	#endregion


	#region Button Control
	//Button interactions
	public void Attack1_OnClick(){
		//deal attack 1 dmg
		if(isPlayerTurn){
			Debug.Log("You attack1");
			StartCoroutine (MovingAnim(playerObj_Battle, new Vector3(2,0,0)));
			DealDamage (playerinfo.getStrength (), targetEnemy);
			isPlayerTurn = false;
		}
	}
	public void Attack2_OnClick(){
		//deal attack 2 dmg
		if (isPlayerTurn) {
			Debug.Log ("You attack2");
			StartCoroutine (MovingAnim(playerObj_Battle, new Vector3(2,0,0)));
			playerinfo.addStamina (-20);
			DealDamage (playerinfo.getStrength()*2, targetEnemy);
			isPlayerTurn = false;
		}
	}
	public void Attack3_OnClick(){
		//deal attack 3 dmg
		if (isPlayerTurn) {
			Debug.Log ("You attack3");
			isPlayerTurn = false;
		}
	}
	public void Flee_OnClick(){
		//you attempt to run
		if (isPlayerTurn) {
			Debug.Log ("You run away");
			isPlayerTurn = false;
			battleInProgress = false;
			GameObject.FindGameObjectWithTag("enemyHealthBar_1").SetActive(false);
			GameObject.FindGameObjectWithTag("enemyHealthBar_2").SetActive(false);
		}
	}

	public void ChangeTarget_OnClick(){
		
		if (enemyTotal < 1) {
			Debug.Log ("halp");
			targetEnemy = null;
		}
		else {
			if (targetIndex + 1 < enemyList.Count){
				targetIndex ++;
				targetEnemy = enemyList[targetIndex];
			}
			else{
				targetIndex = 0;
				targetEnemy = enemyList [targetIndex];
			}
			targetIndicator.transform.localPosition = targetEnemy.transform.localPosition + new Vector3 (0,2,0);
			Debug.Log ("New Target: " + targetEnemy);
		}
	}
	#endregion

	#region Damage Control
	public void DealDamage(int damage, GameObject targEnemy){ //make enemy choice 2nd parameter
		EnemiesScipt enemyInfo = targEnemy.GetComponent<EnemiesScipt> ();

		enemyInfo.health -= damage;
		if (enemyInfo.health <= 0) {
			Destroy (targEnemy);
			enemyList.Remove(targEnemy);
			enemyTotal--;
			ChangeTarget_OnClick();
			GameObject.FindGameObjectWithTag("enemyHealthBar_" + enemyInfo.enemy_id.ToString()).SetActive(false);
		}
	}
	#endregion

}
