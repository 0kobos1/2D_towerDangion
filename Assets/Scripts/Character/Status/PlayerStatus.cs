using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �v���[���[�̃X�e�[�^�X���Ǘ�����
public class PlayerStatus : MonoBehaviour
{
    [SerializeField] StatusBase playerStatusBase; // �v���[���[�̃X�e�[�^�X�̃x�[�X
    Status playerStatus; // �v���[���[�̃X�e�[�^�X�i���x���A�b�v�𔽉f�j
    public StatusBase PlayerStatusBase { get => playerStatusBase; }

    public int Level { get; set; } // ���x��
    public int MaxHp { get; set; } // �ő�HP
    public int Hp { get; set; } // HP
    public int Atk { get; set; } // �U����
    public int Def { get; set; } // �h���

    public int Exp { get; set; } // �����o���l
    public int Money { get; set; } // ������

    // public List<ScriptableObject> �Ŏ��������Ǘ�

    private void Awake()
    {
        // �J�n���̃��x���ݒ�
        Level = 10;

        // ���x���ɉ�������b�X�e�[�^�X���Q�b�g
        LevelStatusGet(Level);
    }

    // ���x���A�b�v�ɉ������X�e�[�^�X���擾(from BattleSystem?)
    public void LevelStatusGet(int level)
    {
        // ���x���ɉ�������b�X�e�[�^�X���擾
        playerStatus = new Status(playerStatusBase, level);

        MaxHp = playerStatus.MaxHp;
        Hp = playerStatus.Hp;
        Atk = playerStatus.Atk;
        Def = playerStatus.Def;

        // �X�e�[�^�X�ύX���_�C�A���O�ɔ��f������

    }


    // �U�����󂯂����̃_���[�W�𒲂ׂ�
    public void TakeDamage(EnemyStatus attackerStatus, PlayerStatus defenderStatus)
    {
        // �_���[�W���v�Z����
        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attackerStatus.Level + 10) / 250f;
        float d = a * ((float)attackerStatus.Atk / defenderStatus.Def) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        defenderStatus.Hp -= damage;

        // �_���[�W���[���ȉ��Ȃ�A
        if (defenderStatus.Hp <= 0)
        {
            // �_���[�W���[���ɂ���
            defenderStatus.Hp = 0;
        }
    }
    // �|�����G����o���l�A�����A�A�C�e�����擾���� (from BattleSystem)
    public void GetExpMoneyItems(EnemyStatus enemyStatus)
    {
        //
        Exp += enemyStatus.Exp;
        Money += enemyStatus.Money;

        DebugTextManager.Instance.text2 = "Exp: " + Exp.ToString();
        DebugTextManager.Instance.text3 = "Money: " + Money.ToString();



        // �o���l�����̃��x���A�b�v�ւ̕K�v�o���l�𒴂��Ă���΁i�����ł�100�j
        if (Exp >= 100)
        {
            // Level����グ��
            Level++;
            DebugTextManager.Instance.text1 = "PlayerLevel: " + Level.ToString();

            // Exp����100������
            Exp -= 100;
            DebugTextManager.Instance.text2 = "Exp: " + Exp.ToString();
        }
    }

}
