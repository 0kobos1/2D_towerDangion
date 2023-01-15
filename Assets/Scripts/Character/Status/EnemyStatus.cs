using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] StatusBase enemyStatusBase; // �v���[���[�̃X�e�[�^�X�̃x�[�X
    Status status; // �v���[���[�̃X�e�[�^�X�i���x���A�b�v�𔽉f�j
    public StatusBase statusBase { get => enemyStatusBase; }

    public int Level { get; set; } // ���x��
    public int MaxHp { get; set; } // �ő�HP
    public int Hp { get; set; } // HP
    public int Atk { get; set; } // �U����
    public int Def { get; set; } // �h���

    public int Exp { get; set; } // �����o���l
    public int Money { get; set; } // ������
    public List<WeaponBase> Weapons { get; set; } // ��������
    public List<HealingItemBase> HealingItems { get; set; } // �����񕜃A�C�e��

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

    // ���x���ɉ������X�e�[�^�X���擾(from BattleSystem?)
    public void LevelStatusGet(int level)
    {
        // ���x���ɉ������X�e�[�^�X���擾
        status = new Status(enemyStatusBase, level);

        MaxHp = status.MaxHp;
        Hp = status.Hp;
        Atk = status.Atk;
        Def = status.Def;
        Exp = status.Exp;
        Money = status.Money;

        // ��������̃x�[�X���烉���_���ł��������莝���ɉ�����
        Weapons = new List<WeaponBase> { };
        Weapons = GetRandomWeapons(status);

        // �����A�C�e���̃x�[�X���烉���_���ł��������莝���ɉ�����
        HealingItems = new List<HealingItemBase> { };
        HealingItems = GetRandomHealingItems(status);
    }

    // ��������̃x�[�X���烉���_���ł��������莝���ɉ�����
    List<WeaponBase> GetRandomWeapons(Status status)
    {
        // ��������̑������v�Z����
        int weaponsCount = status.Weapons.Count;

        Debug.Log(weaponsCount);

        for (int i = 0; i < weaponsCount; i++)
        {
            if (Random.Range(0, 100) < 50)
            {
                Weapons.Add(status.Weapons[i]);
            }
        }
        return Weapons;
    }

    // �����񕜃A�C�e���̃x�[�X���烉���_���ł��������莝���ɉ�����
    List<HealingItemBase> GetRandomHealingItems(Status status)
    {
        // �����񕜃A�C�e���̑������v�Z����
        int healingItemsCount = status.HealingItems.Count;

        Debug.Log(healingItemsCount);

        for (int i = 0; i < healingItemsCount; i++)
        {
            if (Random.Range(0, 100) < 100)
            {
                HealingItems.Add(status.HealingItems[i]);
            }
        }
        return HealingItems;
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
