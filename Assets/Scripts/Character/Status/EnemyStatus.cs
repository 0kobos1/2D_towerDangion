using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] StatusBase enemyStatusBase; // プレーヤーのステータスのベース
    Status status; // プレーヤーのステータス（レベルアップを反映）
    public StatusBase statusBase { get => enemyStatusBase; }

    public int Level { get; set; } // レベル
    public int MaxHp { get; set; } // 最大HP
    public int Hp { get; set; } // HP
    public int Atk { get; set; } // 攻撃力
    public int Def { get; set; } // 防御力

    public int Exp { get; set; } // 所持経験値
    public int Money { get; set; } // 所持金
    public List<WeaponBase> Weapons { get; set; } // 所持武器
    public List<HealingItemBase> HealingItems { get; set; } // 所持回復アイテム

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

    // レベルに応じたステータスを取得(from BattleSystem?)
    public void LevelStatusGet(int level)
    {
        // レベルに応じたステータスを取得
        status = new Status(enemyStatusBase, level);

        MaxHp = status.MaxHp;
        Hp = status.Hp;
        Atk = status.Atk;
        Def = status.Def;
        Exp = status.Exp;
        Money = status.Money;

        // 所持武器のベースからランダムでいくつかを手持ちに加える
        Weapons = new List<WeaponBase> { };
        Weapons = GetRandomWeapons(status);

        // 所持アイテムのベースからランダムでいくつかを手持ちに加える
        HealingItems = new List<HealingItemBase> { };
        HealingItems = GetRandomHealingItems(status);
    }

    // 所持武器のベースからランダムでいくつかを手持ちに加える
    List<WeaponBase> GetRandomWeapons(Status status)
    {
        // 所持武器の総数を計算する
        int weaponsCount = status.Weapons.Count;

        Debug.Log(weaponsCount);

        for (int i = 0; i < weaponsCount; i++)
        {
            if (Random.Range(0, 100) < 50)
            {
                Weapons.Add(status.Weapons[i]);
            }
        }
        return Weapons;
    }

    // 所持回復アイテムのベースからランダムでいくつかを手持ちに加える
    List<HealingItemBase> GetRandomHealingItems(Status status)
    {
        // 所持回復アイテムの総数を計算する
        int healingItemsCount = status.HealingItems.Count;

        Debug.Log(healingItemsCount);

        for (int i = 0; i < healingItemsCount; i++)
        {
            if (Random.Range(0, 100) < 100)
            {
                HealingItems.Add(status.HealingItems[i]);
            }
        }
        return HealingItems;
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
