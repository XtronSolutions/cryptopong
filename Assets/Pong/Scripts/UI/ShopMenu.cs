using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI.Extensions;

public class ShopMenu : PersistentSingleton<ShopMenu>
{
    [SerializeField] private Text StatusText;

    [SerializeField] private Button NextButton;
    [SerializeField] private Button PrevButton;
    [SerializeField] private Button SelectButton;

    [SerializeField] private Animator Preview;
    [SerializeField] private Image CharacterPrefab;
    [SerializeField] private Transform CharacterContainer;
    [SerializeField] private HorizontalScrollSnap Pagination;

    private CharactersDatabase Database;

    private int Index;

    private CharactersDatabase.Character GetCharacter => Database.Characters[Index];

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        Database = Resources.Load<CharactersDatabase>(nameof(CharactersDatabase));
        Init();
    }

    private void Init()
    {
        Index = Database.GetSelectedCharacterIndex;

        for (int i = 0; i < Database.Characters.Count; i++)
        {
            var character = Instantiate(CharacterPrefab, CharacterContainer);
            character.sprite = Database.Characters[i].GetIcon;
            // character.gameObject.SetActive(Database.Characters[i].IsSelected);
            character.gameObject.SetActive(true);
        }
    }

    private IEnumerator Start()
    {
        UpdatePreview();
        NextButton.onClick.AddListener(OnNext);
        PrevButton.onClick.AddListener(OnPrev);
        SelectButton.onClick.AddListener(OnSelect);

        yield return new WaitForSeconds(0.1f);
        Pagination.ChangePage(Index);
    }

    private void OnSelect()
    {
        Database.Select(Database.Characters[Index].Key);
        StatusText.text = "EQUIPPED";
        UpdatePreview();
    }

    private void OnNext()
    {
        if (Index + 1 >= Database.Characters.Count)
        {
            Index = 0;
        }
        else
            Index++;

        UpdatePreview();
    }

    private void OnPrev()
    {
        if (Index - 1 >= 0)
        {
            Index--;
        }
        else
            Index = Database.Characters.Count - 1;

        UpdatePreview();
    }

    private void UpdatePreview()
    {
        StatusText.gameObject.SetActive(GetCharacter.IsSelected);
        SelectButton.gameObject.SetActive(!GetCharacter.IsSelected);
        Preview.runtimeAnimatorController = GetCharacter.AnimatorController;
    }
}
