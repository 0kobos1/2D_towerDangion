using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���x���ɉ������v���C���[�̃X�e�[�^�X�𐶐�
// �f�[�^�݂̂������i������C#�̃N���X�j
[System.Serializable]
public class PlayerStatus
{
    [SerializeField] PlayerStatusBase playerStatusBase; // ���p����PlayerStatusBase
    [SerializeField] int level; // �v���C���[�̃��x��

    public PlayerStatusBase PlayerStatusBase { get => playerStatusBase; }

    // �R���X�g���N�^
    public PlayerStatus(PlayerStatusBase pPlayerStatusBase, int pLevel)
    {
        playerStatusBase = pPlayerStatusBase;
        level= pLevel;
    }

    // playerStatusBase�̃X�e�[�^�X���擾���A���x���ɉ������v�Z�����Čv�Z��̌��ʂ�Ԃ�
    public int MaxHP { get { return Mathf.FloorToInt((playerStatusBase.MaxHp * level) / 100f) + 5; } }
    public int Hp { get { return Mathf.FloorToInt((playerStatusBase.Hp * level) / 100f) + 5; } }
    public int Atk { get { return Mathf.FloorToInt((playerStatusBase.Atk * level) / 100f) + 5; } }
    public int Def { get { return Mathf.FloorToInt((playerStatusBase.Def * level) / 100f) + 5; } }
    
}
