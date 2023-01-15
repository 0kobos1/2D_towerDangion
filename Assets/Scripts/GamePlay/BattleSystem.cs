using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// 修正

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleDialogManager battleDialogManager;

    GameObject playerObject; // 戦闘に参加するプレーヤーオブジェクト
    GameObject enemyObject; // 戦闘に参加するエネミーオブジェクト


    PlayerStatus playerStatus; // 戦闘開始時に接触したプレイヤーの情報
    EnemyStatus enemyStatus; // 戦闘開始時に接触した敵の情報

    public UnityAction OnBattleStart;
    public static BattleSystem Instance { get; private set; }

    float battleSpeed; // 戦闘コルーチンのwait時間(from GameController)
    bool isBattling; // 戦闘中かどうかを判定

    
    private void Awake()
    {
        Instance = this;

        // バトル速度の設定
        battleSpeed = GameController.Instance.BattleSpeed; 
    }

    // 戦闘開始時に接触したプレイヤーと敵の情報を取得する
    public void GetBattleObjects(GameObject touchedPlayerObject, GameObject touchedEnemyObject)
    {
        playerObject = touchedPlayerObject;
        enemyObject = touchedEnemyObject;
        playerStatus = touchedPlayerObject.GetComponent<PlayerStatus>();
        enemyStatus = touchedEnemyObject.GetComponent<EnemyStatus>();
    }

    // 戦闘準備（from GameController）
    public void HandleStart()
    {
        // 戦闘が開始されていれば、戦闘終了までは実行しない。
        if(isBattling == true)
        {
            return;
        }

        // 戦闘中登録
        isBattling = true;

        // GameControllerの関数が呼び出され、gameStateがBattleになる
        GameController.Instance.SetCurrentState(GameState.Battle);

        // 敵ステータスのセットアップ    
        //playerStatus.Setup();
        Debug.Log(enemyObject.name);
        enemyStatus.SetUp();

        // バトルダイアログのセットアップ
        battleDialogManager.SetUp(playerObject, enemyObject);

        // 戦闘更新への移行
        HandleUpdate();

        Debug.Log("HandleStart");
    }

    // 戦闘更新（from GameController）
    public void HandleUpdate()
    {        
        StartCoroutine(Battle());

    }

    // 戦闘コルーチン
    IEnumerator Battle()
    {
        // 繰り返す
        while (true)
        {
            yield return new WaitForSeconds(battleSpeed);

            // 自分から敵への攻撃
            enemyStatus.TakeDamage(playerStatus, enemyStatus);
            Debug.Log("プレイヤーから敵への攻撃");

            // バトルダイアログのアップデート
            StatusDialogManager.Instance.DialogUpdate();
            battleDialogManager.HandleUpdate();

            // 敵のダメージが0以下であれば
            if (enemyStatus.Hp <= 0)
            {
                Debug.Log("敵はプレイヤーに倒された");

                // 敵の経験値、お金、アイテムをPlayerSubStatusに渡す
                playerStatus.GetExpMoneyItems(enemyStatus);

                // プレーヤーのステータス変化をダイアログに反映する
                StatusDialogManager.Instance.DialogUpdate();

                // 敵のオブジェクトを非表示にする
                enemyObject.SetActive(false);

                // BattleDialogを閉じる
                battleDialogManager.Close();

                // GameStateをFreeRoamに戻す
                GameController.Instance.SetCurrentState(GameState.FreeRoam);

                // バトル終了
                isBattling = false;

                // コルーチンを抜ける
                yield break;
            }

            yield return new WaitForSeconds(battleSpeed);

            // 敵のHPが残っていれば

            // 敵から自分への攻撃
            playerStatus.TakeDamage(enemyStatus, playerStatus);
            Debug.Log("敵からプレイヤーへの攻撃");

            //// バトルダイアログのアップデート
            battleDialogManager.HandleUpdate();

            // 自分のHPが0以下であれば
            if (playerStatus.Hp <= 0)
            {
                // ゲームオーバーの処理
                GameOver();

                yield break;
            }

            yield return new WaitForSeconds(battleSpeed);

            Debug.Log("Update終了");

            yield return null;
        }
    }

    void GameOver()
    {
        // ゲームオーバーの処理
        Debug.Log("ゲームオーバー");
    }

    
}
