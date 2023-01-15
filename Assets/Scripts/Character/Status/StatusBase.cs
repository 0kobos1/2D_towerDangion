using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �v���C���[�X�e�[�^�X�̃x�[�X
[CreateAssetMenu]
public class StatusBase : ScriptableObject
{
    // �G�l�~�[�̃X�e�[�^�X��ݒ肷��
    [SerializeField] new string name; // ���O

    [TextArea]
    [SerializeField] string description;

    // �㉺���E�̃X�v���C�g
    [SerializeField] Sprite downSprite1;
    [SerializeField] Sprite downSprite2;
    [SerializeField] Sprite upSprite1;
    [SerializeField] Sprite upSprite2;
    [SerializeField] Sprite leftSprite1;
    [SerializeField] Sprite leftSprite2;
    [SerializeField] Sprite rightSprite1;
    [SerializeField] Sprite rightSprite2;

    [SerializeField] int maxHp; // �ő�HP
    [SerializeField] int hp; // ���݂�HP
    [SerializeField] int atk; // �U����
    [SerializeField] int def; // �h���

    [SerializeField] int exp; // �o���l
    [SerializeField] int money; // ���� 

    [SerializeField] List<WeaponBase> weaponBases; // ��������
    [SerializeField] List<HealingItemBase> healingItemBases; // �����񕜃A�C�e��

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