using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ConsoleMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text Text;
    [SerializeField] private GameObject Parent;
    [SerializeField] private Button CloseButton;
    [SerializeField] private Button ResendButton;
    private Action Callback;

    // Start is called before the first frame update
    void Start()
    {
        Events.OnReportMessage += OnReportMessage;
        CloseButton.onClick.AddListener(OnCloseButtonClicked);
        ResendButton.onClick.AddListener(OnResendButtonClicked);
    }

    private void OnReportMessage(messageInfo info)
    {
        Parent.SetActive(true);
        Text.text = info.message;
        Callback = info.Callback;

        this.ResendButton.gameObject.SetActive(info.Resend);
    }

    private void OnCloseButtonClicked()
    {
        Parent.SetActive(false);
        Callback?.Invoke();
    }

    private void OnResendButtonClicked()
    {
        this.ResendButton.interactable = false;
        apiRequestHandler.Instance.sendVerificationAgain();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
