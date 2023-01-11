using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


// �C��
// �N���X�}�̍쐬
// �G�Ǝ������d�Ȃ��ĕ\�������o�O���Ȃ����i�G�L�����j
// NPC�̃����_���ړ��i�w�肵���͈͓��Łj
// �X�e�[�^�X�\���p��UI���l����
// �퓬�Ɛ퓬�̊ԂɃC���^�[�o���̎��Ԃ����
// �Z�f�o�b�O�p�̃_�C�A���O����ɕ\���ł���悤�ɂ���
public enum GameState
{
    FreeRoam,
    Dialog,
    Battle,
}

// �Q�[���̑J�ڏ��
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] int stageDifficulty; // �X�e�[�W��Փx:�G��Level�ݒ�Ɏg�p
    [SerializeField] float battleSpeed; // �퓬���x:�퓬�R���[�`����Wait���ԂɎg�p

    public static GameController Instance { get; private set; }
    public int StageDifficulty { get => stageDifficulty; }
    public float BattleSpeed { get => battleSpeed; }

    GameState gameState;
    public GameState GameState { get => gameState; }
    public List<Vector2> MoveTargetList { get; set; } // �S�Ă̈ړ����̖ړI�n���Ǘ�����



    // �C���X�^���X����gameState�̏�����
    private void Awake()
    {
        Instance = this;
        gameState = GameState.FreeRoam;
        MoveTargetList = new List<Vector2>();
    }

    // ���݂̃X�e�[�g��ݒ�
    public void SetCurrentState(GameState state)
    {
        gameState = state;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.FreeRoam)
        {
            // playerController�𓮂���
            playerController.HandleUpdate();
        }

        // Dialog�X�e�[�g�̎���
        if (gameState == GameState.Dialog)
        {
            // TalkDialogManager�𓮂���
            TalkDialogManager.Instance.HandleUpdate();
        }

        // Battle�X�e�[�g�̎���
        if (gameState == GameState.Battle)
        {
            // BattleSystem���X�^�[�g������
            BattleSystem.Instance.HandleStart();
        }
    }
}
