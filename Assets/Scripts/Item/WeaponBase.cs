using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// •Ší“Á«
[CreateAssetMenu]
public class WeaponBase : ScriptableObject
{
    [SerializeField] new string name; // –¼‘O
    [TextArea][SerializeField] string description; // à–¾
    [SerializeField] int atk; // UŒ‚—Í
    [SerializeField] int buyPrice; // ”ƒ’l
    [SerializeField] int sellPrice; // ”„’l

    public string Name { get => name; }
    public string Description { get => description; }
    public int Atk { get => atk; }
    public int BuyPrice { get => buyPrice; }
    public int SellPrice { get => sellPrice; }
    
}
