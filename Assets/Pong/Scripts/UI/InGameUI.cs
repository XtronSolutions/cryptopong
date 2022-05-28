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
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{

    public Text info;
    public Text score_bot;
    public Text score_player;
    public Text player_text;
    public Button gameBackButton;

    [HideInInspector]
    public Color infoInitColor;
    [HideInInspector]
    public Color scoreInitColor;

    private void OnEnable()
    {
        var username = FirebaseManager.Instance.PlayerData.UserName;
        player_text.text = username;
    }

    void Start()
    {
        infoInitColor = info.color;
        scoreInitColor = score_bot.color;
    }

    public void UpdateScore()
    {
        score_bot.text = Managers.Score.aiScore.ToString("0");
        score_player.text = Managers.Score.playerScore.ToString("0");
    }

    public void GameInfo(string txt)
    {
        info.text = txt;
    }

    public void GameBackButtonClicked()
    {
        Events.DoFireGameStart(false);
        Managers.Audio.PlayClickSound();
        Managers.UI.ActivateUI(Menus.MAIN);
        Managers.Game.SetState(typeof(MenuState));
        Managers.Match.SaveMatch();
    }

    public void SetInfoText(string text, bool isEnabled)
    {
        Managers.UI.inGameUI.info.enabled = isEnabled;
        Managers.UI.inGameUI.info.text = text;

        if (!isEnabled)
            info.color = infoInitColor;
    }


}
