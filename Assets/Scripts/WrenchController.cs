using System;
using UnityEngine;

public class WrenchController : MonoBehaviour
{
	private Rigidbody2D _Rb2d;
	private Player _Player;
	private bool _IsOnGround = false;
	private int _Mult = 1;
	private int _UpdatesSinceGround = 0;
	private bool _Used = false;
	private GameScript _GameScript;

	void Start()
	{
		_Rb2d = GetComponent<Rigidbody2D>();
		_Player = FindObjectOfType<Player>();
		_GameScript = FindObjectOfType<GameScript>();
	}

	public void OnCollisionEnter2D(Collision2D objHit)
	{
		if (objHit.gameObject.tag == "player" && !Statics.BonusMode)
			FindObjectOfType<GameScript>().EndGame();

		if (objHit.gameObject.tag == "platLeft")
		{
			_Mult = -1;
			_IsOnGround = true;
		}
		else if (objHit.gameObject.tag == "platRight")
		{
			_Mult = 1;
			_IsOnGround = true;
		}

	}

	public void OnCollisionExit2D(Collision2D objHit)
	{
		if (objHit.gameObject.tag == "platLeft" || objHit.gameObject.tag == "platRight")
			_IsOnGround = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "dropbox")
		{
			if (FindObjectOfType<GameScript>().Rand.NextDouble() < 0.5D)
			{
				LadderZone ladder = collision.gameObject.GetComponent<Dropbox>().Ladder;
				Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), ladder.IgnoredPlatform.GetComponent<Collider2D>(), true);
				Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), ladder.IgnoredPlatformBottom.GetComponent<Collider2D>(), true);
				_Rb2d.velocity = new Vector2(0, _Rb2d.velocity.y);
			}
		}
	}

	private void FixedUpdate()
	{
		if (_IsOnGround)
		{
			_Rb2d.velocity = new Vector2(1.5F * _Mult, 0F);
			_Rb2d.angularVelocity = 0F;
			_Rb2d.rotation += _Mult * -20;
		}
		else
		{
			if (!Statics.BonusMode)
				_Rb2d.velocity = new Vector2(0, _Rb2d.velocity.y);
			if (_UpdatesSinceGround++ > 30)
			{
				if (!Statics.BonusMode)
					_Rb2d.velocity = new Vector2(1 * _Mult, 0F);
				else
					_Rb2d.velocity = new Vector2(1 * _Mult, 0.75F);
				_UpdatesSinceGround = 0;
			}
		}

		if (!_Used && Math.Abs(_Player.transform.position.x - transform.position.x) <= 0.2F)
		{
			float diff = _Player.transform.position.y - transform.position.y;

			if (diff > 0 && diff <= 0.35F)
			{
				_GameScript.EarnJumpPoint();
				_Used = true;
			}
		}
	}
}
