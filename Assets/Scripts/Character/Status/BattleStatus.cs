using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �C��
// �Z�X�e�[�W�̓�Փx��GameController����擾

// �퓬���̃X�e�[�^�X�ω�(HP�̕ω����܂ށj���Ǘ�
public class BattleStatus : MonoBehaviour
{ 
    [SerializeField] StatusBase statusBase; // �x�[�X�X�e�[�^�X
    [SerializeField] bool isPlayer = false; // �v���C���[���ǂ����𔻒肷��
    // [SerializeField] int level; // ���x��

    private PlayerSubStatus playerSubStatus; // �v���[���[�̃T�u�X�e�[�^�X

    public Status Status { get; set; }
    public int Hp { get; set; }
    public int Level { get; set; } // ���x�� 
    public StatusBase StatusBase { get => statusBase;}

    public void Awake()
    {
        // �v���[���[�Ȃ�
        if(isPlayer == true)
        {
            // playerSubStatus���擾
            playerSubStatus = GetComponent<PlayerSubStatus>();
        }
    }

    public void SetUp()
    {
        // �v���C���[�Ȃ��
        if (isPlayer)
        {
            // ���x���A����Hp��PlayerSubStatus����ݒ�i�o���l�擾�ɂ�郌�x���A�b�v�����f�����j
            Level = playerSubStatus.Level;
            Hp = playerSubStatus.Hp;
            DebugTextManager.Instance.text1 = "PlayerLevel: " + Level.ToString();

        }

        // �G�Ȃ��
        else
        {
            // �X�e�[�W��Փx���擾(from GameController)
            int stageDifficulty = GameController.Instance.StageDifficulty;

            // �����Ń��x����ω�������
            float modifier = Random.Range(0.85f, 1f);
            
            // �X�e�[�W�̓�Փx�ɍ��킹�ă��x����ς���
            Level = (int) (stageDifficulty * modifier);

            
        }

        // �X�e�[�^�X�𐶐�
        Status = new Status(statusBase, Level);

        // Hp���ő�l�Ŏ擾����
        Hp = Status.Hp;
    }

    // �U�����󂯂����̃_���[�W�𒲂ׂ�
    public void TakeDamage(BattleStatus attackerBattleStatus, BattleStatus defenderBattleStatus)
    {
        // �_���[�W���v�Z����
        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attackerBattleStatus.Status.Level + 10) / 250f;
        float d = a * ((float)attackerBattleStatus.Status.Atk / defenderBattleStatus.Status.Def) + 2;
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

