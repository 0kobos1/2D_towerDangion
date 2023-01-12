using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] StatusBase enemyStatusBase; // プレーヤーのステータスのベース
    Status enemyStatus; // プレーヤーのステータス（レベルアップを反映）
    public StatusBase EnemyStatusBase { get => enemyStatusBase; }

    public int Level { get; set; } // レベル
    public int MaxHp { get; set; } // 最大HP
    public int Hp { get; set; } // HP
    public int Atk { get; set; } // 攻撃力
    public int Def { get; set; } // 防御力

    public int Exp { get; set; } // 所持経験値
    public int Money { get; set; } // 所持金

    // public List<ScriptableObject> で持ち物を管理

    // 戦闘開始時に敵のステータス情報が生成される
    public void SetUp()
    {
        // 開始時のレベル設定
        Level = LevelSetter();

        // レベルに応じた基礎ステータスをゲット
        LevelStatusGet(Level);
    }

    // ステージ難易度に合わせてレベルを設定する
    int LevelSetter()
    {
        // ステージ難易度を取得(from GameController)
        int stageDifficulty = GameController.Instance.StageDifficulty;

        // 乱数でレベルを変化させる
        float modifier = Random.Range(0.85f, 1f);

        // ステージの難易度に合わせてレベルを変える
        Level = (int)(stageDifficulty * modifier);

        return Level;
    }

    // レベルアップに応じたステータスを取得(from BattleSystem?)
    public void LevelStatusGet(int level)
    {
        // レベルに応じた基礎ステータスを取得
        enemyStatus = new Status(enemyStatusBase, level);

        MaxHp = enemyStatus.MaxHp;
        Hp = enemyStatus.Hp;
        Atk = enemyStatus.Atk;
        Def = enemyStatus.Def;
    }

    // 攻撃を受けた時のダメージを調べる
    public void TakeDamage(PlayerStatus attackerBattleStatus, EnemyStatus defenderBattleStatus)
    {
        // ダメージを計算する
        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attackerBattleStatus.Level + 10) / 250f;
        float d = a * ((float)attackerBattleStatus.Atk / defenderBattleStatus.Def) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        defenderBattleStatus.Hp -= damage;

        // ダメージがゼロ以下なら、
        if (defenderBattleStatus.Hp <= 0)
        {
            // ダメージをゼロにする
            defenderBattleStatus.Hp = 0;
        }
    }
}
