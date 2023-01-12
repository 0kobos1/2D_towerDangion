using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// �C��
// player�̌��݂̃X�e�[�^�X��ۑ��ł���X�N���v�g���쐬���ABattleSystem�ł����炩��f�[�^���擾����悤�ɂ���B
// �퓬���J�n������v���C���[�ƃG�l�~�[�̏��HP���\�������
// �U�����󂯂���_���[�W�̃|�b�v�A�b�v���o��
// �Z�v���C���[���ړ����悤�Ƃ����ꏊ���������X�g����폜����Ȃ�
// �N���X�}�̍쐬
// �퓬���I����Ă��v���C���[��HP�Ȃǂ��Ђ������悤�ɂ���B
// �~�j�}�b�v�̍쐬�i����j
// �Z�G�Ǝ������d�Ȃ��ĕ\�������o�O���Ȃ����i�G�L�����j
// �ZNPC�̃����_���ړ��i�w�肵���͈͓��Łj
// �X�e�[�^�X�\���p��UI���l����
// �퓬�Ɛ퓬�̊ԂɃC���^�[�o���̎��Ԃ����
// �Z�f�o�b�O�p�̃_�C�A���O����ɕ\���ł���悤�ɂ���
// �U���A�j���[�V�������쐬
// BGM�ƌ��ʉ����悹��
// �U���񐔂̃K�X�K�X��������
// �ʏ��苭���G�i�Ԃ��G�������j
// �A�C�e���w��������
// �Z�G����o���l�Ƃ��������ł���悤�ɂ���
// �Z���x���A�b�v�ł���悤�ɂ���
// �Z�퓬�R���[�`����waitForTimeSecond��퓬���x�Ƃ����`�ŃI�v�V�����ݒ�ł���悤�ɂ���

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
