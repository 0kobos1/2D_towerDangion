using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 修正
// 〇敵と接触したタイミングでGameControllerのstateをBattleに変える

// プレイヤーの動作制御
public class PlayerController : MonoBehaviour
{
    
    Vector2 input; // キーボード入力を格納
    
    Character character; // キャラクター
  
    private void Awake()
    {
        character = GetComponent<Character>();
    }

    // キーボード入力を受け付けて、位置を変える
    public void HandleUpdate()
    {
        // 移動中でなければ、
        // Vボタンを押したときにメニュー画面を開く
        if(!character.IsMoving)
        {
            if(Input.GetKeyDown(KeyCode.V)) 
            {
                Debug.Log("メニューを開いた！");
                GameController.Instance.SetCurrentState(GameState.Menu);
                
            }
        }

        // 移動中でなければ、
        if (!character.IsMoving)
        {
            // 移動入力を受け付ける
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // 斜め入力の禁止
            if (input.x != 0)
            {
                input.y = 0;
            }

            // 矢印の入力があるとき
            if (input != Vector2.zero)
            {
                // 目的地に敵がいれば
                if (TouchedEnemy(input) != null) 
                {
                    // touchedEnemyに接触があった敵のゲームオブジェクトを格納
                    GameObject touchedEnemy = TouchedEnemy(input).gameObject;

                    // タッチした敵のBattleStatusをBattleSystemに送る
                    BattleSystem.Instance.GetBattleObjects(this.gameObject, touchedEnemy);


                    // GameStateをBattleに設定
                    GameController.Instance.SetCurrentState(GameState.Battle);
                }

                // 移動（コルーチン）
                StartCoroutine(character.Move(input));
            }
        }
        character.HandleUpdate();

        Interact();
    }

    void Interact()
    {
        // ZボタンをおしてInteract
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // 向いている方向
            Vector3 faceDirection = new Vector3(character.CharacterAnimator.MoveX, character.CharacterAnimator.MoveY);
            // 干渉する場所
            Vector3 interactPos = transform.position + faceDirection;

            // 干渉する場所にRayを飛ばす
            Collider2D collider2D = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayers.Instance.InteractbleLayer);

            if (collider2D)
            {
                collider2D.GetComponent<IInteractable>()?.Interact(transform.position);
            }

        }
    }

    // エネミーとの接触を調べる
    Collider2D TouchedEnemy(Vector2 moveVec)
    {
        Vector2 targetPos;
        targetPos = (Vector2)transform.position + moveVec;

        // 移動先にレイを飛ばし、EnemyLayerとの接触判定を調べる
        return Physics2D.OverlapCircle(targetPos, 0.2f, GameLayers.Instance.EnemyLayer);
    }

}
