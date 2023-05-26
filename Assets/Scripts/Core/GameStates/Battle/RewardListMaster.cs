using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardListMaster : MonoBehaviour
{
    [SerializeField] private Actor player;
    [SerializeField] private Battle currentBattle;
    [SerializeField] private GameObject rewardScreen;
    [SerializeField] private GameObject rewardList;
    [SerializeField] private GameObject rewardListItemPrefab;

    [SerializeField] private Button closeBtn;

    private void Awake() 
    {
        closeBtn.onClick.AddListener(OnCloseBtnClick);
    }

    public void OnPlayerObjectSet(SOEventArgs e)
    {
        var eventArg = (SOEventArgOne<Actor>)e;
        player = eventArg.arg;
    }
    public void OnBattleUIInit(SOEventArgs e)
    {
        var eventArg = (SOEventArgOne<Battle>)e;
        this.currentBattle = eventArg.arg;

        rewardScreen.SetActive(false);
    }

    public void OnVictoryInBattle()
    {
        rewardScreen.SetActive(true);
        EmptyRewardList();
        FillRewardList();
        AddItemsToPlayerInventory();
    }

    public void OnCloseBtnClick()
    {
        rewardScreen.SetActive(false);
    }

     private void EmptyRewardList()
    {
        foreach(Transform item in rewardList.transform)
        {
            Destroy(item.gameObject);
        }
    }

    private void FillRewardList()
    {
        foreach(var item in currentBattle.loot)
        {
            var obj = Instantiate(rewardListItemPrefab, rewardList.transform);
            //Set rewardItemWrapper
            obj.GetComponent<RewardItem>().SetItem(item);
        }
    }
    
    private void AddItemsToPlayerInventory()
    {
        //In process
    }
}
