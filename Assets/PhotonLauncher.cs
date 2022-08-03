using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using ExitGames.Client.Photon;
public class PhotonLauncher : MonoBehaviourPunCallbacks
{
    private static PhotonLauncher instance;

    #region Private Serializable Fields

    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    [SerializeField]
    private GameObject controlPanel;

    [Tooltip("The Ui Text to inform the user about the connection progress")]
    [SerializeField]
    private Text feedbackText;

    [Tooltip("The maximum number of players per room")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;

    [Tooltip("The UI Loader Anime")]
    [SerializeField]
    private LoaderAnime loaderAnime;

    [SerializeField] private CharacterInfo[] CharactersLobby;

    #endregion

    #region Private Fields
    /// <summary>
    /// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon, 
    /// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
    /// Typically this is used for the OnConnectedToMaster() callback.
    /// </summary>
    bool isConnecting;

    /// <summary>
    /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
    /// </summary>
    string gameVersion = "1";

    public static Hashtable GetCustomProperties()
    {
        var customProperties = new Hashtable();
        customProperties["maxScores"] = 5;
        customProperties["mode"] = Constants.ModeIndex;
        customProperties["level"] = Constants.LevelIndex;
        customProperties["controller"] = Constants.ControllerIndex;
        customProperties["bet"] = Constants.BetAmount;
        Debug.Log(customProperties.ToStringFull());
        return customProperties;
    }

    public static void ConnectMaster() => instance.Connect();
    public static void DisconnectMaster() => PhotonNetwork.Disconnect();

    #endregion

    #region MonoBehaviour CallBacks

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    /// </summary>
    void Awake()
    {
        if (loaderAnime == null)
        {
            Debug.LogError("<Color=Red><b>Missing</b></Color> loaderAnime Reference.", this);
        }

        instance = this;

        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;

    }

    #endregion


    #region Public Methods

    /// <summary>
    /// Start the connection process. 
    /// - If already connected, we attempt joining a random room
    /// - if not yet connected, Connect this application instance to Photon Cloud Network
    /// </summary>
    public void Connect()
    {
        // we want to make sure the log is clear everytime we connect, we might have several failed attempted if connection failed.
        feedbackText.text = "";

        // keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
        isConnecting = true;

        // hide the Play button for visual consistency
        controlPanel.SetActive(false);

        // start the loader animation for visual effect.
        if (loaderAnime != null)
        {
            loaderAnime.StartLoaderAnimation();
        }

        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {
            LogFeedback("Joining Room...");
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {

            LogFeedback("Connecting...");

            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = this.gameVersion;
        }
    }

    public void Return()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
            UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
        }
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
    }

    /// <summary>
    /// Logs the feedback in the UI view for the player, as opposed to inside the Unity Editor for the developer.
    /// </summary>
    /// <param name="message">Message.</param>
    void LogFeedback(string message)
    {
        // we do not assume there is a feedbackText defined.
        if (feedbackText == null)
        {
            return;
        }

        // add new messages as a new line and at the bottom of the log.
        //feedbackText.text += System.Environment.NewLine + message;
        feedbackText.text = message;
    }

    #endregion


    #region MonoBehaviourPunCallbacks CallBacks
    // below, we implement some callbacks of PUN
    // you can find PUN's callbacks in the class MonoBehaviourPunCallbacks


    /// <summary>
    /// Called after the connection to the master is established and authenticated
    /// </summary>
    public override void OnConnectedToMaster()
    {
        // we don't want to do anything if we are not attempting to join a room. 
        // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
        // we don't want to do anything.
        if (isConnecting)
        {
            LogFeedback("Connected with master");
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room.\n Calling: PhotonNetwork.JoinRandomRoom(); Operation will fail if no room found");

            var customProperties = GetCustomProperties();
            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            PhotonNetwork.JoinRandomRoom();//customProperties, (byte)maxPlayersPerRoom);
        }
    }

    /// <summary>
    /// Called when a JoinRandom() call failed. The parameter provides ErrorCode and message.
    /// </summary>
    /// <remarks>
    /// Most likely all rooms are full or no rooms are available. <br/>
    /// </remarks>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //LogFeedback("<Color=Red>OnJoinRandomFailed</Color>: Next -> Create a new Room");
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        var options = new RoomOptions();
        options.MaxPlayers = this.maxPlayersPerRoom;

        options.CustomRoomProperties = GetCustomProperties();

        PhotonNetwork.CreateRoom("", options);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        // #Critical: We only load if we are the first player, else we rely on  PhotonNetwork.AutomaticallySyncScene to sync our instance scene.
        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersPerRoom)
        {
            feedbackText.text = " Starting game...";
            //feedbackText.text += System.Environment.NewLine + " Starting game...";

            // #Critical
            // Load the Room Level. 
            if (PhotonNetwork.IsMasterClient)
                Invoke(nameof(LoadArena), 3f);
        }
        else
        {
            feedbackText.text = " Waiting for 1 more player to join.";
            //feedbackText.text += System.Environment.NewLine + " Waiting for 1 more player to join.";
        }

    }

    /// <summary>
    /// Called after disconnecting from the Photon server.
    /// </summary>
    public override void OnDisconnected(DisconnectCause cause)
    {
        LogFeedback("<Color=Red>OnDisconnected</Color> " + cause);
        Debug.LogError("PUN Basics Tutorial/Launcher:Disconnected");

        // #Critical: we failed to connect or got disconnected. There is not much we can do. Typically, a UI system should be in place to let the user attemp to connect again.
        loaderAnime.StopLoaderAnimation();

        isConnecting = false;
        feedbackText.text = "";
        controlPanel.SetActive(true);
        // UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
    }

    /// <summary>
    /// Called when entering a room (by creating or joining it). Called on all clients (including the Master Client).
    /// </summary>
    /// <remarks>
    /// This method is commonly used to instantiate player characters.
    /// If a match has to be started "actively", you can call an [PunRPC](@ref PhotonView.RPC) triggered by a user's button-press or a timer.
    ///
    /// When this is called, you can usually already access the existing players in the room via PhotonNetwork.PlayerList.
    /// Also, all custom properties should be already available as Room.customProperties. Check Room..PlayerCount to find out if
    /// enough players are in the room to start playing.
    /// </remarks>
    public override void OnJoinedRoom()
    {
        LogFeedback("<Color=Green>OnJoinedRoom</Color> with " + PhotonNetwork.CurrentRoom.PlayerCount + " Player(s)");
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.\nFrom here on, your game would be running.");
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            // Add a button for each player in the room.
            // You can use p.NickName to access the player's nick name.
            CharactersLobby[p.ActorNumber - 1].gameObject.SetActive(true);
            CharactersLobby[p.ActorNumber - 1].chaarcterName.text = p.NickName;
            CharactersLobby[p.ActorNumber - 1].winRate.text = "0";//((int)p.CustomProperties["winrate"]).ToString();
            CharactersLobby[p.ActorNumber - 1].characterImage.runtimeAnimatorController = Databases.CharactersDatabase.GetCharacterOfIndex((int)p.CustomProperties["character"]).AnimatorController;
        }

        // #Critical: We only load if we are the first player, else we rely on  PhotonNetwork.AutomaticallySyncScene to sync our instance scene.
        if (PhotonNetwork.CurrentRoom.PlayerCount < maxPlayersPerRoom)
        {
            feedbackText.text = " Waiting for 1 more player to join.";
            //feedbackText.text += System.Environment.NewLine + " Waiting for 1 more player to join.";
        }
        else
        {
            feedbackText.text = " Starting Game...";
            //feedbackText.text += System.Environment.NewLine + " Starting Game...";
        }
    }

    private void LoadArena()
    {
        PhotonNetwork.LoadLevel("PhotonGamePlay");
    }

    #endregion

}
