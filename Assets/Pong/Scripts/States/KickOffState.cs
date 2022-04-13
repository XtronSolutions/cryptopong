using UnityEngine;
using System.Collections;
using DG.Tweening;

public class KickOffState : _StatesBase
{

    private int countdown;
    private Vector2 _ballVelocity;

    #region implemented abstract members of _StatesBase

    public override void OnActivate()
    {
        Debug.Log("<color=green>KickOff State</color> OnActive");
        Managers.Game.isGameActive = true;

        countdown = 4;
        Managers.Input.isActive = false;
        Managers.UI.inGameUI.gameBackButton.gameObject.SetActive(false);

        if (!Managers.Game.isGameActive)
            Managers.Match.Reset();
        else
        {
            _ballVelocity = Managers.Match.ball.ballBody.velocity;
            Managers.Match.ball.ballBody.velocity = Vector2.zero;
        }
        CountDown();
    }

    public override void OnDeactivate()
    {
        Debug.Log("<color=red>KickOff State</color> OnDeactivate");

    }

    public override void OnUpdate()
    {
        Debug.Log("<color=yellow>KickOff State</color> OnUpdate");

    }

    #endregion


    public void CountDown()
    {
        Managers.UI.inGameUI.info.enabled = true;
        Color initColor = Managers.UI.inGameUI.info.color;
        Managers.UI.inGameUI.score.enabled = false;
        DOTween.To(() => initColor, x => Managers.UI.inGameUI.info.color = x, new Color(initColor.r, initColor.g, initColor.b, 0), 1f).SetLoops(4)
            .OnStepComplete(() =>
               {
                   countdown--;
                   Managers.Audio.PlayCollisionSound();
                   Managers.UI.inGameUI.SetInfoText(countdown.ToString(), true);
               })
            .OnComplete(() =>
               {
                   Managers.UI.inGameUI.SetInfoText("", false);
                   KickOff();
                   Managers.Audio.PlayClickSound();
                   Managers.UI.inGameUI.score.enabled = true;
                   Managers.PowUps.canSpawnPowerup = true;
                   Managers.Match.ball.ballBody.velocity = _ballVelocity;
                   StartCoroutine(Managers.PowUps.SpawnPowerup());

               });
    }


    public void KickOff()
    {
        Managers.UI.inGameUI.info.enabled = false;
        Managers.UI.inGameUI.gameBackButton.gameObject.SetActive(true);
        Managers.Input.isActive = true;
        Managers.Match.ball.KickOffBall();
        Managers.Game.SetState(typeof(GamePlayState));
    }
}
