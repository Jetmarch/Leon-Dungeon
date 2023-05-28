using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private Actor player;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject initiativeBar;

    public void OnPlayerObjectSet(SOEventArgs e)
    {
        var eventArg = (SOEventArgOne<Actor>)e;
        player = eventArg.arg;

        healthBar.GetComponent<Slider>().interactable = false;
        initiativeBar.GetComponent<Slider>().interactable = false;
    }

    public void OnPlayerHealthChange(SOEventArgs e)
    {
        var obj = (SOEventArgTwo<Actor, float>)e;
        if(obj.arg1 != player) return;

        float newValue = ((100 / player.healthStatus.GetMaxHealth()) * player.healthStatus.GetCurrentHealth()) / 100;
        Debug.Log(newValue);

        healthBar.GetComponent<Slider>().value = newValue;
        //TODO: Animation with floating value here
    }

    public void OnPlayerInitiativeChange(SOEventArgs e)
    {
        var obj = (SOEventArgOne<Actor>)e;
        if(obj.arg != player) return;

        initiativeBar.GetComponent<Slider>().value = player.Initiative;
    }
}
