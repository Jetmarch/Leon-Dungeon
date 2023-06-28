using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using TMPro;

public class BattleAnnounce : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI announceText;
    [SerializeField] private MMF_Player battleStartAnimation;
    [SerializeField] private MMF_Player battleEndAnimation;
    [SerializeField] private MMF_Player victoryAnimation;
    [SerializeField] private MMF_Player defeatAnimation;

    [SerializeField] private TextMeshProUGUI battleMessageText;
    [SerializeField] private MMF_Player battleMessageAnimation;

    private bool isVictoryOrDefeatAnimationPlayedOnce;

    public void OnBattleStartAnimation()
    {
        // announceText.gameObject.SetActive(true);
        // announceText.text = "BATTLE!";
        // battleStartAnimation?.PlayFeedbacks();

        battleStartAnimation.InitializationMode = MMFeedbacks.InitializationModes.Awake;

        EnableAnnounceTextWithTextAndPlayAnimation("BATTLE!", battleStartAnimation);
    }

    public void OnBattleStartAnimationEnd()
    {
        // announceText.gameObject.SetActive(false);
        // SOEventKeeper.Instance.GetEvent("onBattleStartAnimationEnd").Raise();
        DisableAnnounceTextAndRaiseEvent("onBattleStartAnimationEnd");
        isVictoryOrDefeatAnimationPlayedOnce = false;
    }

    public void OnVictoryInBattle()
    {
        if (isVictoryOrDefeatAnimationPlayedOnce) return;
        // announceText.gameObject.SetActive(true);
        // announceText.text = "VICTORY";
        // victoryAnimation?.PlayFeedbacks();
        victoryAnimation.InitializationMode = MMFeedbacks.InitializationModes.Awake;
        EnableAnnounceTextWithTextAndPlayAnimation("VICTORY", victoryAnimation);
        isVictoryOrDefeatAnimationPlayedOnce = true;
    }

    public void OnVictoryAnimationEnd()
    {
        //  announceText.gameObject.SetActive(false);
        // SOEventKeeper.Instance.GetEvent("onVictoryAnimationEnd").Raise();
        DisableAnnounceTextAndRaiseEvent("onVictoryAnimationEnd");
    }

    public void OnDefeatInBattle()
    {
        if (isVictoryOrDefeatAnimationPlayedOnce) return;
        // announceText.gameObject.SetActive(true);
        // announceText.text = "DEFEAT";
        // victoryAnimation?.PlayFeedbacks();
        defeatAnimation.InitializationMode = MMFeedbacks.InitializationModes.Awake;
        EnableAnnounceTextWithTextAndPlayAnimation("DEFEAT", defeatAnimation);
        isVictoryOrDefeatAnimationPlayedOnce = true;
    }

    public void OnDefeatAnimationEnd()
    {
        DisableAnnounceTextAndRaiseEvent("onDefeatAnimationEnd");
    }

    public void OnBattleEndAnimation()
    {
        battleEndAnimation?.PlayFeedbacks();
    }

    public void OnBattleEndAnimationEnd()
    {
        SOEventKeeper.Instance.GetEvent("onBattleEndAnimationEnd").Raise();
    }

    public void OnBattleMessage(SOEventArgs e)
    {
        var obj = (SOEventArgOne<string>)e;
        battleMessageText.gameObject.SetActive(true);
        battleMessageText.text = obj.arg;
        battleMessageAnimation?.PlayFeedbacks();
    }

    public void OnBattleMessageAnimationEnd()
    {
        battleMessageText.gameObject.SetActive(false);
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
