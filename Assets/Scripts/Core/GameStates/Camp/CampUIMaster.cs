using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampUIMaster : MonoBehaviour
{
    [SerializeField] private GameObject campScreen;

    [SerializeField] private bool isCampScreenActive;

    [Header("Controls")]
    [SerializeField] private Button cookBtn;
    [SerializeField] private Button inventoryBtn;
    [SerializeField] private Button restBtn;
    [SerializeField] private Button exitBtn;

    [Header("Animations")]
    [SerializeField] private MMF_Player showScreen;
    [SerializeField] private MMF_Player hideScreen;

    private void Awake()
    {
        exitBtn.onClick.AddListener(HideScreen);
    }

    public void ShowScreen()
    {
        if (isCampScreenActive) return;

        campScreen.SetActive(true);

        isCampScreenActive = true;
    }

    public void HideScreen()
    {
        if (!isCampScreenActive) return;

        campScreen.SetActive(false);

        isCampScreenActive = false;

        SOEventKeeper.Instance.GetEvent("onEndCamp").Raise();
    }
}
