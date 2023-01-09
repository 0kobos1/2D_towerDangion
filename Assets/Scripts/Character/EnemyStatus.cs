using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// レベルに応じたエネミーのステータスを生成
// データのみを扱う（純粋なC#のクラス）
[System.Serializable]
public class EnemyStatus
{
    [SerializeField] EnemyStatusBase enemyStatusBase; // 引用元のPlayerStatusBase
    [SerializeField] int level; // プレイヤーのレベル

    public EnemyStatusBase EnemyStatusBase { get => enemyStatusBase; }

    // コンストラクタ
    public EnemyStatus(EnemyStatusBase pEnemyStatusBase, int pLevel)
    {
        enemyStatusBase = pEnemyStatusBase;
        level = pLevel;
    }

    // playerStatusBaseのステータスを取得し、レベルに応じた計算をして計算後の結果を返す
    public int MaxHP { get { return Mathf.FloorToInt((enemyStatusBase.MaxHp * level) / 100f) + 5; } }
    public int Hp { get { return Mathf.FloorToInt((enemyStatusBase.Hp * level) / 100f) + 5; } }
    public int Atk { get { return Mathf.FloorToInt((enemyStatusBase.Atk * level) / 100f) + 5; } }
    public int Def { get { return Mathf.FloorToInt((enemyStatusBase.Def * level) / 100f) + 5; } }
}
