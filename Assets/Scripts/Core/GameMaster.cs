using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [Header("Player object")]
    [SerializeField] private SOActor SOplayer;

    [Header("Game states")]
    [SerializeField] private BattleMaster battleMaster;
    [SerializeField] private TravelMaster travelMaster;
    [SerializeField] private CampMaster campMaster;
    [SerializeField] private GameState currentGameState;

    [Header("Events")]
    [SerializeField] private SOEvent setPlayerObject;

    private void Awake()
    {
        //Just for test
        Application.targetFrameRate = 60;    
    }

    private void Start()
    {
        SetPlayerObjectForAll();

        ChangeGameState(GameState.TRAVEL);
    }

    public void SetPlayerObjectForAll()
    {
        var player = new Actor(SOplayer);
        setPlayerObject.Raise(new SOEventArgOne<Actor>(player));
    }

    public void OnStartBattle(SOEventArgs e)
    {
        ChangeGameState(GameState.BATTLE);
        battleMaster.OnStartBattle(e);
    }

    public void OnBattleEndAnimationEnd()
    {
        ChangeGameState(GameState.TRAVEL);
    }

    public void ChangeGameState(GameState state)
    {
        currentGameState = state;
        switch (currentGameState)
        {
            case GameState.PAUSE:
                break;
            case GameState.TRAVEL:
                travelMaster.gameObject.SetActive(true);
                travelMaster.ShowScreen();
                battleMaster.gameObject.SetActive(false);
                campMaster.gameObject.SetActive(false);
                break;
            case GameState.BATTLE:
                travelMaster.HideScreen(true);
                battleMaster.gameObject.SetActive(true);
                campMaster.gameObject.SetActive(false);
                break;
            case GameState.CAMP:
                travelMaster.HideScreen(true);
                battleMaster.gameObject.SetActive(false);
                campMaster.gameObject.SetActive(true);
                break;
        }
    }
}

public enum GameState 
{   
    PAUSE,
    TRAVEL,
    BATTLE,
    CAMP
}
