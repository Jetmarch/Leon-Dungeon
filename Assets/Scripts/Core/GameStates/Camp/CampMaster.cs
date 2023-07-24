using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampMaster : MonoBehaviour
{
    public void OnEnterCamp()
    {
        ////SOEventKeeper.Instance.AddEventToQueue("onEnterCamp");
        SOEventKeeper.Instance.GetEvent("onStartAnimationCamp").Raise();
    }

    public void OnExitCamp()
    {
        SOEventKeeper.Instance.GetEvent("onEndAnimationCamp").Raise();
    }
}
