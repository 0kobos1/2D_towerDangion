using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] StatusBase enemyStatusBase; // �v���[���[�̃X�e�[�^�X�̃x�[�X
    Status enemyStatus; // �v���[���[�̃X�e�[�^�X�i���x���A�b�v�𔽉f�j
    public StatusBase EnemyStatusBase { get => enemyStatusBase; }

    public int Level { get; set; } // ���x��
    public int MaxHp { get; set; } // �ő�HP
    public int Hp { get; set; } // HP
    public int Atk { get; set; } // �U����
    public int Def { get; set; } // �h���

    public int Exp { get; set; } // �����o���l
    public int Money { get; set; } // ������

    // public List<ScriptableObject> �Ŏ��������Ǘ�

    // �퓬�J�n���ɓG�̃X�e�[�^�X��񂪐��������
    public void SetUp()
    {
        // �J�n���̃��x���ݒ�
        Level = LevelSetter();

        // ���x���ɉ�������b�X�e�[�^�X���Q�b�g
        LevelStatusGet(Level);
    }

    // �X�e�[�W��Փx�ɍ��킹�ă��x����ݒ肷��
    int LevelSetter()
    {
        // �X�e�[�W��Փx���擾(from GameController)
        int stageDifficulty = GameController.Instance.StageDifficulty;

        // �����Ń��x����ω�������
        float modifier = Random.Range(0.85f, 1f);

        // �X�e�[�W�̓�Փx�ɍ��킹�ă��x����ς���
        Level = (int)(stageDifficulty * modifier);

        return Level;
    }

    // ���x���A�b�v�ɉ������X�e�[�^�X���擾(from BattleSystem?)
    public void LevelStatusGet(int level)
    {
        // ���x���ɉ�������b�X�e�[�^�X���擾
        enemyStatus = new Status(enemyStatusBase, level);

        MaxHp = enemyStatus.MaxHp;
        Hp = enemyStatus.Hp;
        Atk = enemyStatus.Atk;
        Def = enemyStatus.Def;
    }

    // �U�����󂯂����̃_���[�W�𒲂ׂ�
    public void TakeDamage(PlayerStatus attackerBattleStatus, EnemyStatus defenderBattleStatus)
    {
        // �_���[�W���v�Z����
        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attackerBattleStatus.Level + 10) / 250f;
        float d = a * ((float)attackerBattleStatus.Atk / defenderBattleStatus.Def) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        defenderBattleStatus.Hp -= damage;

        // �_���[�W���[���ȉ��Ȃ�A
        if (defenderBattleStatus.Hp <= 0)
        {
            // �_���[�W���[���ɂ���
            defenderBattleStatus.Hp = 0;
        }
    }
}
