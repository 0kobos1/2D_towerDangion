using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������
[CreateAssetMenu]
public class WeaponBase : ScriptableObject
{
    [SerializeField] new string name; // ���O
    [TextArea][SerializeField] string description; // ����
    [SerializeField] int atk; // �U����
    [SerializeField] int buyPrice; // ���l
    [SerializeField] int sellPrice; // ���l

    public string Name { get => name; }
    public string Description { get => description; }
    public int Atk { get => atk; }
    public int BuyPrice { get => buyPrice; }
    public int SellPrice { get => sellPrice; }
    
}
