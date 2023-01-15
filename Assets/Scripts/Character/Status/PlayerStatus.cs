using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �v���[���[�̃X�e�[�^�X���Ǘ�����
public class PlayerStatus : MonoBehaviour
{
    [SerializeField] StatusBase playerStatusBase; // �v���[���[�̃X�e�[�^�X�̃x�[�X
    Status status; // �v���[���[�̃X�e�[�^�X�i���x���A�b�v�𔽉f�j
    public StatusBase statusBase { get => playerStatusBase; }

    public int Level { get; set; } // ���x��
    public int Exp { get; set; } // �����o���l
    public int Money { get; set; } // ������

    public int MaxHp { get; set; } // �ő�HP
    public int Hp { get; set; } // HP
    public int Atk { get; set; } // �U����
    public int Def { get; set; } // �h���
    public List<WeaponBase> Weapons { get; set; } // ��������
    public List<HealingItemBase> HealingItems { get; set; } // �����񕜃A�C�e��

    // public List<ScriptableObject> �Ŏ��������Ǘ�

    private void Awake()
    {
        // �J�n���̐ݒ�
        Level = 10;
        Exp = 0;
        Money = 0;
        Weapons = new List<WeaponBase>();
        HealingItems = new List<HealingItemBase>();

        // ���x���ɉ�������b�X�e�[�^�X���Q�b�g
        LevelStatusGet(Level);

        Debug.Log(Atk);

        // �X�e�[�^�X�ύX���_�C�A���O�ɔ��f������
        StatusDialogManager.Instance.DialogUpdate();
        
    }

    // ���x���A�b�v�ɉ������X�e�[�^�X���擾(from BattleSystem?)
    public void LevelStatusGet(int level)
    {
        // ���x���ɉ�������b�X�e�[�^�X���擾
        status = new Status(playerStatusBase, level);
        MaxHp = status.MaxHp;
        Hp = status.Hp;
        Atk = status.Atk;
        Def = status.Def;        
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

    // �񕜃A�C�e�����g�p����(from MenuDialogManager)
    public void UseHealingItem(int currentItemSelection)
    {
        // �g�p����A�C�e�����w��
        HealingItemBase healingItem = HealingItems[currentItemSelection];

        // �g�p�����A�C�e���̌��ʂ�Hp�ɑ���
        Hp += healingItem.HealPoint;

        // HP���ő�HP�𒴂��Ă����
        if (Hp > MaxHp)
        {
            // Hp���ő�l�ƈ�v������
            Hp = MaxHp;
        }

        // HealingItem���X�g����g�p�����A�C�e��������
        HealingItems.Remove(healingItem);
    }

    // �|�����G����o���l�A�����A�A�C�e�����擾���� (from BattleSystem)
    public void GetExpMoneyItems(EnemyStatus enemyStatus)
    {
        //
        Exp += enemyStatus.Exp;
        Money += enemyStatus.Money;
        Weapons.AddRange(enemyStatus.Weapons);
        HealingItems.AddRange(enemyStatus.HealingItems);

        DebugTextManager.Instance.text2 = "Exp: " + Exp.ToString();
        DebugTextManager.Instance.text3 = "Money: " + Money.ToString();

        // �f�o�b�O�p=>
        string weaponNames = "";
        foreach (WeaponBase weaponBase in Weapons)
        {
            string weaponName = weaponBase.name;
            weaponNames += weaponName + " ";
        }
        DebugTextManager.Instance.text4 = "Weapon: " + weaponNames;
        // <=

        // �f�o�b�O�p=>
        string healingItemNames = "";
        foreach (HealingItemBase healingItemBase in HealingItems)
        {
            string healingItemName = healingItemBase.name;
            healingItemNames += healingItemName + " ";
        }
        DebugTextManager.Instance.text5 = "HealingItem: " + healingItemNames;
        // <=

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
