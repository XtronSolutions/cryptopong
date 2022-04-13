//  /*********************************************************************************
//   *********************************************************************************
//   *********************************************************************************
//   * Produced by Skard Games										                  *
//   * Facebook: https://goo.gl/5YSrKw											      *
//   * Contact me: https://goo.gl/y5awt4								              *											
//   * Developed by Cavit Baturalp Gürdin: https://tr.linkedin.com/in/baturalpgurdin *
//   *********************************************************************************
//   *********************************************************************************
//   *********************************************************************************/

using UnityEngine;
using System.Collections;

public enum PaddleOwner
{
	PLAYER, 
	AI
}

public class Paddle : MonoBehaviour 
{
    public float speed ;
	public PaddleOwner owner;
	[HideInInspector]
	public Vector2 scale;
	int cnt =0;
	#region Private Vars
	private Vector3 screenPoint;
	private Ball _ball;
	private Rigidbody2D _rigidBody;
	public float MaxBound = 2.36f;
	#endregion

	void Start()
	{
		_ball = Managers.Match.ball;
		_rigidBody = GetComponent<Rigidbody2D>();
	}
    
	void Update()
	{

		if (Input.GetKeyDown (KeyCode.Space)){
			cnt++;
			ScreenCapture.CaptureScreenshot (cnt.ToString ()+".png");
				}
		if (owner == PaddleOwner.PLAYER)
		{
			if (Managers.Input.isActive)
			{
				if (Managers.Input.inputType == InputMethod.KeyboardInput)
					KeyboardInput ();
				else if (Managers.Input.inputType == InputMethod.TouchLRInput)
					TouchLRInput ();
				else
					DragInput ();
			}
		}
		else if (owner == PaddleOwner.AI)
		{
			AIControl ();
		}
	}

	void KeyboardInput()
	{
		float direction = Input.GetAxisRaw("Horizontal");
		CheckMovementBlock (direction);		
	}

	void TouchLRInput()
	{
		float direction = 0;

		if (Input.GetMouseButton (0)) 
		{
			direction = (Input.mousePosition.x > Screen.width / 2) ? 1 : -1;         
		}
		CheckMovementBlock (direction);		
	}

	void DragInput()
	{
		Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, 0, 0);

		Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint);
		curPosition.y = -4;
		curPosition.z = 0;
		if (Mathf.Abs (curPosition.x) > MaxBound)
			return;
		transform.position = curPosition;
	}

	void CheckMovementBlock(float dir)
	{
		float nextFramePosX = Mathf.Abs((new Vector2(dir, 0) * speed * Time.deltaTime).x + transform.position.x); 

		if ( nextFramePosX < MaxBound)
		{
			transform.Translate(new Vector2(dir, 0) * speed * Time.deltaTime);
		}     
	}

	void AIControl()
	{
		if (Mathf.Sign (transform.position.y) == Mathf.Sign (_ball.ballBody.velocity.y))
		{
			if (_ball.transform.position.x > transform.position.x +0.410f)
			{
				if (_rigidBody.velocity.x < 0)
					_rigidBody.velocity = Vector2.zero;

				_rigidBody.velocity = Vector2.right * speed;
			} 
			else if (_ball.transform.position.x < transform.position.x -0.410f )
			{
				if (_rigidBody.velocity.x > 0)
					_rigidBody.velocity = Vector2.zero;

				_rigidBody.velocity = Vector2.left * speed;
			} 
			else
			{
				_rigidBody.velocity = Vector2.zero;
			}
		} 
		else
			_rigidBody.velocity = Vector2.zero;
	}
}
