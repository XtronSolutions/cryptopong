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
    [SerializeField] private Sprite[] Previews;

    [SerializeField] private Transform ButtonsContainer;
    [SerializeField] private LevelButton LevelButtonPrefab;
    private List<LevelButton> LevelButtons = new List<LevelButton>();

    [SerializeField] private Button BackButton;

    private void OnBackButtonPressed()
    {
        Managers.Audio.PlayClickSound();
        Managers.UI.ActivateUI(Menus.MAIN);
    }

    private int Index;

    private void OnSelect()
    {
        PlayerPrefs.SetInt("Level", Index);
        PlayerPrefs.Save();

        Managers.Audio.PlayClickSound();
        Managers.UI.ActivateUI(Menus.DIFFICULTY);
        GA_AnalyticsManager.Instance.StoredProgression.MapUsed = $"Level_{Index}";
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
        BackButton.onClick.AddListener(OnBackButtonPressed);

        Index = PlayerPrefs.GetInt("Level", Index);

        UpdateView();
        GenerateButtons();
    }

    private void GenerateButtons()
    {
        for (int i = 0; i < LevelButtons.Count; i++)
        {
            Destroy(LevelButtons[i].gameObject);
        }

        for (int i = 0; i < Levels.Length; i++)
        {
            var LevelButton = Instantiate<LevelButton>(LevelButtonPrefab, ButtonsContainer);
            LevelButtons.Add(LevelButton);

            LevelButton.Init(Previews[i], i, Index == i, (ind) =>
            {
                Managers.Audio.PlayClickSound();
                LevelButtons[Index].UpdateView(ind);
                Levels[Index].SetActive(false);
                LevelButton.UpdateView(ind);
                Index = ind;
                UpdateView();
            });
        }
    }

}
