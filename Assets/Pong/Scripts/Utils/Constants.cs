using System;
using UnityEngine;

public static class Constants
{
	public static readonly Vector2 PLAYER = new Vector2(8, -0.25f);
	public static readonly Vector2 AI = new Vector2(-8, -0.25f);
	public static readonly Vector3 PADDLE_SCALE = new Vector3(1f, 1f, 1f);
	public static readonly float PADDLE_SPEED = 15f;
	public static readonly float PADDLE_SPEED_AI = 5f;

    public static string APP_VERSION = "V1.0";
    public static string WalletAccountKey = "Account";
    public static string NameCurrency = "$Pong";
    public static string SoundKey = "Sound";
    public static string MusicKey = "Music";
    public static string CredKey = "Credentails";
    public static string MAIN_MENU_SCENE_NAME = "Gameplay";
    public static bool IsPractice = false;
    public static bool IsTournament = false;
    public static bool TournamentActive = false;

    public static bool IsTest = false; //bool to test wallet functionality inside editor
	public static bool IsTestNet = false;//bool to test code on testnet chain
    public static bool IsStagging = true;
    public static bool IsDebugBuild = false;


    public static string UserName = "";//name of the user
	public static int FlagSelectedIndex = 0;//flag selected index for avatar
	public static int TotalWins = 0; //variable to store total wins of the player
	public static int TotalScore = 0; //variable to store total score accomulation of the user
	public static string WalletAddress = ""; //stored wallet address for the connected user
	public static bool WalletConnected = false;//bool to keep track if wallet was connected or not
	public static bool WalletChanged = false;//bool to keep track if wallet was changed
	public static bool isUsingFirebaseSDK = false;//switch to enable/disable firebase sdk usage

    public static bool LoggedIn = false; //keep track of loggin from firebase
    public static bool RegisterSubmit = false;
    public static string SavedEmail = "";
    public static string SavedPass = "";
    public static string SavedConfirmPass = "";
    public static string ResendTokenID = "";
    public static string SavedUserName = "";

    public static bool IsSendConfirmation = false;
    public static bool IsResetPassword = false;
    public static string EmailSent = "";
    public static bool IsMultiplayer = false;
    public static string StoredPID = "";

    public static bool PushingTries = false;
    public static bool PushingWins = false;

    public static int DiscountPercentage = 0;
    public static int DiscountForPong = 0;
    public static bool ApprovePong = false;

    public static int LeaderboardCount = 200;//top player count for leaderboard

    public static void PrintLog(string Txt)
	{
		Debug.Log(Txt);
	}

    public static void PrintError(string Txt)
    {
        Debug.LogError(Txt);
    }

    public static void PrintExp(Exception Txt, UnityEngine.Object ins)
    {
        Debug.LogException(Txt,ins);
    }
    public static void ResetData()
    {
        WalletChanged = false;
        WalletAccountKey = "Account";
        CredKey = "Credentails";
        MAIN_MENU_SCENE_NAME = "MainMenu";
        IsPractice = false;
        IsTournament = false;
        TournamentActive = false;
        WalletConnected = false;
        WalletAddress = "";
        UserName = "XYZ";
        FlagSelectedIndex = 0;
        LoggedIn = false;
        RegisterSubmit = false;
        SavedEmail = "";
        SavedPass = "";
        SavedConfirmPass = "";
        SavedUserName = "";
    }
}

