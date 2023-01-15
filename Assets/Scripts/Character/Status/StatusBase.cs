using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーステータスのベース
[CreateAssetMenu]
public class StatusBase : ScriptableObject
{
    // エネミーのステータスを設定する
    [SerializeField] new string name; // 名前

    [TextArea]
    [SerializeField] string description;

    // 上下左右のスプライト
    [SerializeField] Sprite downSprite1;
    [SerializeField] Sprite downSprite2;
    [SerializeField] Sprite upSprite1;
    [SerializeField] Sprite upSprite2;
    [SerializeField] Sprite leftSprite1;
    [SerializeField] Sprite leftSprite2;
    [SerializeField] Sprite rightSprite1;
    [SerializeField] Sprite rightSprite2;

    [SerializeField] int maxHp; // 最大HP
    [SerializeField] int hp; // 現在のHP
    [SerializeField] int atk; // 攻撃力
    [SerializeField] int def; // 防御力

    [SerializeField] int exp; // 経験値
    [SerializeField] int money; // お金 

    [SerializeField] List<WeaponBase> weaponBases; // 所持武器
    [SerializeField] List<HealingItemBase> healingItemBases; // 所持回復アイテム

    public string Name { get => name; }
    public int MaxHp { get => maxHp; }
    public int Hp { get => hp; }
    public int Atk { get => atk; }
    public int Def { get => def; }

    public int Exp { get => exp; }
    public int Money { get => money; }
    public List<WeaponBase> WeaponBases { get => weaponBases; }
    public List<HealingItemBase> HealingItemBases { get => healingItemBases; }
}