using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour {

	public Animator animator;

	private string levelName;

	public void LoadScene(string name) {
		levelName = name;
		animator.SetTrigger ("FadeOut");

	}

	public void OnFadeComplete () {
		SceneManager.LoadScene (sceneName: levelName);
	}

		

	public void QuitGame() {
		Application.Quit ();
	}


}
