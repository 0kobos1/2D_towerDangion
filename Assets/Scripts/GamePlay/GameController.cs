using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// 修正
// playerの現在のステータスを保存できるスクリプトを作成し、BattleSystemでそちらからデータを取得するようにする。
// 戦闘が開始したらプレイヤーとエネミーの上にHPが表示される
// 攻撃を受けたらダメージのポップアップが出る
// 〇プレイヤーが移動しようとした場所が統括リストから削除されない
// クラス図の作成
// 戦闘が終わってもプレイヤーのHPなどがひきつがれるようにする。
// ミニマップの作成（左上）
// 〇敵と自分が重なって表示されるバグをなおす（敵キャラ）
// 〇NPCのランダム移動（指定した範囲内で）
// ステータス表示用のUIを考える
// 戦闘と戦闘の間にインターバルの時間を作る
// 〇デバッグ用のダイアログを常に表示できるようにする
// 攻撃アニメーションを作成
// BGMと効果音を乗せる
// 攻撃回数のガスガス音を入れる
// 通常より強い敵（赤い敵を実装）
// アイテム購入を実装
// 〇敵から経験値とお金を入手できるようにする
// 〇レベルアップできるようにする
// 〇戦闘コルーチンのwaitForTimeSecondを戦闘速度という形でオプション設定できるようにする

public enum GameState
{
    FreeRoam,
    Dialog,
    Battle,
}

// ゲームの遷移状態
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] int stageDifficulty; // ステージ難易度:敵のLevel設定に使用
    [SerializeField] float battleSpeed; // 戦闘速度:戦闘コルーチンのWait時間に使用

    public static GameController Instance { get; private set; }
    public int StageDifficulty { get => stageDifficulty; }
    public float BattleSpeed { get => battleSpeed; }

    GameState gameState;
    public GameState GameState { get => gameState; }
    public List<Vector2> MoveTargetList { get; set; } // 全ての移動物の目的地を管理する

    // インスタンス化とgameStateの初期化
    private void Awake()
    {
        Instance = this;
        gameState = GameState.FreeRoam;
        MoveTargetList = new List<Vector2>();
    }

    // 現在のステートを設定
    public void SetCurrentState(GameState state)
    {
        gameState = state;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.FreeRoam)
        {
            // playerControllerを動かす
            playerController.HandleUpdate();
        }

        // Dialogステートの時は
        if (gameState == GameState.Dialog)
        {
            // TalkDialogManagerを動かす
            TalkDialogManager.Instance.HandleUpdate();
        }

        // Battleステートの時は
        if (gameState == GameState.Battle)
        {
            // BattleSystemをスタートさせる
            BattleSystem.Instance.HandleStart();
        }
    }
}
