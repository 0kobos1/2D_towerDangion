using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���x���ɉ������G�l�~�[�̃X�e�[�^�X�𐶐�
// �f�[�^�݂̂������i������C#�̃N���X�j
[System.Serializable]
public class EnemyStatus
{
    [SerializeField] EnemyStatusBase enemyStatusBase; // ���p����PlayerStatusBase
    [SerializeField] int level; // �v���C���[�̃��x��

    public EnemyStatusBase EnemyStatusBase { get => enemyStatusBase; }

    // �R���X�g���N�^
    public EnemyStatus(EnemyStatusBase pEnemyStatusBase, int pLevel)
    {
        enemyStatusBase = pEnemyStatusBase;
        level = pLevel;
    }

    // playerStatusBase�̃X�e�[�^�X���擾���A���x���ɉ������v�Z�����Čv�Z��̌��ʂ�Ԃ�
    public int MaxHP { get { return Mathf.FloorToInt((enemyStatusBase.MaxHp * level) / 100f) + 5; } }
    public int Hp { get { return Mathf.FloorToInt((enemyStatusBase.Hp * level) / 100f) + 5; } }
    public int Atk { get { return Mathf.FloorToInt((enemyStatusBase.Atk * level) / 100f) + 5; } }
    public int Def { get { return Mathf.FloorToInt((enemyStatusBase.Def * level) / 100f) + 5; } }
}
