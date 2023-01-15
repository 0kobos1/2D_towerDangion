using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// <�C��>
// ������ʂ�ݒ�
// �A�C�e���̎���
// ���j���[UI���쐬����
// S�{�^���������Ɠ����Ƀ��j���[�E�C���h�E���o��悤�ɂ���
// �񕜃A�C�e���̎���
// �Z�X�N���v�^�u���I�u�W�F�N�g�ŕ�����쐬
// �G����Ƃ�������𑕔��ł���悤�ɂ���F
// �G�̃��x���ɉ����āA�ǂ��A�C�e�������炦��悤�ɂ���
// �U�����󂯂���_���[�W�̃|�b�v�A�b�v���o��
// �퓬���I����Ă��v���C���[��HP�Ȃǂ��Ђ������悤�ɂ���B
// �~�j�}�b�v�̍쐬�i����j
// NPC�̃����_���ړ��i�w�肵���͈͓��Łj
// �X�e�[�^�X�\���p��UI���l����
// ���j���[��ʂ̎���
// �퓬�Ɛ퓬�̊ԂɃC���^�[�o���̎��Ԃ����
// �U���A�j���[�V�������쐬
// BGM�ƌ��ʉ����悹��
// �U���񐔂̃K�X�K�X��������
// �ʏ��苭���G�i�Ԃ��G�������j
// �A�C�e���w��������

public enum GameState
{
    FreeRoam,
    Dialog,
    Battle,
    Menu,
}

// �Q�[���̑J�ڏ��
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] MenuDialogManager menuDialogManager;
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
            // MessageDialogManager�𓮂���
            MessageDialogManager.Instance.HandleUpdate();
        }

        // Battle�X�e�[�g�̎���
        if (gameState == GameState.Battle)
        {
            // BattleSystem���X�^�[�g������
            BattleSystem.Instance.HandleStart();
        }

        // Menu�X�e�[�g�̎���
        if(gameState == GameState.Menu)
        {
            // Menu�X�e�[�g�𓮂���
            menuDialogManager.SetUp();
        }
    }
}
