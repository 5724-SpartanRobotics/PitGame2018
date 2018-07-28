using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D _Rb2d;
	private SpriteRenderer _SpriteRenderer;

	private bool _IsInLadder;
	private bool _IsOnLadder;

	public LadderZone Ladder;

	public bool IsInLadder
	{
		get
		{
			return _IsInLadder;
		}
		set
		{
			if (!value)
				IsOnLadder = false;
			_IsInLadder = value;
		}
	}

	public bool IsOnLadder
	{
		get
		{
			return _IsOnLadder;
		}
		set
		{
			if (value)
			{
				_Rb2d.gravityScale = 0;
				Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Ladder.IgnoredPlatform.GetComponent<Collider2D>(), true);
				Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Ladder.IgnoredPlatformBottom.GetComponent<Collider2D>(), true);
				//_Rb2d.MovePosition(new Vector2((int)(_Rb2d.position.x * 10) / 10F,
				//	(int)(_Rb2d.position.y * 10) / 10F));
			}
			else
			{
				Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Ladder.IgnoredPlatform.GetComponent<Collider2D>(), false);
				Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Ladder.IgnoredPlatformBottom.GetComponent<Collider2D>(), false);
				_Rb2d.gravityScale = 1;
			}

			_IsOnLadder = value;
		}
	}

	private bool _IsOnGround = false;

	private void Start()
	{
		_Rb2d = GetComponent<Rigidbody2D>();
		_SpriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
	}

	private float _Speed = Statics.BonusMode ? 3.5F : 1.65F;

	private void FixedUpdate()
	{
		float x = 0;
		float y = 0;

		if (Input.GetKey(KeyCode.D) || Input.GetAxisRaw("Horizontal") > 0F)
		{
			x = _Speed;
			_SpriteRenderer.flipX = false;
		}
		else if (Input.GetKey(KeyCode.A) || Input.GetAxisRaw("Horizontal") < 0F)
		{
			x = -_Speed;
			_SpriteRenderer.flipX = true;
		}

		if (Input.GetAxisRaw("Vertical") > 0 || Input.GetKey(KeyCode.Joystick1Button1))
			y = 1.0F;
		else if (Input.GetAxisRaw("Vertical") < 0 || Input.GetKey(KeyCode.Joystick1Button0))
			y = -1.0F;


		if (IsInLadder)
		{
			if (IsOnLadder)
			{
				if (x != 0)
				{
					IsOnLadder = false;
					if (y > 0)
						y = 2.1F;
				}
				_Rb2d.velocity = new Vector2(x, y);
			}
			else
			{
				if (y != 0 && x == 0)
				{
					IsOnLadder = true;
					x = 0;
					_Rb2d.velocity = new Vector2(x, y);
				}
				else
				{
					if (_IsOnGround)
					{
						if (y > 0)
							y = 2.1F;
						_Rb2d.velocity = new Vector2(x, y);
					}
				}
			}
		}
		else if (_IsOnGround)
		{
			if (y > 0)
				y = 2.1F;
			_Rb2d.velocity = new Vector2(x, y);
		}
	}

	private void OnCollisionEnter2D(Collision2D objHit)
	{
		if (objHit.gameObject.tag == "platLeft" || objHit.gameObject.tag == "platRight" || objHit.gameObject.tag == "wrench")
			_IsOnGround = true;
	}

	private void OnCollisionExit2D(Collision2D objHit)
	{
		if (objHit.gameObject.tag == "platLeft" || objHit.gameObject.tag == "platRight" || objHit.gameObject.tag == "wrench")
			_IsOnGround = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Finish")
			FindObjectOfType<GameScript>().WinGame();
	}

}
