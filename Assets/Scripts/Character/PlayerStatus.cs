using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// レベルに応じたプレイヤーのステータスを生成
// データのみを扱う（純粋なC#のクラス）
[System.Serializable]
public class PlayerStatus
{
    [SerializeField] PlayerStatusBase playerStatusBase; // 引用元のPlayerStatusBase
    [SerializeField] int level; // プレイヤーのレベル

    public PlayerStatusBase PlayerStatusBase { get => playerStatusBase; }

    // コンストラクタ
    public PlayerStatus(PlayerStatusBase pPlayerStatusBase, int pLevel)
    {
        playerStatusBase = pPlayerStatusBase;
        level= pLevel;
    }

    // playerStatusBaseのステータスを取得し、レベルに応じた計算をして計算後の結果を返す
    public int MaxHP { get { return Mathf.FloorToInt((playerStatusBase.MaxHp * level) / 100f) + 5; } }
    public int Hp { get { return Mathf.FloorToInt((playerStatusBase.Hp * level) / 100f) + 5; } }
    public int Atk { get { return Mathf.FloorToInt((playerStatusBase.Atk * level) / 100f) + 5; } }
    public int Def { get { return Mathf.FloorToInt((playerStatusBase.Def * level) / 100f) + 5; } }
    
}
