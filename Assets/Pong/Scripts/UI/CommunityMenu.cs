using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunityMenu : MonoBehaviour
{
    [SerializeField] private Button TwitterButton;
    [SerializeField] private Button TelegramButton;
    [SerializeField] private Button MainMenuButton;

    private void Start()
    {
        Managers._communityMenu = this;
        TwitterButton.onClick.AddListener(OnTwitterPressed);
        TelegramButton.onClick.AddListener(OnTelegramPressed);
        MainMenuButton.onClick.AddListener(OnMainMenuPressed);
    }

    public void OnTwitterPressed()
    {
        Managers.Audio.PlayClickSound();
        Application.OpenURL("https://twitter.com/PongHeroes");
    }

    public void OnTelegramPressed()
    {
        Managers.Audio.PlayClickSound();
        Application.OpenURL("https://t.me/pongheroes");
    }

    public void OnMainMenuPressed()
    {
        Managers.Audio.PlayClickSound();
        Managers.Game.SetState(typeof(MenuState));
    }
}
