using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using TMPro;

public class BattleAnnounce : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI announceText;
    [SerializeField] private MMF_Player battleStartAnimation;
    [SerializeField] private MMF_Player victoryAnimation;
    [SerializeField] private MMF_Player defeatAnimation;

    public void OnBattleStartAnimation()
    {
        // announceText.gameObject.SetActive(true);
        // announceText.text = "BATTLE!";
        // battleStartAnimation?.PlayFeedbacks();
        EnableAnnounceTextWithTextAndPlayAnimation("BATTLE!", battleStartAnimation);
    }

    public void OnBattleStartAnimationEnd()
    {
        // announceText.gameObject.SetActive(false);
        // SOEventKeeper.Instance.GetEvent("onBattleStartAnimationEnd").Raise();
        DisableAnnounceTextAndRaiseEvent("onBattleStartAnimationEnd");
    }

    public void OnVictoryInBattle()
    {
        // announceText.gameObject.SetActive(true);
        // announceText.text = "VICTORY";
        // victoryAnimation?.PlayFeedbacks();
        EnableAnnounceTextWithTextAndPlayAnimation("VICTORY", victoryAnimation);
    }

    public void OnVictoryAnimationEnd()
    {
        //  announceText.gameObject.SetActive(false);
        // SOEventKeeper.Instance.GetEvent("onVictoryAnimationEnd").Raise();
        DisableAnnounceTextAndRaiseEvent("onVictoryAnimationEnd");
    }

    public void OnDefeatInBattle()
    {
        // announceText.gameObject.SetActive(true);
        // announceText.text = "DEFEAT";
        // victoryAnimation?.PlayFeedbacks();
        EnableAnnounceTextWithTextAndPlayAnimation("DEFEAT", defeatAnimation);
    }

    public void OnDefeatAnimationEnd()
    {
        DisableAnnounceTextAndRaiseEvent("onDefeatAnimationEnd");
    }

    private void EnableAnnounceTextWithTextAndPlayAnimation(string text, MMF_Player animation)
    {
        announceText.gameObject.SetActive(true);
        announceText.text = text;
        animation?.PlayFeedbacks();
    }

    private void DisableAnnounceTextAndRaiseEvent(string eventName)
    {
        announceText.gameObject.SetActive(false);
        SOEventKeeper.Instance.GetEvent(eventName).Raise();
    }
}
