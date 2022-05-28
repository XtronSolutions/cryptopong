using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public static partial class Events
{
    public static Action OnLoginSuccess = null;
    public static void DoFireLoginSuccess() => OnLoginSuccess?.Invoke();
    public static Action<string> OnLoginFailed = null;
    public static void DoFireLoginFailed(string reason) => OnLoginFailed?.Invoke(reason);
}

public class LoginMenu : MonoBehaviour
{
    [SerializeField] private Button LoginButton;
    [SerializeField] private Button RegisterButton;
    [SerializeField] private TMP_InputField EmailField, PasswordField;
    [SerializeField] private TMP_Text StatusText;
    private Coroutine animateRoutine;
    // Start is called before the first frame update
    void Start()
    {
        this.LoginButton.onClick.AddListener(OnLoginButtonClicked);
        this.RegisterButton.onClick.AddListener(OnRegisterButtonClicked);

        Events.OnLoginFailed += OnFailure;
        Events.OnLoginSuccess += OnLoginSuccess;
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
        MakeInteractable(true);
        this.StatusText.text = $"";
        StopCoroutine(animateRoutine);
        Managers.UI.ActivateUI(Menus.MAIN);
    }
    
    private void MakeInteractable(bool value)
    {
        LoginButton.interactable = value;
        RegisterButton.interactable = value;
        EmailField.interactable = value;
        PasswordField.interactable = value;
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
        MakeInteractable(false);
        animateRoutine = StartCoroutine(AnimateStatus());
        apiRequestHandler.Instance.signInWithEmail(EmailField.text, PasswordField.text);
    }

    private void OnRegisterButtonClicked()
    {
        Managers.UI.ActivateUI(Menus.REGISTER);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
