using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] GameObject battleDialogBox; // BattleDialogBoxをインスペクター登録
    [SerializeField] GameObject battleHPBox;     // BattleHPBoxをインスペクター登録

    public UnityAction OnBattleStart;
    public static BattleSystem Instance { get; private set; }

    
    private void Awake()
    {
        Instance = this;
    }

    // PlayerLayerとEnemyLayerの接触判定から呼び出される
    public void HandleStart(GameObject playerObject, GameObject enemyObject)
    {
        // GameControllerの関数が呼び出され、gameStateがBattleになる
        OnBattleStart();

        // プレイヤーと敵のHPをHPTextとHPBarに反映させる
        

        // 敵と味方のHPバーがダイアログボックスに表示される
        battleDialogBox.SetActive(true);
        battleHPBox.SetActive(true);
        
        // 自分から敵を攻撃し

        // ダメージ量がポップアップ表示され

        // 敵のHPが減少し

        // 敵から自分を攻撃し

        // ダメージ量がポップアップ表示され

        // ともにHPがのこっていれば

        // 戦闘が継続する

        // HPがのこっておらず

        // 敵のHPがゼロであれば、

        // 敵から経験値とお金、アイテムを取得
    }

    public void HandleUpdate()
    {

    }
}
