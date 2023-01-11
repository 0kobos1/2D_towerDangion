using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// �C��
// �G����o���l�Ƃ��������ł���悤�ɂ���
// ���x���A�b�v�ł���悤�ɂ���
// �퓬�R���[�`����waitForTimeSecond��퓬���x�Ƃ����`�ŃI�v�V�����ݒ�ł���悤�ɂ���
public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleDialogManager battleDialogManager;

    GameObject playerBattleObject; // �퓬�ɎQ������v���[���[�I�u�W�F�N�g
    GameObject enemyBattleObject; // �퓬�ɎQ������G�l�~�[�I�u�W�F�N�g

    BattleStatus playerBattleStatus; // �퓬�J�n���ɐڐG�����v���C���[�̏��
    PlayerSubStatus playerSubStatus; // �퓬�J�n���ɐڐG�����v���C���[�̃T�u���i�o���l�A�������A���L���Ȃǁj
    BattleStatus enemyBattleStatus; // �퓬�J�n���ɐڐG�����G�̏��
    

    public UnityAction OnBattleStart;
    public static BattleSystem Instance { get; private set; }

    float battleSpeed; // �퓬�R���[�`����wait����(from GameController)
    bool isBattling; // �퓬�����ǂ����𔻒�

    
    private void Awake()
    {
        Instance = this;

        // �o�g�����x�̐ݒ�
        battleSpeed = GameController.Instance.BattleSpeed; 
    }

    // �퓬�J�n���ɐڐG�����v���C���[�ƓG�̏����擾����
    public void GetBattleObjects(GameObject gotPlayerObject, GameObject gotEnemyObject)
    {
        playerBattleObject = gotPlayerObject;
        enemyBattleObject = gotEnemyObject;
        playerBattleStatus = gotPlayerObject.GetComponent<BattleStatus>();
        enemyBattleStatus = gotEnemyObject.GetComponent<BattleStatus>();
        playerSubStatus = playerBattleStatus.GetComponent<PlayerSubStatus>();
    }

    // �퓬�����ifrom GameController�j
    public void HandleStart()
    {
        // �퓬���J�n����Ă���΁A�퓬�I���܂ł͎��s���Ȃ��B
        if(isBattling == true)
        {
            return;
        }

        // �퓬���o�^
        isBattling = true;

        // GameController�̊֐����Ăяo����AgameState��Battle�ɂȂ�
        GameController.Instance.SetCurrentState(GameState.Battle);

        // �o�g�������o�[�̃Z�b�g�A�b�v
        playerBattleStatus.SetUp();
        enemyBattleStatus.SetUp();
        
        // �o�g���_�C�A���O�̃Z�b�g�A�b�v
        battleDialogManager.SetUp(playerBattleObject, enemyBattleObject);

        // �퓬�X�V�ւ̈ڍs
        HandleUpdate();

        Debug.Log("HandleStart");
    }

    // �퓬�X�V�ifrom GameController�j
    public void HandleUpdate()
    {        
        StartCoroutine(Battle());

    }

    // �퓬�R���[�`��
    IEnumerator Battle()
    {
        // �J��Ԃ�
        while (true)
        {
            yield return new WaitForSeconds(battleSpeed);

            // ��������G�ւ̍U��
            enemyBattleStatus.TakeDamage(playerBattleStatus, enemyBattleStatus);
            Debug.Log("�v���C���[����G�ւ̍U��");

            // �o�g���_�C�A���O�̃A�b�v�f�[�g
            battleDialogManager.HandleUpdate(playerBattleStatus, enemyBattleStatus);

            // �G�̃_���[�W��0�ȉ��ł����
            if (enemyBattleStatus.Hp <= 0)
            {
                Debug.Log("�G�̓v���C���[�ɓ|���ꂽ");

                // �G�̌o���l�A�����A�A�C�e����PlayerSubStatus�ɓn��
                playerSubStatus.GetExpMoneyItems(enemyBattleStatus);

                // �G�̃I�u�W�F�N�g���\���ɂ���
                enemyBattleStatus.gameObject.SetActive(false);

                // BattleDialog�����
                battleDialogManager.Close();

                // GameState��FreeRoam�ɖ߂�
                GameController.Instance.SetCurrentState(GameState.FreeRoam);

                // �o�g���I��
                isBattling = false;

                // �R���[�`���𔲂���
                yield break;
            }

            yield return new WaitForSeconds(battleSpeed);

            // �G��HP���c���Ă����

            // �G���玩���ւ̍U��
            playerBattleStatus.TakeDamage(enemyBattleStatus, playerBattleStatus);
            Debug.Log("�G����v���C���[�ւ̍U��");

            // �o�g���_�C�A���O�̃A�b�v�f�[�g
            battleDialogManager.HandleUpdate(playerBattleStatus, enemyBattleStatus);

            // ������HP��0�ȉ��ł����
            if (playerBattleStatus.Hp <= 0)
            {
                // �Q�[���I�[�o�[�̏���
                GameOver();

                yield break;
            }

            yield return new WaitForSeconds(battleSpeed);

            Debug.Log("Update�I��");

            yield return null;
        }
    }

    void GameOver()
    {
        // �Q�[���I�[�o�[�̏���
        Debug.Log("�Q�[���I�[�o�[");
    }

    
}
