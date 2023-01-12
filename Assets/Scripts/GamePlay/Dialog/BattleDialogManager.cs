using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// バトル中のダイアログの変更を制御する
public class BattleDialogManager : MonoBehaviour
{
    [SerializeField] GameObject statusPlayerHpBar;
    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] TextMeshProUGUI enemyNameText;

    [SerializeField] TextMeshProUGUI statusHpText;

    GameObject playerObject;
    GameObject enemyObject;
    GameObject playerHpBar; // プレイヤー上のHpBar
    GameObject enemyHpBar; // エネミー上のHpBar

    PlayerStatus playerStatus;
    EnemyStatus enemyStatus;

    Vector2 playerHpBarScale;
    Vector2 enemyHpBarScale;

    // ダイアログのセットアップ(from BattleSystem)
    public void SetUp(GameObject touchedPlayerObject, GameObject touchedEnemyObject)
    {
        // 接触したプレーヤー、敵のオブジェクトを取得
        playerObject = touchedPlayerObject;
        enemyObject = touchedEnemyObject;

        // オブジェクトについているステータスを取得
        playerStatus = playerObject.GetComponent<PlayerStatus>();
        enemyStatus = enemyObject.GetComponent<EnemyStatus>();

        // オブジェクトの子であるHpBarオブジェクトを取得
        playerHpBar = playerObject.transform.Find("HpBarCanvas/PlayerHpBar").gameObject;
        enemyHpBar = enemyObject.transform.Find("HpBarCanvas/EnemyHpBar").gameObject;

        // HpBarのスケールを取得
        playerHpBarScale = playerHpBar.GetComponent<RectTransform>().localScale;
        enemyHpBarScale = enemyHpBar.GetComponent<RectTransform>().localScale;

        // プレイヤーNameText反映
        //playerNameText.text = playerStatus.PlayerStatusBase.Name;

        // エネミーNameText反映
        //enemyNameText.text = enemyStatus.EnemyStatusBase.Name;

        // プレイヤーHpText反映
        statusHpText.text = $"HP {playerStatus.Hp}/{playerStatus.MaxHp}";

        // プレイヤーHpBar反映
        playerHpBarScale.x = (float)playerStatus.Hp / playerStatus.MaxHp;
        playerHpBar.GetComponent<RectTransform>().localScale = playerHpBarScale;

        // エネミーHpBar反映
        enemyHpBarScale.x = (float)enemyStatus.Hp / enemyStatus.MaxHp;
        enemyHpBar.GetComponent<RectTransform>().localScale = enemyHpBarScale;

        // 敵と味方のHpバーがダイアログボックスに表示される
        playerHpBar.SetActive(true);
        enemyHpBar.SetActive(true);
    }

    // 表示の更新
    public void HandleUpdate()
    {
        // プレイヤーHpText反映
        statusHpText.text = $"HP {playerStatus.Hp}/{playerStatus.MaxHp}";

        // プレイヤーHpBar反映
        playerHpBarScale.x = (float) playerStatus.Hp / playerStatus.MaxHp;
        playerHpBar.GetComponent<RectTransform>().localScale = playerHpBarScale;

        // エネミーHpBar反映
        enemyHpBarScale.x = (float) enemyStatus.Hp / enemyStatus.MaxHp;
        enemyHpBar.GetComponent<RectTransform>().localScale = enemyHpBarScale;
    }

    // 表示終了
    public void Close()
    {
        playerHpBar.SetActive(false);
        enemyHpBar.SetActive(false);
    }
}
