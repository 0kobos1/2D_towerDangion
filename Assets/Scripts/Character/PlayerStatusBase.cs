using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーステータスのベース
[CreateAssetMenu]
public class PlayerStatusBase : ScriptableObject
{
    // エネミーのステータスを設定する
    [SerializeField] new string name; // 名前

    [TextArea]
    [SerializeField] string description;

    [SerializeField] int maxHp; // 最大HP
    [SerializeField] int hp; // 現在のHP
    [SerializeField] int atk; // 攻撃力
    [SerializeField] int def; // 防御力

    public int MaxHp { get => maxHp; set => maxHp = value; }
    public int Hp { get => hp; set => hp = value; }
    public int Atk { get => atk; set => atk = value; }
    public int Def { get => def; set => def = value; }
}
