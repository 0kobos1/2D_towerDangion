using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// �C��

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleDialogManager battleDialogManager;

    GameObject playerObject; // �퓬�ɎQ������v���[���[�I�u�W�F�N�g
    GameObject enemyObject; // �퓬�ɎQ������G�l�~�[�I�u�W�F�N�g


    PlayerStatus playerStatus; // �퓬�J�n���ɐڐG�����v���C���[�̏��
    EnemyStatus enemyStatus; // �퓬�J�n���ɐڐG�����G�̏��

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
    public void GetBattleObjects(GameObject touchedPlayerObject, GameObject touchedEnemyObject)
    {
        playerObject = touchedPlayerObject;
        enemyObject = touchedEnemyObject;
        playerStatus = touchedPlayerObject.GetComponent<PlayerStatus>();
        enemyStatus = touchedEnemyObject.GetComponent<EnemyStatus>();
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

        // �G�X�e�[�^�X�̃Z�b�g�A�b�v    
        //playerStatus.Setup();
        Debug.Log(enemyObject.name);
        enemyStatus.SetUp();

        // �o�g���_�C�A���O�̃Z�b�g�A�b�v
        battleDialogManager.SetUp(playerObject, enemyObject);

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
            enemyStatus.TakeDamage(playerStatus, enemyStatus);
            Debug.Log("�v���C���[����G�ւ̍U��");

            // �o�g���_�C�A���O�̃A�b�v�f�[�g
            StatusDialogManager.Instance.DialogUpdate();
            battleDialogManager.HandleUpdate();

            // �G�̃_���[�W��0�ȉ��ł����
            if (enemyStatus.Hp <= 0)
            {
                Debug.Log("�G�̓v���C���[�ɓ|���ꂽ");

                // �G�̌o���l�A�����A�A�C�e����PlayerSubStatus�ɓn��
                playerStatus.GetExpMoneyItems(enemyStatus);

                // �v���[���[�̃X�e�[�^�X�ω����_�C�A���O�ɔ��f����
                StatusDialogManager.Instance.DialogUpdate();

                // �G�̃I�u�W�F�N�g���\���ɂ���
                enemyObject.SetActive(false);

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
            playerStatus.TakeDamage(enemyStatus, playerStatus);
            Debug.Log("�G����v���C���[�ւ̍U��");

            //// �o�g���_�C�A���O�̃A�b�v�f�[�g
            battleDialogManager.HandleUpdate();

            // ������HP��0�ȉ��ł����
            if (playerStatus.Hp <= 0)
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
