using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LauncherSceneUIManager : MonoBehaviour
{
    public static LauncherSceneUIManager Instance;
    public GameObject multiplayerButtonPanel;
    public GameObject modesPanel;
    public GameObject mapPanel;
    public GameObject wagePanel;
    public GameObject roomPanel;
    public GameObject namePanel;
    public GameObject shopPanel;
    public GameObject controllerPanel;

    private void OnEnable()
    {
        Instance = this;
    }

    public void OnClickMultiplayerButton()
    {
        multiplayerButtonPanel.SetActive(false);
        modesPanel.SetActive(true);
    }

    public void OnClickControllerButton(int type)
    {
        Constants.ControllerIndex = type;
        controllerPanel.SetActive(false);
        mapPanel.SetActive(true);
    }

    public void OnClickFreeToPlay()
    {
        modesPanel.SetActive(false);
        controllerPanel.SetActive(true);

        Constants.ModeIndex = 0;
    }

    public void OnClickP2E()
    {
        modesPanel.SetActive(false);
        wagePanel.SetActive(true);
        Constants.ModeIndex = 1;
    }

    public void OnClickWageButton(int wageAmount)
    {
        // need to use wage amount in its prespective
        wagePanel.SetActive(false);
        controllerPanel.SetActive(true);
        Constants.BetAmount = (byte)wageAmount;
    }
    public void OnClickWageBackButton()
    {

        modesPanel.SetActive(true);
        wagePanel.SetActive(false);

    }

    public void OnCharacterSelectionNextButton()
    {
        // if (Constants.UserName == "")
        // {
        //     namePanel.SetActive(true);
        // }
        // else
        // {
        PhotonNetwork.NickName = Constants.UserName;
        shopPanel.SetActive(false);
        roomPanel.SetActive(true);
        // }

    }

    public void OnClickNamePanelPlayBUtton()
    {
        namePanel.SetActive(false);
        roomPanel.SetActive(true);

    }

    public void GoBackButton_MapSelection()
    {
        mapPanel.SetActive(false);
        controllerPanel.SetActive(true);
    }

    public void GoBackButton_ControlPanel()
    {
        controllerPanel.SetActive(false);
        modesPanel.SetActive(true);
    }

    public void GoBackButton_ShopMenu()
    {
        shopPanel.SetActive(false);
        mapPanel.SetActive(true);
    }

    public void GoBackButton_MultiplayerConnection()
    {
        PhotonLauncher.DisconnectMaster();
        roomPanel.SetActive(false);
        shopPanel.SetActive(true);
    }
}
