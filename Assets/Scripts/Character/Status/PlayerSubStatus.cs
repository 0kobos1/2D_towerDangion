using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �C��
// �A�C�e�����Q�b�g�����珊�L���ɉ�������悤�ɂ���
// ���̃��x���A�b�v�ւ̌o���l�K�v�ʂ����x���ɂ���ĕς���


// ���L�o���l�A�������A�������Ȃǂ̊Ǘ�
public class PlayerSubStatus : MonoBehaviour
{
    public int Level { get; private set; } // ���x���A�b�v���̃X�g�b�N
    public int Exp { get; private set; }
    public int Money { get; private set; }

    // List<ScriptableObject> �Ŏ��������Ǘ�

    private void Start()
    {
        // ���x���X�g�b�N�A���L�o���l�A�������̏�����
        Level = 50;
        Exp = 0;  
        Money = 0;
    }

    // �|�����G�Ɍo���l�A�����A�A�C�e�����擾���� (from BattleSystem)
    public void GetExpMoneyItems(BattleStatus enemyBattleStatus)
    {
        //
        Exp += enemyBattleStatus.Status.Exp;
        Money+= enemyBattleStatus.Status.Money;
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
