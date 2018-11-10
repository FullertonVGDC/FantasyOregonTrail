using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {


	public Vector3Int currPos;
	float health = 100;
	float stamina = 100;
	int strength = 10;
	int speed = 5;

	float max_health = 100;
	float max_stamina = 100;
	int max_speed = 5;

	//Health Functions
	public float getMaxHealth(){
		return max_health;
	}
	public float getHealth(){
		return health;
	}
	public void addHealth(float val){
		health += val;
	}
	public void setHealth(float val){
		health = val;
	}

	//Stamina Functions
	public float getMaxStamina(){
		return max_stamina;
	}
	public float getStamina(){
		return stamina;
	}
	public void addStamina(float val){
		stamina += val;
	}
	public void setStamina(float val){
		stamina = val;
	}
}
