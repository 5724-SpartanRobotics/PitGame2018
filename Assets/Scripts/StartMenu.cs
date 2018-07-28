using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
	private void Start()
	{
		Statics.Lives = 3;
		Statics.BonusMode = false;
	}

	private void Update()
	{
		if (Input.anyKey)
			SceneManager.LoadScene("game");
	}

}
