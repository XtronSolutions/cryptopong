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

public class ScoreManager : MonoBehaviour {

	public int playerScore;
	public int aiScore;
	public int scoreLimit;

	public PaddleOwner Winner
	{
		get
		{ 
			return (aiScore > playerScore) ? PaddleOwner.PLAYER : PaddleOwner.AI;
		}
	}

	public void OnScore(PaddleOwner scorer)
	{	
		if (scorer == PaddleOwner.PLAYER)
		{
			playerScore++;
		} 
		else if (scorer == PaddleOwner.AI)
		{
			aiScore++;
		}

		Managers.UI.inGameUI.UpdateScore ();

		if(playerScore == scoreLimit || aiScore == scoreLimit)
			Managers.Game.SetState (typeof(GameOverState));
		else 
			Managers.Game.SetState (typeof(GoalState));
	}
}
