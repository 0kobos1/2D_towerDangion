using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Windows;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] float moveSpeed; // 移動速度
    CharacterAnimator characterAnimator; // アニメーション
    public bool IsMoving { get; set; } // 移動中かどうか

    public CharacterAnimator CharacterAnimator { get => characterAnimator; }


    private void Awake()
    {
        characterAnimator = GetComponent<CharacterAnimator>();
    }

    public void HandleUpdate()
    {
        // アニメーションの"IsMoving"にここの"IsMoving"を設定する
        characterAnimator.IsMoving = IsMoving;
    }

    // 移動とアニメーションを管理する
    public IEnumerator Move(Vector3 moveVec)
    {   
        // アニメーションのMoveXにmoveVecのxを設定し、-1~1の範囲を超えないようにする
        characterAnimator.MoveX = Mathf.Clamp(moveVec.x, -1f, 1f);

        // アニメーションのMoveYにmoveVecのyを設定し、-1~1の範囲を超えないようにする
        characterAnimator.MoveY = Mathf.Clamp(moveVec.y, -1f, 1f);

        // 目的地を設定する
        Vector3 targetPos = transform.position;
        targetPos += moveVec;

        // 移動先に障害物があれば関数を抜ける
        if (!IsPathClear(targetPos))
        {
            yield break;
        }

        // 移動中
        IsMoving = true;

        // targetPosとの距離を徐々につめる
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // 十分に近づいたら位置をtargetPosに移動させる
        transform.position = targetPos;

        // 移動終了
        IsMoving = false;
    }

    bool IsPathClear(Vector3 targetPos)
    {
        // 目的地と現在地の差を計算
        Vector3 diff = targetPos - transform.position;

        // 現在地からみた目的地の方向（長さ1に正規化）
        Vector3 dir = diff.normalized;

        // 現在地から目的地に向かって箱型のレイを飛ばし、SolidObjectLayerかInteractableLayerと接触があるかを確認する（自分への接触を防ぐため、となりのマスから飛ばす）
        return !Physics2D.BoxCast(transform.position + dir, new Vector2(0.2f, 0.2f), 0, dir, diff.magnitude - 1,
                                  GameLayers.Instance.SolidObjectLayer | GameLayers.Instance.InteractbleLayer | GameLayers.Instance.PlayerLayer | GameLayers.Instance.EnemyLayer);
    }

    // targetPosの方向を向く
    public void LookTowards(Vector3 targetPos)
    {
        // 現在地から目的地への向きを計算（切り捨てにしておくことでバグ回避）
        float xDiff = Mathf.Floor(targetPos.x) - Mathf.Floor(transform.position.x);
        float yDiff = Mathf.Floor(targetPos.y) - Mathf.Floor(transform.position.y);

        // xかyかどちらかがゼロのときだけ（斜めにならないようにして）
        if (xDiff == 0 || yDiff ==0)
        {
            // アニメーターのXにx方向の向きを入れる
            characterAnimator.MoveX = Mathf.Clamp(xDiff, -1f, 1f);

            // アニメーターのYにy方向の向きを入れる
            characterAnimator.MoveY = Mathf.Clamp(yDiff, -1f, 1f);
        }
        
    }
}
