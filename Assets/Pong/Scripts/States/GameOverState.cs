using UnityEngine;
using System.Collections;

public class GameOverState : _StatesBase {

	#region implemented abstract members of _StatesBase
	public override void OnActivate ()
	{
		Debug.Log ("<color=green>Game Over State</color> OnActive");	
		Managers.Game.isGameActive = false;
		Managers.Anal.SendScoreAnalytic ();
		Managers.UI.inGameUI.SetInfoText ("Gameover\n" + Managers.Score.Winner.ToString ()+" won",true);
		Managers.Match.Reset ();
		if (Managers.Score.Winner == PaddleOwner.AI)
			Managers.Audio.PlayLoseSound ();
		else 
			Managers.Audio.PlayWinSound ();
		StartCoroutine (WaitIntervalGameOver ());
	}
	public override void OnDeactivate ()
	{
		Debug.Log ("<color=red>Game Over State</color> OnDeactivate");
	}

	public override void OnUpdate ()
	{
		Debug.Log ("<color=yellow>Game Over State</color> OnUpdate");
	}
	#endregion

	IEnumerator WaitIntervalGameOver()
	{
		yield return new WaitForSeconds (3);
		Managers.Game.SetState(typeof(MenuState));
	}


}
