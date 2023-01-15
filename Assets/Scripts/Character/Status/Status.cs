using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���x���ɉ������v���C���[�̃X�e�[�^�X�𐶐�
// �f�[�^�݂̂������i������C#�̃N���X�j
public class Status
{
    public StatusBase StatusBase { get; set; } // ���p����Status�x�[�X
    public int Level { get; set; }

    // �R���X�g���N�^
    public Status(StatusBase pStatusBase, int pLevel)
    {
        StatusBase = pStatusBase;
        Level = pLevel;
    }

    // playerStatusBase�̃X�e�[�^�X���擾���A���x���ɉ������v�Z�����Čv�Z��̌��ʂ�Ԃ�
    public int MaxHp { get { return Mathf.FloorToInt((StatusBase.MaxHp * Level) / 100f) + 5; } }
    public int Hp { get { return Mathf.FloorToInt((StatusBase.Hp * Level) / 100f) + 5; } }
    public int Atk { get { return Mathf.FloorToInt((StatusBase.Atk * Level) / 100f) + 5; } }
    public int Def { get { return Mathf.FloorToInt((StatusBase.Def * Level) / 100f) + 5; } }

    public int Exp { get { return Mathf.FloorToInt((StatusBase.Exp * Level) / 100f) + 5; } }
    public int Money { get { return Mathf.FloorToInt((StatusBase.Money * Level) / 100f) + 5; } }

    public List<WeaponBase> Weapons { get { return StatusBase.WeaponBases; } }
    public List<HealingItemBase> HealingItems { get { return StatusBase.HealingItemBases; } }
}
