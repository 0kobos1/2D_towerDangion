using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 修正
// 〇ステージの難易度をGameControllerから取得

// 戦闘中のステータス変化(HPの変化も含む）を管理
public class BattleStatus : MonoBehaviour
{ 
    [SerializeField] StatusBase statusBase; // ベースステータス
    [SerializeField] bool isPlayer = false; // プレイヤーかどうかを判定する
    // [SerializeField] int level; // レベル

    private PlayerSubStatus playerSubStatus; // プレーヤーのサブステータス

    public Status Status { get; set; }
    public int Hp { get; set; }
    public int Level { get; set; } // レベル 
    public StatusBase StatusBase { get => statusBase;}

    public void Awake()
    {
        // プレーヤーなら
        if(isPlayer == true)
        {
            // playerSubStatusを取得
            playerSubStatus = GetComponent<PlayerSubStatus>();
        }
    }

    public void SetUp()
    {
        // プレイヤーならば
        if (isPlayer)
        {
            // レベル、現在HpをPlayerSubStatusから設定（経験値取得によるレベルアップが反映される）
            Level = playerSubStatus.Level;
            Hp = playerSubStatus.Hp;
            DebugTextManager.Instance.text1 = "PlayerLevel: " + Level.ToString();

        }

        // 敵ならば
        else
        {
            // ステージ難易度を取得(from GameController)
            int stageDifficulty = GameController.Instance.StageDifficulty;

            // 乱数でレベルを変化させる
            float modifier = Random.Range(0.85f, 1f);
            
            // ステージの難易度に合わせてレベルを変える
            Level = (int) (stageDifficulty * modifier);

            
        }

        // ステータスを生成
        Status = new Status(statusBase, Level);

        // Hpを最大値で取得する
        Hp = Status.Hp;
    }

    // 攻撃を受けた時のダメージを調べる
    public void TakeDamage(BattleStatus attackerBattleStatus, BattleStatus defenderBattleStatus)
    {
        // ダメージを計算する
        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attackerBattleStatus.Status.Level + 10) / 250f;
        float d = a * ((float)attackerBattleStatus.Status.Atk / defenderBattleStatus.Status.Def) + 2;
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

