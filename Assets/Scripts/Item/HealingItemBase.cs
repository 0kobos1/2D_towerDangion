using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 回復アイテム特性
[CreateAssetMenu]
public class HealingItemBase : ScriptableObject
{
    [SerializeField] new string name; // 名前
    [TextArea][SerializeField] string description; // 説明
    [SerializeField] int healPoint; // 回復量
    [SerializeField] int buyPrice; // 買値
    [SerializeField] int sellPrice; // 売値

    public string Name { get => name; }
    public string Description { get => description; }
    public int HealPoint { get => healPoint; }
    public int BuyPrice { get => buyPrice; }
    public int SellPrice { get => sellPrice; }
}
