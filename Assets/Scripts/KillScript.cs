using UnityEngine;

public class KillScript : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D objHit)
	{
		if (objHit.gameObject.tag == "wrench")
			Destroy(objHit.gameObject);
		else if (objHit.gameObject.tag == "player")
			FindObjectOfType<GameScript>().EndGame();
	}
}
