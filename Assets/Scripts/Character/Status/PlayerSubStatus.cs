using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 修正
// アイテムをゲットしたら所有物に加えられるようにする
// 次のレベルアップへの経験値必要量をレベルによって変える


// 所有経験値、所持金、持ち物などの管理
public class PlayerSubStatus : MonoBehaviour
{
    public int Level { get; private set; } // レベルアップ分のストック
    public int Exp { get; private set; }
    public int Money { get; private set; }

    // List<ScriptableObject> で持ち物を管理

    private void Start()
    {
        // レベルストック、所有経験値、所持金の初期化
        Level = 50;
        Exp = 0;  
        Money = 0;
    }

    // 倒した敵に経験値、お金、アイテムを取得する (from BattleSystem)
    public void GetExpMoneyItems(BattleStatus enemyBattleStatus)
    {
        //
        Exp += enemyBattleStatus.Status.Exp;
        Money+= enemyBattleStatus.Status.Money;
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
