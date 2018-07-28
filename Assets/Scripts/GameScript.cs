using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
	public Text TimerText;
	public Text PointsText;
	public GameObject Points100Template;
	private DateTime _EndTime;
	private TimeSpan _TenSecMark = TimeSpan.FromSeconds(10);
	public bool Finished = false;
	public GameObject WrenchTemplate;
	private int _Points;
	public System.Random Rand = new System.Random();
	private Player _Player;

	public Image Life1;
	public Image Life2;

	public void EndGame()
	{
		Finished = true;
		if (--Statics.Lives > 0)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else
		{
			Debug.Log("Game ended.");
			SceneManager.LoadScene("lose");
		}
	}

	void Start()
	{
		_EndTime = DateTime.UtcNow + TimeSpan.FromSeconds(45);
		if (Statics.Lives < 3)
			Life1.enabled = false;
		if (Statics.Lives < 2)
			Life2.enabled = false;
		_Player = FindObjectOfType<Player>();
	}

	private float _Time = 0;

	private void FixedUpdate()
	{
		_Time -= Time.deltaTime;
		if (_Time < 0)
		{
			if (Statics.BonusMode)
				_Time = .05F + ((float)Rand.NextDouble() * .01F);
			else
				_Time = 2F + ((float)Rand.NextDouble() * 2F);
			GameObject wrench = Instantiate(WrenchTemplate);
			wrench.SetActive(true);
			wrench.transform.position =
				new Vector2(((float)FindObjectOfType<GameScript>().Rand.NextDouble() - 0.5F) * 0.5F + wrench.transform.position.x,
				wrench.transform.position.y);
		}
	}

	private void LateUpdate()
	{
		if (!Finished)
		{
			TimeSpan remaining = _EndTime - DateTime.UtcNow;
			if (remaining <= TimeSpan.Zero)
			{
				EndGame();
				TimerText.color = Color.red;
				TimerText.text = "Time to Stop Build Day: 00.00";
			}
			else
			{
				string text = "Time to Stop Build Day: " + remaining.Seconds.ToString().PadLeft(2, '0') + ".";
				text += (remaining.Milliseconds * 100 / 1000).ToString().PadLeft(2, '0');

				if (remaining < _TenSecMark)
				{
					if (remaining.Milliseconds > 500)
						TimerText.color = Color.red;
					else
						TimerText.color = Color.white;
				}

				TimerText.text = text;
			}

			PointsText.text = "Total Points: " + _Points.ToString().PadLeft(4, '0');
		}
	}

	public void EarnJumpPoint()
	{
		Debug.Log("Earned jumping points");
		Instantiate(Points100Template, _Player.transform.position, _Player.transform.rotation).SetActive(true);
		_Points += 100;
	}

	public void WinGame()
	{
		Statics.LastScore = _Points;
		SceneManager.LoadScene("win");
	}
}
