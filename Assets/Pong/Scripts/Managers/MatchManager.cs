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

public class MatchManager : MonoBehaviour
{

    public Ball ball;
    public GameObject aiPaddle;
    public GameObject playerPaddle;
    public SavedGame savedGame;

    public void Reset()
    {
        ball.ResetBall();

        playerPaddle.transform.position = Constants.PLAYER;
        aiPaddle.transform.position = Constants.AI;
        playerPaddle.transform.localScale = Constants.PADDLE_SCALE;
        aiPaddle.transform.localScale = Constants.PADDLE_SCALE;
        playerPaddle.GetComponent<Paddle>().speed = Constants.PADDLE_SPEED;
        aiPaddle.GetComponent<Paddle>().speed = Constants.PADDLE_SPEED;
    }

    public void RetrieveSavedMatch()
    {
        ball.transform.position = savedGame.ballPosition;
        playerPaddle.transform.position = savedGame.playerPosition;
        aiPaddle.transform.position = savedGame.aiPosition;
        playerPaddle.transform.localScale = savedGame.playerScale;
        aiPaddle.transform.localScale = savedGame.aiScale;

        ball.ballBody.velocity = savedGame.ballVelocity;
        playerPaddle.GetComponent<Paddle>().speed = savedGame.playerSpeed;
        aiPaddle.GetComponent<Paddle>().speed = savedGame.aiSpeed;

        Managers.Score.aiScore = savedGame.aiScore;
        Managers.Score.playerScore = savedGame.playerScore;
    }

    public void SaveMatch()
    {
        savedGame.ballPosition = ball.transform.position;
        savedGame.playerPosition = playerPaddle.transform.position;
        savedGame.aiPosition = aiPaddle.transform.position;
        savedGame.playerScale = playerPaddle.transform.localScale;
        savedGame.aiScale = aiPaddle.transform.localScale;

        savedGame.ballVelocity = ball.ballBody.velocity;
        savedGame.playerSpeed = playerPaddle.GetComponent<Paddle>().speed;
        savedGame.aiSpeed = aiPaddle.GetComponent<Paddle>().speed;

        savedGame.aiScore = Managers.Score.aiScore;
        savedGame.playerScore = Managers.Score.playerScore;

        Reset();
    }

    public void ResetSavedGame()
    {
        savedGame.ballVelocity = Vector2.zero;
        savedGame.playerPosition = Constants.PLAYER;
        savedGame.aiPosition = Constants.AI;
        savedGame.aiScore = 0;
        savedGame.playerScore = 0;
    }
}
