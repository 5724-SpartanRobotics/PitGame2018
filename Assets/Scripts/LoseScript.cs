using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScript : MonoBehaviour
{
	private float _TimeElapsed;
	public Text PressAnyKeyText;

	private void Start()
	{
		PressAnyKeyText.enabled = false;
	}

	private void Update()
	{
		_TimeElapsed += Time.deltaTime;
		if (_TimeElapsed > 3F)
		{
			PressAnyKeyText.enabled = true;

			if (Input.anyKey)
				SceneManager.LoadScene("menu");
		}
	}
}
