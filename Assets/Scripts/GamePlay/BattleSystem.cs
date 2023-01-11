using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// 修正
// 敵から経験値とお金を入手できるようにする
// レベルアップできるようにする
// 戦闘コルーチンのwaitForTimeSecondを戦闘速度という形でオプション設定できるようにする
public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleDialogManager battleDialogManager;

    GameObject playerBattleObject; // 戦闘に参加するプレーヤーオブジェクト
    GameObject enemyBattleObject; // 戦闘に参加するエネミーオブジェクト

    BattleStatus playerBattleStatus; // 戦闘開始時に接触したプレイヤーの情報
    PlayerSubStatus playerSubStatus; // 戦闘開始時に接触したプレイヤーのサブ情報（経験値、所持金、所有物など）
    BattleStatus enemyBattleStatus; // 戦闘開始時に接触した敵の情報
    

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
    public void GetBattleObjects(GameObject gotPlayerObject, GameObject gotEnemyObject)
    {
        playerBattleObject = gotPlayerObject;
        enemyBattleObject = gotEnemyObject;
        playerBattleStatus = gotPlayerObject.GetComponent<BattleStatus>();
        enemyBattleStatus = gotEnemyObject.GetComponent<BattleStatus>();
        playerSubStatus = playerBattleStatus.GetComponent<PlayerSubStatus>();
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

        // バトルメンバーのセットアップ
        playerBattleStatus.SetUp();
        enemyBattleStatus.SetUp();
        
        // バトルダイアログのセットアップ
        battleDialogManager.SetUp(playerBattleObject, enemyBattleObject);

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
            enemyBattleStatus.TakeDamage(playerBattleStatus, enemyBattleStatus);
            Debug.Log("プレイヤーから敵への攻撃");

            // バトルダイアログのアップデート
            battleDialogManager.HandleUpdate(playerBattleStatus, enemyBattleStatus);

            // 敵のダメージが0以下であれば
            if (enemyBattleStatus.Hp <= 0)
            {
                Debug.Log("敵はプレイヤーに倒された");

                // 敵の経験値、お金、アイテムをPlayerSubStatusに渡す
                playerSubStatus.GetExpMoneyItems(enemyBattleStatus);

                // 敵のオブジェクトを非表示にする
                enemyBattleStatus.gameObject.SetActive(false);

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
            playerBattleStatus.TakeDamage(enemyBattleStatus, playerBattleStatus);
            Debug.Log("敵からプレイヤーへの攻撃");

            // バトルダイアログのアップデート
            battleDialogManager.HandleUpdate(playerBattleStatus, enemyBattleStatus);

            // 自分のHPが0以下であれば
            if (playerBattleStatus.Hp <= 0)
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
