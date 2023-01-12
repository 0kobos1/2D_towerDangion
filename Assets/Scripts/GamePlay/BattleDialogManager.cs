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

    GameObject playerBattleObject;
    GameObject enemyBattleObject;
    GameObject playerHpBar; // プレイヤー上のHpBar
    GameObject enemyHpBar; // エネミー上のHpBar

    BattleStatus playerBattleStatus;
    BattleStatus enemyBattleStatus;

    Vector2 playerHpBarScale;
    Vector2 enemyHpBarScale;

    // ダイアログのセットアップ(from BattleSystem)
    public void SetUp(GameObject gotPlayerBattleObject, GameObject gotEnemyBattleObject)
    {
        playerBattleObject = gotPlayerBattleObject;
        enemyBattleObject = gotEnemyBattleObject;

        playerBattleStatus = playerBattleObject.GetComponent<BattleStatus>();
        enemyBattleStatus = enemyBattleObject.GetComponent<BattleStatus>();

        playerHpBar = playerBattleObject.transform.Find("HpBarCanvas/PlayerHpBar").gameObject;
        enemyHpBar = enemyBattleObject.transform.Find("HpBarCanvas/EnemyHpBar").gameObject;

        playerHpBarScale = playerHpBar.GetComponent<RectTransform>().localScale;
        enemyHpBarScale = enemyHpBar.GetComponent<RectTransform>().localScale;

        // プレイヤーNameText反映
        playerNameText.text = playerBattleStatus.Status.StatusBase.Name;

        // エネミーNameText反映
        enemyNameText.text = enemyBattleStatus.Status.StatusBase.Name;

        // プレイヤーHpText反映
        statusHpText.text = $"HP {playerBattleStatus.Hp}/{playerBattleStatus.Status.MaxHp}";

        // プレイヤーHpBar反映
        playerHpBarScale.x = (float)playerBattleStatus.Status.Hp / playerBattleStatus.Status.MaxHp;
        playerHpBar.GetComponent<RectTransform>().localScale = playerHpBarScale;

        // エネミーHpBar反映
        enemyHpBarScale.x = (float)enemyBattleStatus.Status.Hp / enemyBattleStatus.Status.MaxHp;
        enemyHpBar.GetComponent<RectTransform>().localScale = enemyHpBarScale;

        // 敵と味方のHpバーがダイアログボックスに表示される
        playerHpBar.SetActive(true);
        enemyHpBar.SetActive(true);
    }

    // 表示の更新
    public void HandleUpdate(BattleStatus gotPlayerBattleStatus, BattleStatus gotEnemyBattleStatus)
    {
        // プレイヤーHpText反映
        statusHpText.text = $"HP {playerBattleStatus.Hp}/{playerBattleStatus.Status.MaxHp}";

        // プレイヤーHpBar反映
        playerHpBarScale.x = (float) playerBattleStatus.Hp / playerBattleStatus.Status.MaxHp;
        playerHpBar.GetComponent<RectTransform>().localScale = playerHpBarScale;

        // エネミーHpBar反映
        enemyHpBarScale.x = (float) enemyBattleStatus.Hp / enemyBattleStatus.Status.MaxHp;
        enemyHpBar.GetComponent<RectTransform>().localScale = enemyHpBarScale;
    }

    // 表示終了
    public void Close()
    {
        playerHpBar.SetActive(false);
        enemyHpBar.SetActive(false);
    }
}
