using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyScript : MonoBehaviour {
	public int ally_id = 0;
	public int health;
	public int maxHealth;
	public int stamina;
	public int MaxStamina;
	public int strength;
	public int speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setEnemyNum(int val){
		ally_id = val;
	}

	public void Ally1_OnClick(){
		//deal attack 1 dmg
		if(true){
			Debug.Log ("Ally attack1");
		}
	}
}
