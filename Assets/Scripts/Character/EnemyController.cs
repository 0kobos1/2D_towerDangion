using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public enum Enemystate
{
    Idle,
    Walk,
    Battle,
}

public class EnemyController : MonoBehaviour
{
    [SerializeField] Dialog dialog;
    [SerializeField] List<Vector2> movePatten;

    Character character;
    Enemystate state;

    float idleTimer;
    int currentMovePattern;

    private void Awake()
    {
        currentMovePattern = 0;
        state = Enemystate.Idle;
        character = GetComponent<Character>();
    }

    private void Update()
    {
        character.HandleUpdate();

        // ランダムな時間間隔を取得する
        float timeBetweenPattern = Random.Range(1, 3);

        // timeBetweenPatternの間隔でステートを"Walk"に変更する
        if (state == Enemystate.Idle)
        {
            // idleTimerを更新
            idleTimer += Time.deltaTime;

            // idleTimerが時間間隔を超えていれば
            if (idleTimer > timeBetweenPattern)
            {
                // idleTimerを初期化し
                idleTimer = 0f;

                // RandomWalkのコルーチンを開始
                StartCoroutine(RandomWalk());
            }
        }
    }

    // 自動ランダム歩行
    IEnumerator RandomWalk()
    {
        // ランダムな目的地を取得する
        Vector2 randomMovePos;
        ;
        randomMovePos.x = Random.Range(-1, 2);
        randomMovePos.y = Random.Range(-1, 2);

        // もし位置に変化がなければ、
        if(randomMovePos == Vector2.zero)
        {
            // Idleステートに遷移して
            state = Enemystate.Idle;
        }

        // 位置に変化があれば
        else
        {
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

                // 敵の状態をBattleに変更
                state = Enemystate.Battle;

                // 戦闘システムを立ち上げる
                BattleSystem.Instance.HandleStart(touchedPlayer, this.gameObject);
            }

            // 現在のステートをWalkに変更
            state = Enemystate.Walk;

            // 歩行する
            yield return character.Move(randomMovePos);

            // 次の移動パターンを設定する
            currentMovePattern = (currentMovePattern + 1) % movePatten.Count;

            // Idleステートに遷移
            state = Enemystate.Idle;
        }      
    }

    bool IsTouchedPlayer(Vector2 moveVec)
    {
        //移動先の座標をtargetPosに格納
        Vector2 targetPos;
        targetPos = (Vector2)transform.position + moveVec;

        // 移動先にレイを飛ばし、EnemyLayerとの接触判定を調べる
        return Physics2D.OverlapCircle(targetPos, 0.2f, GameLayers.Instance.PlayerLayer);
    }


    Collider2D TouchedPlayer(Vector2 moveVec)
    {
        Vector2 targetPos;
        targetPos = (Vector2)transform.position + moveVec;

        // 移動先にレイを飛ばし、EnemyLayerとの接触判定を調べる
        return Physics2D.OverlapCircle(targetPos, 0.2f, GameLayers.Instance.PlayerLayer);
    }
}
