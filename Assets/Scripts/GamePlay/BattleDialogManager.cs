using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// バトル中のダイアログの変更を制御する
public class BattleDialogManager : MonoBehaviour
{
    [SerializeField] GameObject battleDialogBox;
    [SerializeField] GameObject battleHPBox;
    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] TextMeshProUGUI enemyNameText;
    [SerializeField] TextMeshProUGUI playerHpText;
    [SerializeField] TextMeshProUGUI enemyHpText;
    [SerializeField] GameObject playerHpBar;
    [SerializeField] GameObject enemyHpBar;

    GameObject playerBattleObject;
    GameObject enemyBattleObject;

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

        playerHpBarScale = playerHpBar.GetComponent<RectTransform>().localScale;
        enemyHpBarScale = enemyHpBar.GetComponent<RectTransform>().localScale;

        // プレイヤーNameText反映
        playerNameText.text = playerBattleStatus.Status.StatusBase.Name;

        // エネミーNameText反映
        enemyNameText.text = enemyBattleStatus.Status.StatusBase.Name;

        // プレイヤーHpText反映
        playerHpText.text = $"HP {playerBattleStatus.Hp}/{playerBattleStatus.Status.MaxHp}";

        // エネミーHpText反映
        enemyHpText.text = $"HP {enemyBattleStatus.Hp}/{enemyBattleStatus.Status.MaxHp}";

        // プレイヤーHpBar反映
        playerHpBarScale.x = (float)playerBattleStatus.Status.Hp / playerBattleStatus.Status.MaxHp;
        playerHpBar.GetComponent<RectTransform>().localScale = playerHpBarScale;

        // エネミーHpBar反映
        enemyHpBarScale.x = (float)enemyBattleStatus.Status.Hp / enemyBattleStatus.Status.MaxHp;
        enemyHpBar.GetComponent<RectTransform>().localScale = enemyHpBarScale;

        // 敵と味方のHpバーがダイアログボックスに表示される
        battleDialogBox.SetActive(true);
        battleHPBox.SetActive(true);
    }

    // 表示の更新
    public void HandleUpdate(BattleStatus gotPlayerBattleStatus, BattleStatus gotEnemyBattleStatus)
    {
        // プレイヤーHpText反映
        playerHpText.text = $"HP {playerBattleStatus.Hp}/{playerBattleStatus.Status.MaxHp}";

        // プレイヤーHpBar反映
        playerHpBarScale.x = (float) playerBattleStatus.Hp / playerBattleStatus.Status.MaxHp;
        playerHpBar.GetComponent<RectTransform>().localScale = playerHpBarScale;

        // エネミーHpText反映
        enemyHpText.text = $"HP {enemyBattleStatus.Hp}/{enemyBattleStatus.Status.MaxHp}";

        // エネミーHpBar反映
        enemyHpBarScale.x = (float) enemyBattleStatus.Hp / enemyBattleStatus.Status.MaxHp;
        enemyHpBar.GetComponent<RectTransform>().localScale = enemyHpBarScale;
    }

    // 表示終了
    public void Close()
    {
        battleDialogBox.SetActive(false);
        battleHPBox.SetActive(false);
    }
}
