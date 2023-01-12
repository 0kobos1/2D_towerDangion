using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレーヤーのステータスを管理する
public class PlayerStatus : MonoBehaviour
{
    [SerializeField] StatusBase playerStatusBase; // プレーヤーのステータスのベース
    Status playerStatus; // プレーヤーのステータス（レベルアップを反映）
    public StatusBase PlayerStatusBase { get => playerStatusBase; }

    public int Level { get; set; } // レベル
    public int MaxHp { get; set; } // 最大HP
    public int Hp { get; set; } // HP
    public int Atk { get; set; } // 攻撃力
    public int Def { get; set; } // 防御力

    public int Exp { get; set; } // 所持経験値
    public int Money { get; set; } // 所持金

    // public List<ScriptableObject> で持ち物を管理

    private void Awake()
    {
        // 開始時のレベル設定
        Level = 10;

        // レベルに応じた基礎ステータスをゲット
        LevelStatusGet(Level);
    }

    // レベルアップに応じたステータスを取得(from BattleSystem?)
    public void LevelStatusGet(int level)
    {
        // レベルに応じた基礎ステータスを取得
        playerStatus = new Status(playerStatusBase, level);

        MaxHp = playerStatus.MaxHp;
        Hp = playerStatus.Hp;
        Atk = playerStatus.Atk;
        Def = playerStatus.Def;

        // ステータス変更をダイアログに反映させる

    }


    // 攻撃を受けた時のダメージを調べる
    public void TakeDamage(EnemyStatus attackerStatus, PlayerStatus defenderStatus)
    {
        // ダメージを計算する
        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attackerStatus.Level + 10) / 250f;
        float d = a * ((float)attackerStatus.Atk / defenderStatus.Def) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        defenderStatus.Hp -= damage;

        // ダメージがゼロ以下なら、
        if (defenderStatus.Hp <= 0)
        {
            // ダメージをゼロにする
            defenderStatus.Hp = 0;
        }
    }
    // 倒した敵から経験値、お金、アイテムを取得する (from BattleSystem)
    public void GetExpMoneyItems(EnemyStatus enemyStatus)
    {
        //
        Exp += enemyStatus.Exp;
        Money += enemyStatus.Money;

        DebugTextManager.Instance.text2 = "Exp: " + Exp.ToString();
        DebugTextManager.Instance.text3 = "Money: " + Money.ToString();



        // 経験値が次のレベルアップへの必要経験値を超えていれば（ここでは100）
        if (Exp >= 100)
        {
            // Levelを一つ上げる
            Level++;
            DebugTextManager.Instance.text1 = "PlayerLevel: " + Level.ToString();

            // Expから100を引く
            Exp -= 100;
            DebugTextManager.Instance.text2 = "Exp: " + Exp.ToString();
        }
    }

}
