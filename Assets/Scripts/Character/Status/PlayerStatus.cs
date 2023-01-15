using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレーヤーのステータスを管理する
public class PlayerStatus : MonoBehaviour
{
    [SerializeField] StatusBase playerStatusBase; // プレーヤーのステータスのベース
    Status status; // プレーヤーのステータス（レベルアップを反映）
    public StatusBase statusBase { get => playerStatusBase; }

    public int Level { get; set; } // レベル
    public int Exp { get; set; } // 所持経験値
    public int Money { get; set; } // 所持金

    public int MaxHp { get; set; } // 最大HP
    public int Hp { get; set; } // HP
    public int Atk { get; set; } // 攻撃力
    public int Def { get; set; } // 防御力
    public List<WeaponBase> Weapons { get; set; } // 所持武器
    public List<HealingItemBase> HealingItems { get; set; } // 所持回復アイテム

    // public List<ScriptableObject> で持ち物を管理

    private void Awake()
    {
        // 開始時の設定
        Level = 10;
        Exp = 0;
        Money = 0;
        Weapons = new List<WeaponBase>();
        HealingItems = new List<HealingItemBase>();

        // レベルに応じた基礎ステータスをゲット
        LevelStatusGet(Level);

        Debug.Log(Atk);

        // ステータス変更をダイアログに反映させる
        StatusDialogManager.Instance.DialogUpdate();
        
    }

    // レベルアップに応じたステータスを取得(from BattleSystem?)
    public void LevelStatusGet(int level)
    {
        // レベルに応じた基礎ステータスを取得
        status = new Status(playerStatusBase, level);
        MaxHp = status.MaxHp;
        Hp = status.Hp;
        Atk = status.Atk;
        Def = status.Def;        
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

    // 回復アイテムを使用する(from MenuDialogManager)
    public void UseHealingItem(int currentItemSelection)
    {
        // 使用するアイテムを指定
        HealingItemBase healingItem = HealingItems[currentItemSelection];

        // 使用したアイテムの効果をHpに足す
        Hp += healingItem.HealPoint;

        // HPが最大HPを超えていれば
        if (Hp > MaxHp)
        {
            // Hpを最大値と一致させる
            Hp = MaxHp;
        }

        // HealingItemリストから使用したアイテムを除く
        HealingItems.Remove(healingItem);
    }

    // 倒した敵から経験値、お金、アイテムを取得する (from BattleSystem)
    public void GetExpMoneyItems(EnemyStatus enemyStatus)
    {
        //
        Exp += enemyStatus.Exp;
        Money += enemyStatus.Money;
        Weapons.AddRange(enemyStatus.Weapons);
        HealingItems.AddRange(enemyStatus.HealingItems);

        DebugTextManager.Instance.text2 = "Exp: " + Exp.ToString();
        DebugTextManager.Instance.text3 = "Money: " + Money.ToString();

        // デバッグ用=>
        string weaponNames = "";
        foreach (WeaponBase weaponBase in Weapons)
        {
            string weaponName = weaponBase.name;
            weaponNames += weaponName + " ";
        }
        DebugTextManager.Instance.text4 = "Weapon: " + weaponNames;
        // <=

        // デバッグ用=>
        string healingItemNames = "";
        foreach (HealingItemBase healingItemBase in HealingItems)
        {
            string healingItemName = healingItemBase.name;
            healingItemNames += healingItemName + " ";
        }
        DebugTextManager.Instance.text5 = "HealingItem: " + healingItemNames;
        // <=

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
