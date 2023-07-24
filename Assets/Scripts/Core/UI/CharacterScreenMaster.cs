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
    [SerializeField] private TextMeshProUGUI speedValue;
    [SerializeField] private TextMeshProUGUI sturdinessValue;
    [SerializeField] private TextMeshProUGUI mindValue;
    [SerializeField] private TextMeshProUGUI intelligenceValue;
    [SerializeField] private TextMeshProUGUI perceptionValue;
    [SerializeField] private TextMeshProUGUI luckValue;

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

        strengthValue.text = player.stats.Strength.ToString();
        agilityValue.text = player.stats.Agility.ToString();
        speedValue.text = player.stats.Speed.ToString();
        sturdinessValue.text = player.stats.Sturdiness.ToString();
        mindValue.text = player.stats.Mind.ToString();
        intelligenceValue.text = player.stats.Intelligence.ToString();
        perceptionValue.text = player.stats.Perception.ToString();
        luckValue.text = player.stats.Luck.ToString();

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
