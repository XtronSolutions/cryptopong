using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{

    public int playerScore;
    public int aiScore;
    public int scoreLimit;

    public int TotalHits;
    public int TotalWins;
    public int TotalScore => TotalHits + TotalWins;

    public PaddleOwner Winner
    {
        get
        {
            //Debug.Log("Winner");
            var playerWon = playerScore > aiScore;

            if (playerWon)
            {
                var data = FirebaseManager.Instance.PlayerData;

                data.TotalWins += 1;
                TotalWins += data.TotalWins;
                TotalHits += data.TotalHits;

                data.TotalHits = TotalHits;
                data.TotalWins = TotalWins;

                if (data.TotalScore < TotalScore)
                {
                    data.TotalScore = TotalScore;
                }

                apiRequestHandler.Instance.updatePlayerData();
            }

            TotalWins = 0;
            TotalHits = 0;
            return playerWon ? PaddleOwner.PLAYER : PaddleOwner.AI;
        }
    }

    public void OnScore(PaddleOwner scorer)
    {
        if (scorer == PaddleOwner.PLAYER)
        {
            playerScore++;
            Managers.Audio.PlayPointWinSound();
        }
        else if (scorer == PaddleOwner.AI)
        {
            aiScore++;
            Managers.Audio.PlayPointLoseSound();
        }

        Managers.UI.inGameUI.UpdateScore();

        if (playerScore == scoreLimit || aiScore == scoreLimit)
            Managers.Game.SetState(typeof(GameOverState));
        else
            Managers.Game.SetState(typeof(GoalState));
    }
}
