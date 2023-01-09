using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] GameObject battleDialogBox; // BattleDialogBox���C���X�y�N�^�[�o�^
    [SerializeField] GameObject battleHPBox;     // BattleHPBox���C���X�y�N�^�[�o�^

    public UnityAction OnBattleStart;
    public static BattleSystem Instance { get; private set; }

    
    private void Awake()
    {
        Instance = this;
    }

    // PlayerLayer��EnemyLayer�̐ڐG���肩��Ăяo�����
    public void HandleStart(GameObject playerObject, GameObject enemyObject)
    {
        // GameController�̊֐����Ăяo����AgameState��Battle�ɂȂ�
        OnBattleStart();

        // �v���C���[�ƓG��HP��HPText��HPBar�ɔ��f������
        

        // �G�Ɩ�����HP�o�[���_�C�A���O�{�b�N�X�ɕ\�������
        battleDialogBox.SetActive(true);
        battleHPBox.SetActive(true);
        
        // ��������G���U����

        // �_���[�W�ʂ��|�b�v�A�b�v�\������

        // �G��HP��������

        // �G���玩�����U����

        // �_���[�W�ʂ��|�b�v�A�b�v�\������

        // �Ƃ���HP���̂����Ă����

        // �퓬���p������

        // HP���̂����Ă��炸

        // �G��HP���[���ł���΁A

        // �G����o���l�Ƃ����A�A�C�e�����擾
    }

    public void HandleUpdate()
    {

    }
}
