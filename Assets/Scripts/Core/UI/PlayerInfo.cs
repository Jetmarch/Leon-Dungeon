using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private Actor player;
    //[SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject initiativeBar;
    [SerializeField] private MMProgressBar healthBarNew;
    [SerializeField] private GameObject buffListParent;
    [SerializeField] private GameObject buffItemPrefab;

    [Header("Animations")]
    [SerializeField] private MMF_Player initiativeBarInAnimation;
    [SerializeField] private MMF_Player initiativeBarOutAnimation;


    public void OnPlayerObjectSet(SOEventArgs e)
    {
        var eventArg = (SOEventArgOne<Actor>)e;
        player = eventArg.arg;

        //healthBar.GetComponent<Slider>().interactable = false;
        initiativeBar.GetComponent<Slider>().interactable = false;
        healthBarNew.UpdateBar(player.healthStatus.GetCurrentHealth(), 0f, player.healthStatus.GetMaxHealth());
    }

    public void OnPlayerHealthChange(SOEventArgs e)
    {
        var obj = (SOEventArgTwo<Actor, float>)e;
        if(obj.arg1 != player) return;

        float newValue = ((100 / player.healthStatus.GetMaxHealth()) * player.healthStatus.GetCurrentHealth()) / 100;
        Debug.Log(newValue);

        //healthBar.GetComponent<Slider>().value = newValue;
        //TODO: Animation with floating value here

        healthBarNew.UpdateBar(player.healthStatus.GetCurrentHealth(), 0f, player.healthStatus.GetMaxHealth());
    }

    public void OnPlayerInitiativeChange(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Actor>)e;
        if(obj.arg != player) return;

        initiativeBar.GetComponent<Slider>().value = player.Initiative;
    }

    public void OnStartBattle()
    {
        initiativeBarInAnimation?.PlayFeedbacks();
    }

    public void OnInitiativeBarInAnimationEnd()
    {
        Debug.Log("InitiativeBarInAnimationEnd!");
    }

    public void OnBattleEnd()
    {
        initiativeBarOutAnimation?.PlayFeedbacks();
    }

    public void OnInitiativeBarOutAnimationEnd()
    {
        Debug.Log("InitiativeBarOutAnimationEnd!");
    }

    public void OnBuffStartAffect(SOEventArgs e)
    {
        var obj = (SOEventArgTwo<Actor, Buff>)e;
        
        if (obj.arg1 != player) return;

        UpdateBuffList();
    }

    public void OnBuffUpdateAffect(SOEventArgs e)
    {

    }

    public void OnBuffEndAffect(SOEventArgs e)
    {
        UpdateBuffList();
    }

    private void UpdateBuffList()
    {
        ClearBuffList();
        FillBuffList();
    }

    private void FillBuffList()
    {
        foreach (var buff in player.buffs)
        {
            GameObject newBuff = Instantiate(buffItemPrefab, buffListParent.transform);
            newBuff.GetComponent<BuffItemUI>().SetBuff(buff);
        }
    }

    private void ClearBuffList()
    {
        foreach(Transform obj in buffListParent.transform)
        {
            Destroy(obj.gameObject);
        }
    }
}
