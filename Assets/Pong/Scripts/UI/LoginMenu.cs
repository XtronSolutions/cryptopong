using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public static partial class Events
{
    public static event Action OnLoginSuccess = null;
    public static void DoFireLoginSuccess() => OnLoginSuccess?.Invoke();
    public static event Action<string> OnLoginFailed = null;
    public static void DoFireLoginFailed(string reason) => OnLoginFailed?.Invoke(reason);

    public static event Action OnForgetPassword = null;
    public static void DoFireForgetPassword() => OnForgetPassword?.Invoke();
}

public class LoginMenu : MonoBehaviour
{
    [SerializeField] private Button LoginButton;
    [SerializeField] private Button RegisterButton;
    [SerializeField] private Button PlayAsGuestButton;
    [SerializeField] private TMP_InputField EmailField, PasswordField;
    [SerializeField] private TMP_Text StatusText;
    private Coroutine animateRoutine;
    // Start is called before the first frame update
    void Start()
    {
        this.LoginButton.onClick.AddListener(OnLoginButtonClicked);
        this.RegisterButton.onClick.AddListener(OnRegisterButtonClicked);
        this.PlayAsGuestButton.onClick.AddListener(OnPlayAsGuest);

        Events.OnLoginFailed += OnFailure;
        Events.OnLoginSuccess += OnLoginSuccess;
        Events.OnForgetPassword += OnForgetPassword;
    }

    private void OnForgetPassword()
    {
        apiRequestHandler.Instance.onForgetPassword(EmailField.text);
        Events.DoReportMessage(new messageInfo("Instructions have been sent, please check your email."));
    }

    private void OnFailure(string error)
    {
        MakeInteractable(true);
        this.StatusText.text = $"";
        StopCoroutine(animateRoutine);
        Events.DoReportMessage(new messageInfo($"Error: {error}"));
    }

    private void OnLoginSuccess()
    {
        Constants.PlayingAsGuest = false;
        MakeInteractable(true);
        this.StatusText.text = $"";
        StopCoroutine(animateRoutine);
        Managers.UI.ActivateUI(Menus.MAIN);
        AudioManager.Audio.PlayLobbyMusic();
    }

    private void OnPlayAsGuest()
    {
        Constants.PlayingAsGuest = true;

        if (FirebaseManager.Instance)
        {
            FirebaseManager.Instance.PlayerData = new UserData();
            FirebaseManager.Instance.PlayerData.UserName = "USER_" + UnityEngine.Random.Range(111, 999).ToString();
        }
        else
            Debug.LogError("FM instance is null");

        AudioManager.Audio.PlayClickSound();
        MakeInteractable(true);
        this.StatusText.text = $"";
        Managers.UI.ActivateUI(Menus.MAIN);
        AudioManager.Audio.PlayLobbyMusic();
    }

    private void MakeInteractable(bool value)
    {
        LoginButton.interactable = value;
        RegisterButton.interactable = value;
        EmailField.interactable = value;
        PasswordField.interactable = value;
        this.PlayAsGuestButton.interactable = value;
    }

    IEnumerator AnimateStatus()
    {
        var delay = 0.5f;
        yield return new WaitForSeconds(delay);
        this.StatusText.text = $"please wait.";
        yield return new WaitForSeconds(delay);
        this.StatusText.text = $"please wait..";
        yield return new WaitForSeconds(delay);
        this.StatusText.text = $"please wait...";
        yield return new WaitForSeconds(delay);
        this.StatusText.text = $"please wait";

        animateRoutine = StartCoroutine(AnimateStatus());
    }

    private void OnLoginButtonClicked()
    {
        AudioManager.Audio.PlayClickSound();

        if (!Constants.WalletConnected && !Constants.IsTest)
        {
            Events.DoReportMessage(new messageInfo($"Error: Please connect your wallet first."));
            return;
        }

        MakeInteractable(false);
        animateRoutine = StartCoroutine(AnimateStatus());
        apiRequestHandler.Instance.signInWithEmail(EmailField.text, PasswordField.text);
    }

    private void OnRegisterButtonClicked()
    {
        AudioManager.Audio.PlayClickSound();

        if (!Constants.WalletConnected && !Constants.IsTest)
        {
            Events.DoReportMessage(new messageInfo($"Error: Please connect your wallet first."));
            return;
        }

        Managers.UI.ActivateUI(Menus.REGISTER);
    }
}
