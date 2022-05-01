using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DifficultyMenu : PersistentSingleton<MonoBehaviour>
{
    [SerializeField] private Button Easy, Medium, Hard;

    public override void Awake()
    {
        base.Awake();

        Easy.onClick.AddListener(OnEasyPressed);
        Medium.onClick.AddListener(OnMediumPressed);
        Hard.onClick.AddListener(OnHardPressed);
    }

    private void OnEasyPressed()
    {
        StartGame();
        Events.DoChangeIntelligence(1);
    }

    private void OnMediumPressed()
    {
        StartGame();
        Events.DoChangeIntelligence(2);
    }

    private void OnHardPressed()
    {
        StartGame();
        Events.DoChangeIntelligence(3);
    }

    private void StartGame()
    {
        Managers.Audio.PlayClickSound();
        Managers.Match.ResetSavedGame();
        Managers.Game.SetState(typeof(KickOffState));
        Managers.UI.ActivateUI(Menus.INGAME);
        Managers.Score.playerScore = Managers.Score.aiScore = 0;
        Managers.UI.inGameUI.UpdateScore();
    }
}
