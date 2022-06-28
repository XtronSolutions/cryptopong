using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : PersistentSingleton<MainMenu>
{
    public GameObject menuButtons;
    public GameObject settingsMenu;
    public GameObject credits;
    public GameObject showAdsRewarded;
    public GameObject showAdsDefault;
    public GameObject continueButton;
    public GameObject shopMenu;
    public Text pongLogoText;

    void OnEnable()
    {
        pongLogoText.enabled = true;
        menuButtons.SetActive(true);
    }

    void OnDisable()
    {
        pongLogoText.enabled = false;
        menuButtons.SetActive(false);
    }

    public void Continue()
    {
        Managers.Audio.PlayClickSound();
        Managers.Match.RetrieveSavedMatch();
        Managers.Game.SetState(typeof(KickOffState));
        Managers.UI.ActivateUI(Menus.INGAME);
    }

    public void NewGame()
    {
        Constants.Mode = GameMode.PRACTICE;
        Managers.Audio.PlayClickSound();
        Managers.Match.Reset();
        // Managers.Match.ResetSavedGame ();
        // Managers.Game.SetState(typeof(KickOffState));
        Managers.UI.ActivateUI(Menus.LEVELS);
        GA_AnalyticsManager.Instance.StoredProgression.Mode = "Practice";
    }

    public void FreeStyle_NewGame()
    {
        Constants.Mode = GameMode.FREESTYLE;
        Managers.Audio.PlayClickSound();
        Managers.Match.Reset();
        // Managers.Match.ResetSavedGame ();
        // Managers.Game.SetState(typeof(KickOffState));
        Managers.UI.ActivateUI(Menus.LEVELS);
        GA_AnalyticsManager.Instance.StoredProgression.Mode = "FreeStyle";
    }
    public void Settings()
    {
        Managers.Audio.PlayClickSound();
        DisableMenuButtons();
        settingsMenu.SetActive(true);
    }

    public void Shop()
    {
        Managers.Audio.PlayClickSound();
        DisableMenuButtons();
        shopMenu.SetActive(true);
    }

    public void Credits()
    {
        Managers.Audio.PlayClickSound();
        DisableMenuButtons();
        credits.SetActive(true);
    }

    public void DisableMenuButtons()
    {
        menuButtons.SetActive(false);
    }

}

