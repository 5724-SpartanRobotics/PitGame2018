using UnityEngine;

public class LadderZone : MonoBehaviour
{
	public LadderZone OtherLadder;
	private bool _CollidingWithPlayer = false;
	private Player _Player;
	public GameObject IgnoredPlatform;
	public GameObject IgnoredPlatformBottom;

	private void Start()
	{
		_Player = FindObjectOfType<Player>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "player")
		{
			_CollidingWithPlayer = true;
			if (OtherLadder._CollidingWithPlayer)
			{
				_Player.Ladder = this;
				_Player.IsInLadder = true;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "player")
		{
			_CollidingWithPlayer = false;
			_Player.IsInLadder = false;
		}
	}
}
