using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TravelMaster : MonoBehaviour
{
    [SerializeField] private GameObject travelScreen;
    [SerializeField] private MMF_Player travelScreenInAnimation;
    [SerializeField] private MMF_Player travelScreenOutAnimation;

    [SerializeField] private bool isScreenShowed = false;

    [Header("Controls")]
    [SerializeField] private Button inventoryBtn;
    [SerializeField] private Button skillListBtn;
    [SerializeField] private Button characterBtn;
    [SerializeField] private Button diaryBtn;
    [SerializeField] private Button campBtn;

    private void Start()
    {
        inventoryBtn.onClick.AddListener(ToggleInventory);
        characterBtn.onClick.AddListener(OpenCharacterScreen);
        campBtn.onClick.AddListener(StartCamp);
    }

    public void ShowScreen()
    {
        if (!isScreenShowed)
        {
            EnableInteractionWithControls();

            ShowControlsAnimation();

            isScreenShowed = true;
        }
    }

    public void HideScreen(bool forced = false)
    {
        if (isScreenShowed)
        {
            DisableInteractionWithControls();

            HideControlsAnimation();

            CloseInventory();

            isScreenShowed = false;
        }

        if(forced)
        {
            isScreenShowed = false;
            travelScreen.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void HideControlsAnimation()
    {
        travelScreenOutAnimation?.PlayFeedbacks();
    }

    public void ShowControlsAnimation()
    {
        travelScreenInAnimation?.PlayFeedbacks();
    }

    private void DisableInteractionWithControls()
    {
        inventoryBtn.interactable = false;
        skillListBtn.interactable = false;
        characterBtn.interactable = false;
        diaryBtn.interactable = false;
        campBtn.interactable = false;
    }

    private void EnableInteractionWithControls()
    {
        inventoryBtn.interactable = true;
        skillListBtn.interactable = true;
        characterBtn.interactable = true;
        diaryBtn.interactable = true;
        campBtn.interactable = true;
    }

    private void ToggleInventory()
    {
        SOEventKeeper.Instance.GetEvent("onInventoryToggle").Raise();
    }

    private void OpenInventory()
    {
        SOEventKeeper.Instance.GetEvent("onOpenInventory").Raise();
    }

    private void CloseInventory()
    {
        SOEventKeeper.Instance.GetEvent("onCloseInventory").Raise();
    }

    private void OpenCharacterScreen()
    {
        SOEventKeeper.Instance.GetEvent("onOpenCharacterScreen").Raise();
    }

    private void StartCamp()
    {
        SOEventKeeper.Instance.AddEventToQueue("onStartCamp");
        DisableInteractionWithControls();
    }
}
