﻿//  /*********************************************************************************
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

public enum Menus
{
    MAIN,
    INGAME,
    GAMEOVER,
    DIFFICULTY,
}

public class UIManager : MonoBehaviour
{

    public MainMenu mainMenu;
    public InGameUI inGameUI;
    public DifficultyMenu DifficultyMenu;

    public void ActivateUI(Menus menutype)
    {
        inGameUI.gameObject.SetActive(menutype.Equals(Menus.INGAME));
        mainMenu.gameObject.SetActive(menutype.Equals(Menus.MAIN));
        DifficultyMenu.gameObject.SetActive(menutype.Equals(Menus.DIFFICULTY));
    }
}
