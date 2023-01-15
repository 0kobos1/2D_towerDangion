using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// <修正>
// 装備画面を設定
// アイテムの実装
// メニューUIを作成する
// Sボタンを押すと同時にメニューウインドウが出るようにする
// 回復アイテムの実装
// 〇スクリプタブルオブジェクトで武器を作成
// 敵からとった武器を装備できるようにするF
// 敵のレベルに応じて、良いアイテムがもらえるようにする
// 攻撃を受けたらダメージのポップアップが出る
// 戦闘が終わってもプレイヤーのHPなどがひきつがれるようにする。
// ミニマップの作成（左上）
// NPCのランダム移動（指定した範囲内で）
// ステータス表示用のUIを考える
// メニュー画面の実装
// 戦闘と戦闘の間にインターバルの時間を作る
// 攻撃アニメーションを作成
// BGMと効果音を乗せる
// 攻撃回数のガスガス音を入れる
// 通常より強い敵（赤い敵を実装）
// アイテム購入を実装

public enum GameState
{
    FreeRoam,
    Dialog,
    Battle,
    Menu,
}

// ゲームの遷移状態
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] MenuDialogManager menuDialogManager;
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
            // MessageDialogManagerを動かす
            MessageDialogManager.Instance.HandleUpdate();
        }

        // Battleステートの時は
        if (gameState == GameState.Battle)
        {
            // BattleSystemをスタートさせる
            BattleSystem.Instance.HandleStart();
        }

        // Menuステートの時は
        if(gameState == GameState.Menu)
        {
            // Menuステートを動かす
            menuDialogManager.SetUp();
        }
    }
}
