using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelsMenu : PersistentSingleton<LevelsMenu>
{
    [SerializeField] private Text StatusText;

    [SerializeField] private Button NextButton;
    [SerializeField] private Button PrevButton;
    [SerializeField] private Button SelectButton;
    [SerializeField] private GameObject[] Levels;

    private int Index;

    private void OnSelect()
    {
        PlayerPrefs.SetInt("Level", Index);
        PlayerPrefs.Save();

        Managers.Audio.PlayClickSound();
		Managers.UI.ActivateUI (Menus.DIFFICULTY);
    }

    private void OnNext()
    {
        Levels[Index].SetActive(false);
        if (Index + 1 >= Levels.Length)
        {
            Index = 0;
        }
        else
            Index++;

        UpdateView();
        Managers.Audio.PlayClickSound();
        Debug.Log("OnNext()");
    }

    private void OnPrev()
    {
        Levels[Index].SetActive(false);
        if (Index - 1 >= 0)
        {
            Index--;
        }
        else
            Index = Levels.Length - 1;

        UpdateView();
        Managers.Audio.PlayClickSound();
        Debug.Log("OnPrev()");
    }

    private void UpdateView()
    {
        Levels[Index].SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        NextButton.onClick.AddListener(OnNext);
        PrevButton.onClick.AddListener(OnPrev);
        SelectButton.onClick.AddListener(OnSelect);

        Index = PlayerPrefs.GetInt("Level", Index);
        UpdateView();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
