using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {


	public Vector3Int currPos;
	float health = 100;
	float stamina = 100;
	int strength = 5;
	int speed = 5;

	public int totalBuffs = 0; //for demo
	public bool healthBoost = false; //for demo
	public bool strengthBoost = false; //for demo

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
		if (health > max_health)
			health = max_health;
		else if (health < 0)
			health = 0;
	}
	public void setHealth(float val){
		health = val;
		if (health > max_health)
			health = max_health;
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
		if (stamina > max_stamina)
			stamina = max_stamina;
		else if (stamina < 0)
			stamina = 0;
	}
	public void setStamina(float val){
		stamina = val;
		if (stamina > max_stamina)
			stamina = max_stamina;
	}

	// Strength Functions
	public int getStrength(){
		return strength;
	}

	// Speed Functions
	public int getSpeed(){
		return speed;
	}

	public void boostStats(int stat){
		if (!healthBoost && currPos.x==1 && currPos.y==-2) {
			healthBoost = true;
			max_health = 120;
			health = 120;
			totalBuffs++;
		}
		if (!strengthBoost && currPos.x==9 && currPos.y==-6) {
			strengthBoost = true;
			strength = 7;
			totalBuffs++;
		}
	}
}
