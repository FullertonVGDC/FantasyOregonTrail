using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesScipt : MonoBehaviour {
	public int enemy_id = 0;
	public int health;
	public int maxHealth;
	public int strength;
	public int speed;
	public float evasion = 5f;
	//public GameObject hpb;
	//private GameObject healthBar;
	//Simply a List of Enemies and functions for their control
	//Class to be used with BattleManager.cs
		//public GameObject HealthBar;

	void Start(){
		//healthBar = Instantiate (hpb);
		//healthBar.transform.SetParent(GameObject.FindGameObjectWithTag ("canvas").transform, false);
	}

	void Update(){
		
	}

	public void setEnemyNum(int val){
		enemy_id = val;
	}

	public float Attack1(){
		return -strength;
	}

	public float Attack2(){
		int rand = Random.Range (1,100);
		if (rand < 20) // 20% chance to deal double dmg
			return -strength * 2;
		else
			return 0;
	}
	public void Attack3(){
		strength *= 2;
	}
	public float Attack4(){ // Should affect Stamina stat
		return -strength * 2;
	}
}
