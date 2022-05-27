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
    [SerializeField] private TMP_Text ErrorText;

    // Start is called before the first frame update
    void Start()
    {
        this.LoginButton.onClick.AddListener(OnLoginButtonClicked);
        this.RegisterButton.onClick.AddListener(OnRegisterButtonClicked);

        Events.OnLoginFailed += OnFailure;
        Events.OnRegisterationFailed += OnFailure;

        Events.OnLoginSuccess += OnLoginSuccess;
        Events.OnRegisterationSuccess += OnRegisterationSuccess;
    }

    private void OnFailure(string error) => this.ErrorText.text = $"Error: {error}";

    private void OnLoginSuccess() => Managers.UI.ActivateUI(Menus.MAIN);
    private void OnRegisterationSuccess() => Managers.UI.ActivateUI(Menus.MAIN);

    private void OnLoginButtonClicked()
    {
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
