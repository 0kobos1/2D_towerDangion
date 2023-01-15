using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// レベルに応じたプレイヤーのステータスを生成
// データのみを扱う（純粋なC#のクラス）
public class Status
{
    public StatusBase StatusBase { get; set; } // 引用元のStatusベース
    public int Level { get; set; }

    // コンストラクタ
    public Status(StatusBase pStatusBase, int pLevel)
    {
        StatusBase = pStatusBase;
        Level = pLevel;
    }

    // playerStatusBaseのステータスを取得し、レベルに応じた計算をして計算後の結果を返す
    public int MaxHp { get { return Mathf.FloorToInt((StatusBase.MaxHp * Level) / 100f) + 5; } }
    public int Hp { get { return Mathf.FloorToInt((StatusBase.Hp * Level) / 100f) + 5; } }
    public int Atk { get { return Mathf.FloorToInt((StatusBase.Atk * Level) / 100f) + 5; } }
    public int Def { get { return Mathf.FloorToInt((StatusBase.Def * Level) / 100f) + 5; } }

    public int Exp { get { return Mathf.FloorToInt((StatusBase.Exp * Level) / 100f) + 5; } }
    public int Money { get { return Mathf.FloorToInt((StatusBase.Money * Level) / 100f) + 5; } }

    public List<WeaponBase> Weapons { get { return StatusBase.WeaponBases; } }
    public List<HealingItemBase> HealingItems { get { return StatusBase.HealingItemBases; } }
}
