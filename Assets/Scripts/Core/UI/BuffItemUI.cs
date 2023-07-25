using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffItemUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Buff buff;


    public void SetBuff(Buff newBuff)
    {
        icon.sprite = newBuff.icon;
        buff = newBuff;
    }

    public Buff GetBuff()
    {
        return buff;
    }
}
