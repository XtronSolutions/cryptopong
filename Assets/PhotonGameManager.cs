using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PhotonGameManager : MonoBehaviourPunCallbacks
{
    #region Public Fields

    static public PhotonGameManager Instance;

    #endregion

    #region Private Fields

    [Tooltip("The prefab to use for representing the player")]
    [SerializeField]
    private GameObject PlayerPrefab, BallPrefab;

    public Transform PlayerSpawnPointA, PlayerSpawnPointB, BallSpawnPoint;
    public RectTransform YBounds;
    public Joint2D PlayerJointA, PlayerJointB;

    public List<PhotonView> Players = new List<PhotonView>();
    [SerializeField] private GameObject[] Levels;
    #endregion

    #region MonoBehaviour CallBacks

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during initialization phase.
    /// </summary>
    void Start()
    {
        // in case we started this demo with the wrong scene being active, simply load the menu scene
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("GamePlay");
            return;
        }

        if (PlayerPrefab == null)
        {
            // #Tip Never assume public properties of Components are filled up properly, always check and inform the developer of it.
            Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            if (PhotonPlayerManager.LocalPlayerInstance == null)
            {
                Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);

                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                PhotonNetwork.Instantiate(this.PlayerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
            }
            else
            {
                Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }

            if (PhotonNetwork.IsMasterClient)
            {
                var ball = PhotonNetwork.InstantiateRoomObject(this.BallPrefab.name, BallSpawnPoint.position, Quaternion.identity, 0);
            }
        }

        Levels[(int)PhotonNetwork.CurrentRoom.CustomProperties[Constants.LEVEL_KEY]].SetActive(true);
    }

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity on every frame.
    /// </summary>
    void Update()
    {
        // "back" button of phone equals "Escape". quit app if that's pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitApplication();
        }
    }

    #endregion

    #region Photon Callbacks

    /// <summary>
    /// Called when a Photon Player got connected. We need to then load a bigger scene.
    /// </summary>
    /// <param name="other">Other.</param>
    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.Log("OnPlayerEnteredRoom() " + other.NickName); // not seen if you're the player connecting
    }

    /// <summary>
    /// Called when a Photon Player got disconnected. We need to load a smaller scene.
    /// </summary>
    /// <param name="other">Other.</param>
    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.Log("OnPlayerLeftRoom() " + other.NickName); // seen when other disconnects

        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
                LeaveRoom();
            }
        }
    }

    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("GamePlay");
    }

    #endregion

    #region Public Methods

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    #endregion

    #region Private Methods

    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }

        Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);

        PhotonNetwork.LoadLevel("PunBasics-Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public void ConcludeGame(int winner)
    {
        foreach (var p in Players)
        {
            if (p.IsMine)
            {
                var msg = "";
                if (p.CreatorActorNr == winner)
                {
                    msg = "you win!";
                    Debug.Log("I am winner");
                }
                else
                {
                    msg = "you lose!";
                    Debug.Log("I am loser");
                }

                Events.DoReportMessage(new messageInfo(msg, new System.Action(() =>
                {
                    LeaveRoom();
                }), false, false));
            }
        }
    }

    #endregion
}
