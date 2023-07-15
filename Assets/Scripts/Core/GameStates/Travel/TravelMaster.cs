using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelMaster : MonoBehaviour
{
    [SerializeField] private GameObject travelScreen;
    [SerializeField] private MMF_Player travelScreenInAnimation;
    [SerializeField] private MMF_Player travelScreenOutAnimation;

    [SerializeField] private bool isScreenShowed = false;

    public void ShowScreen()
    {
        if (!isScreenShowed)
        {
            ShowControlsAnimation();

            isScreenShowed = true;
        }
    }

    public void HideScreen(bool forced = false)
    {
        if (isScreenShowed)
        {
            HideControlsAnimation();

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
}
