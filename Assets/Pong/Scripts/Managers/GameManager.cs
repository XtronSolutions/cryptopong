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
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public static class Constants 
{
	public static readonly Vector2 PLAYER           = new  Vector2(0, -4f);
	public static readonly Vector2 AI               = new  Vector2(0,  4f);
	public static readonly Vector2 PADDLE_SCALE     = new  Vector2(0.4f,  0.1f);
	public static readonly float   PADDLE_SPEED     = 10f;
}

public class GameManager : MonoBehaviour
{
	public string debugState;
	public bool isGameActive;

	void Awake()
	{
		isGameActive = false;
	}

	private _StatesBase currentState;
	public _StatesBase State
	{
		get { return currentState; }
	}

	//Changes the current game state
	public void SetState(System.Type newStateType)
	{
		if (currentState != null)
		{
			currentState.OnDeactivate();
		}

		currentState = GetComponentInChildren(newStateType) as _StatesBase;
		if (currentState != null)
		{
			currentState.OnActivate();
		}
	}

	void Update()
	{
		if (currentState != null)
		{
			currentState.OnUpdate();
		}
	}

	void Start()
	{
		SetState(typeof(MenuState));
	}


}