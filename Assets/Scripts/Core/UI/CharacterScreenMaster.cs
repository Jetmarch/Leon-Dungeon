using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScreenMaster : MonoBehaviour
{
    [SerializeField] private Actor player;

    [SerializeField] private GameObject characterScreen;

    [SerializeField] private bool isCharacterScreenOpened;

    [Header("Character")]
    [SerializeField] private Image characterImage;
    [SerializeField] private Image characterBackgroundImage;

    [Header("Character stats")]
    [SerializeField] private TextMeshProUGUI strengthValue;
    [SerializeField] private TextMeshProUGUI agilityValue;
    [SerializeField] private TextMeshProUGUI intelligenceValue;

    [Header("Equipped items")]
    [SerializeField] private GameObject[] items;

    [Header("Animations")]
    [SerializeField] private MMF_Player openCharacterScreen;
    [SerializeField] private MMF_Player closeCharacterScreen;

    [Header("Controls")]
    [SerializeField] private Button closeScreenBtn;

    private void Start()
    {
        isCharacterScreenOpened = false;

        closeScreenBtn.onClick.AddListener(OnCloseScreenBtnClicked);
    }

    public void OnPlayerObjectSet(SOEventArgs e)
    {
        var eventArg = (SOEventArgOne<Actor>)e;
        player = eventArg.arg;
    }

    public void OnOpenCharacterScreen()
    {
        if (isCharacterScreenOpened) return;
        //TODO: animations
        openCharacterScreen?.PlayFeedbacks();

        strengthValue.text = player.stats.strength.ToString();
        agilityValue.text = player.stats.agility.ToString();
        intelligenceValue.text = player.stats.intelligence.ToString();

        isCharacterScreenOpened = true;
    }

    public void OnCloseCharacterScreen()
    {
        if (!isCharacterScreenOpened) return;

        closeCharacterScreen?.PlayFeedbacks();

        

        isCharacterScreenOpened = false;
    }


    public void OnToggleCharacterScreen()
    {
        if(!isCharacterScreenOpened)
        {
            OnOpenCharacterScreen();
        }
        else
        {
            OnCloseCharacterScreen();
        }
    }

    public void OnCloseScreenBtnClicked()
    {
        SOEventKeeper.Instance.GetEvent("onCloseCharacterScreen").Raise();
    }
}
