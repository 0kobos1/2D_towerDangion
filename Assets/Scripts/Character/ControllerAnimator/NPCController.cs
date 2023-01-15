using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCstate
{
    Idle,
    Walk,
    Dialog,
}

public class NPCController : MonoBehaviour, IInteractable
{
    [SerializeField] Dialog dialog;
    [SerializeField] float timeBetweenPattern;
    // [SerializeField] List<Vector2> movePattern;

    Character character;
    NPCstate state;

    float idleTimer;
    //int currentMovePattern;

    private void Awake()
    {
        //currentMovePattern = 0;
        state = NPCstate.Idle;
        character = GetComponent<Character>();
    }

    private void Update()
    {
        // Battleステート時はUpdateしない(全NPCが止まる）
        if (GameController.Instance.GameState == GameState.Battle)
        {
            return;
        }

        // Menuステート時はUpdateしない(全NPCが止まる）
        if (GameController.Instance.GameState == GameState.Menu)
        {
            return;
        }

        character.HandleUpdate();

        // 2秒間隔でステートを"Walk"に変更する
        if (state == NPCstate.Idle)
        {
            idleTimer += Time.deltaTime;
            
            if(idleTimer > timeBetweenPattern)
            {
                idleTimer = 0f;

                //StartCoroutine(Walk());
                StartCoroutine(RandomWalk());
            }
        }
    }

    // パターン自動歩行
    //IEnumerator Walk()
    //{
    //    // 現在のステートをWalkに変更
    //    state = NPCstate.Walk;
        
    //    // 現在位置をoldPositionに格納する
    //    Vector3 oldPosition = transform.position;

    //    // 歩行する
    //    yield return character.Move(movePatten[currentMovePattern]);

    //    // 移動処理前のポジションと移動処理後のポジションが同じでなければ（移動していれば）
    //    if(oldPosition != transform.position)
    //    {
    //        // 次の移動パターンを設定する
    //        currentMovePattern = (currentMovePattern + 1) % movePatten.Count;
    //    }
        
    //    // Idleステートに遷移
    //    state = NPCstate.Idle;
    //}


    // 自動ランダム歩行
    IEnumerator RandomWalk()
    {
        // -1, 0, 1のランダムな目的地を取得する
        Vector2 randomMovePos;
        ;
        randomMovePos.x = Random.Range(-1, 2);
        randomMovePos.y = Random.Range(-1, 2);

        // 移動距離がゼロなら
        if (randomMovePos == Vector2.zero)
        {
            // idleTimerを初期化
            idleTimer = 0f;

            // Idleステートに遷移
            state = NPCstate.Idle;

            // 移動コルーチンを抜ける
            yield break;
        }

        // 移動座標がゼロでないなら    
        // 斜め方向に移動しないようにする
        if (randomMovePos.x != 0)
        {
            randomMovePos.y = 0;
        }

        // 歩行する
        yield return character.Move(randomMovePos);

        // 次の移動パターンを設定する
        // currentMovePattern = (currentMovePattern + 1) % movePattern.Count;

        // idleTimerを初期化
        idleTimer = 0f;

        // Idleステートに遷移
        state = NPCstate.Idle;
    }


    // Playerからの干渉時に動作
    public void Interact(Vector3 initiator)
    {
        // Idleステートのとき、
        if(state == NPCstate.Idle)
        {
            // Dialogステートに遷移
            state = NPCstate.Dialog;

            // 話しかけてきた方をむく
            character.LookTowards(initiator);

            // NPCの持つダイアログを表示させる
            StartCoroutine(MessageDialogManager.Instance.ShowDialog(dialog, OnDialogFinished));
        }
    }

    // 会話が終わったらIdleステートに戻す
    void OnDialogFinished()
    {
        // Idleの初期時点からスタートするようにタイマーを初期化
        idleTimer = 0f;
        state = NPCstate.Idle;
    }
}
