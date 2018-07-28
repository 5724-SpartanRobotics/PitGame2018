using UnityEngine;

public class FloatingText : MonoBehaviour
{
	private float _ElapsedTime = 0;

	void Update()
	{
		_ElapsedTime += Time.deltaTime;

		if (_ElapsedTime > 2F)
			Destroy(gameObject);
	}
}
