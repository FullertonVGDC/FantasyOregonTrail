using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class PlayerScript : MonoBehaviour {

	//Variables
	public static PlayerScript savePlayerControl;

	public Vector3Int currPos;
	float health = 100;
	float stamina = 100;
	int strength = 5;
	int speed = 5;
	int renown = 0; // form of experience

	public bool[] upgrades = new bool[6];

	float max_health = 100;
	float max_stamina = 100;
	int max_speed = 5;

	// PLAYER FUNCTIONS

	#region Health Functions

	public float getMaxHealth() { return max_health; }
	public float getHealth()    { return health; }
	public void addHealth(float val){
		health += val;
		if (health > max_health)
			health = max_health;
		else if (health < 0)
			health = 0;
	}
	public void setMaxHealth(float val) { max_health = health = val; }
	public void setHealth(float val){
		health = val;
		if (health > max_health)
			health = max_health;
	}
	#endregion

	#region STAMINA Functions

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
	public void setMaxStamina(float val) { max_stamina = stamina = val; }
	public void setStamina(float val){
		stamina = val;
		if (stamina > max_stamina)
			stamina = max_stamina;
	}
	#endregion

	#region STRENGTH Functions

	public int getStrength(){
		return strength;
	}

	public void setStrength(int val){
		strength = val;
	}
	#endregion

	#region SPEED Functions

	public int getSpeed(){
		return speed;
	}

	public void setSpeed(int val){
		speed = val;
	}
	#endregion

	#region UPGRADE Functions
	// Upgrade Handling Functions
	public void boostStats(){
		// 6 possible upgrade locations
		if (currPos.x==1 && currPos.y==-2 && !upgrades[0]) {
			upgrades [0] = true;
			max_health += 20;
			health += 20; // better armor
			Debug.Log("Upgrade 1 Acquired");
		}
		else if (currPos.x==9 && currPos.y==-6 && !upgrades[1]) {
			upgrades [1] = true;
			strength += 2; // better sword
			Debug.Log("Upgrade 2 Acquired");
		}
		else if (currPos.x==5 && currPos.y==11 && !upgrades[2]) {
			upgrades [2] = true;
			health += 50; // elixir of health [NEEDS TO BE IMPLEMENTED]
			Debug.Log("Upgrade 3 Acquired");
		}
		else if (currPos.x==9 && currPos.y==14 && !upgrades[3]) {
			upgrades [3] = true;
			max_health += 30;
			health += 30; // shield
			Debug.Log("Upgrade 4 Acquired");
		}
		else if (currPos.x==7 && currPos.y==24 && !upgrades[4]) {
			upgrades [4] = true;
			max_speed += 2;
			speed += 2; // boots of speed
			Debug.Log("Upgrade 5 Acquired");
		}
		else if (currPos.x==-2 && currPos.y==25 && !upgrades[5]) {
			upgrades [5] = true;
			max_stamina += 20; 
			stamina += 20; // form fit armor
			Debug.Log("Upgrade 6 Acquired");
		}

	}
	#endregion

	#region RENOWN Functions
	// Renown Functions
	public int getRenown(){
		return renown;
	}
	public void addRenown(int val) {
		renown += val;
	}
	public void setRenown(int val){
		renown = val;
	}
	#endregion



}
