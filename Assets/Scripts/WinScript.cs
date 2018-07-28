using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScript : MonoBehaviour
{
	public Text ScoreText;
	public Text BonusText;
	public Text PressAnyButtonText;
	private float _TimePassed = 0F;

	void Start()
	{
		if (!Statics.BonusMode)
		{
			ScoreText.text = "Score: " + Statics.LastScore;
			BonusText.text = "You have reached Bonus Mode";
			PressAnyButtonText.text = "Press any button to continue to Bonus Mode";
		}
		else
		{
			ScoreText.text = "Bonus Mode Score: " + Statics.LastScore;
			BonusText.text = "You beat Bonus Mode!";
			PressAnyButtonText.text = "Press any button to continue to main menu";
		}

		PressAnyButtonText.enabled = false;
	}

	void Update()
	{
		_TimePassed += Time.deltaTime;

		if (_TimePassed > 3F)
		{
			PressAnyButtonText.enabled = true;

			if (Input.anyKey)
			{
				Statics.Lives = 3;
				if (!Statics.BonusMode)
				{
					Statics.BonusMode = true;
					SceneManager.LoadScene("game");
				}
				else
				{
					Statics.BonusMode = false;
					SceneManager.LoadScene("main");
				}
			}
		}
	}
}
