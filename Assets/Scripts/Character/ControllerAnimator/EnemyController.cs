using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.Events;

public enum Enemystate
{
    Idle,
    Walk,
}

public class EnemyController : MonoBehaviour
{
    

    [SerializeField] Dialog dialog;

    Character character;
    Enemystate state;
    
    float idleTimer;

    public UnityAction onBattleStart; // バトル開始時に呼び出される

    private void Awake()
    {
        state = Enemystate.Idle;
        character = GetComponent<Character>();
    }

    private void Update()
    {
        // Battleステート時はUpdateしない(全Enemyが止まる）
        if(GameController.Instance.GameState == GameState.Battle)
        {
            return;
        }

        // Menuステート時はUpdateしない(全Enemyが止まる）
        if (GameController.Instance.GameState == GameState.Menu)
        {
            return;
        }

        character.HandleUpdate();

        // ランダムな時間間隔を取得する
        float timeBetweenPattern = Random.Range(1f, 3f);

        // Idelステートの場合、
        if (state == Enemystate.Idle)
        {
            // idleTimerを更新
            idleTimer += Time.deltaTime;

            // idleTimerが時間間隔を超えていれば
            if (idleTimer > timeBetweenPattern)
            {
                // idleTimerを初期化し
                idleTimer = 0f;

                // ステートをWalkに変え
                state= Enemystate.Walk;

                // RandomWalkのコルーチンを開始
                StartCoroutine(RandomWalk());
            }
        }
    }

    // 自動ランダム歩行
    IEnumerator RandomWalk()
    {
        // -1, 0, 1のランダムな目的地を取得する
        Vector2 randomMovePos;
        ;
        randomMovePos.x = Random.Range(-1, 2);
        randomMovePos.y = Random.Range(-1, 2);

        // 移動距離がゼロなら
        if(randomMovePos == Vector2.zero)
        {
            // idleTimerを初期化
            idleTimer = 0f;

            // Idleステートに遷移
            state = Enemystate.Idle;

            // 移動コルーチンを抜ける
            yield break;
        }

        // 移動座標がゼロでないなら    
        // 斜め方向に移動しないようにする
        if (randomMovePos.x != 0)
        {
            randomMovePos.y = 0;
        }

        // Playerとの接触判定があれば
        if (TouchedPlayer(randomMovePos) != null)
        {
            // touchedPlayerに接触判定があったPlayerのゲームオブジェクトを格納
            GameObject touchedPlayer = TouchedPlayer(randomMovePos).gameObject;

            // Battleステートに遷移
            GameController.Instance.SetCurrentState(GameState.Battle);

            // 自分の情報をBattleSystemにおくる
            BattleSystem.Instance.GetBattleObjects(touchedPlayer, this.gameObject);

            // 歩行コルーチンを終了する
            yield break;
        }

        // 歩行する
        yield return character.Move(randomMovePos);


        // idleTimerを初期化
        idleTimer = 0f;

        // Idleステートに遷移
        state = Enemystate.Idle;    
    }

    // プレイヤーとの接触を調べる
    Collider2D TouchedPlayer(Vector2 moveVec)
    {
        Vector2 targetPos;
        targetPos = (Vector2)transform.position + moveVec;

        // 移動先にレイを飛ばし、EnemyLayerとの接触判定を調べる
        return Physics2D.OverlapCircle(targetPos, 0.2f, GameLayers.Instance.PlayerLayer);
    }
}
