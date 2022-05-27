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
    private Action Callback;

    // Start is called before the first frame update
    void Start()
    {
        Events.OnReportMessage += OnReportMessage;
        CloseButton.onClick.AddListener(OnCloseButtonClicked);
    }

    private void OnReportMessage(messageInfo info)
    {
        Parent.SetActive(true);
        Text.text = info.message;
        Callback = info.Callback;
    }

    private void OnCloseButtonClicked()
    {
        Parent.SetActive(false);
        Callback?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
