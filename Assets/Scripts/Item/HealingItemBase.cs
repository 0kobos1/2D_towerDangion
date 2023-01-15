using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �񕜃A�C�e������
[CreateAssetMenu]
public class HealingItemBase : ScriptableObject
{
    [SerializeField] new string name; // ���O
    [TextArea][SerializeField] string description; // ����
    [SerializeField] int healPoint; // �񕜗�
    [SerializeField] int buyPrice; // ���l
    [SerializeField] int sellPrice; // ���l

    public string Name { get => name; }
    public string Description { get => description; }
    public int HealPoint { get => healPoint; }
    public int BuyPrice { get => buyPrice; }
    public int SellPrice { get => sellPrice; }
}
