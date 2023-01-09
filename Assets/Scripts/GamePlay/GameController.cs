using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ÉQÅ[ÉÄÇÃëJà⁄èÛë‘
public enum GameState
{
    FreeRoam,
    Dialog,
    Battle,
}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    GameState gameState = GameState.FreeRoam;

    private void Start()
    {
        // UnityActionÇ…ìoò^ÇÇ®Ç±Ç»Ç§
        TalkDialogManager.Instance.OnShowDialog += ShowDialog;
        TalkDialogManager.Instance.OnCloseDialog += CloseDialog;
        BattleSystem.Instance.OnBattleStart += StartBattle;

    }

    void ShowDialog()
    {
        gameState = GameState.Dialog;
    }

    void CloseDialog()
    {
        if(gameState == GameState.Dialog) 
        {
            gameState = GameState.FreeRoam;
        }
    }

    void StartBattle()
    {
        gameState = GameState.Battle;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }

        if(gameState == GameState.Dialog) 
        {
            TalkDialogManager.Instance.HandleUpdate();        
        }

        if(gameState == GameState.Battle)
        {
            BattleSystem.Instance.HandleUpdate();
        }
    }
}
