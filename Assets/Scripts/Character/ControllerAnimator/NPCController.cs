using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCstate
{
    Idle,
    Walk,
    Dialog,
}

public class NPCController : MonoBehaviour, IInteractable
{
    [SerializeField] Dialog dialog;
    [SerializeField] float timeBetweenPattern;
    // [SerializeField] List<Vector2> movePattern;

    Character character;
    NPCstate state;

    float idleTimer;
    //int currentMovePattern;

    private void Awake()
    {
        //currentMovePattern = 0;
        state = NPCstate.Idle;
        character = GetComponent<Character>();
    }

    private void Update()
    {
        // Battle�X�e�[�g����Update���Ȃ�(�SNPC���~�܂�j
        if (GameController.Instance.GameState == GameState.Battle)
        {
            return;
        }

        // Menu�X�e�[�g����Update���Ȃ�(�SNPC���~�܂�j
        if (GameController.Instance.GameState == GameState.Menu)
        {
            return;
        }

        character.HandleUpdate();

        // 2�b�Ԋu�ŃX�e�[�g��"Walk"�ɕύX����
        if (state == NPCstate.Idle)
        {
            idleTimer += Time.deltaTime;
            
            if(idleTimer > timeBetweenPattern)
            {
                idleTimer = 0f;

                //StartCoroutine(Walk());
                StartCoroutine(RandomWalk());
            }
        }
    }

    // �p�^�[���������s
    //IEnumerator Walk()
    //{
    //    // ���݂̃X�e�[�g��Walk�ɕύX
    //    state = NPCstate.Walk;
        
    //    // ���݈ʒu��oldPosition�Ɋi�[����
    //    Vector3 oldPosition = transform.position;

    //    // ���s����
    //    yield return character.Move(movePatten[currentMovePattern]);

    //    // �ړ������O�̃|�W�V�����ƈړ�������̃|�W�V�����������łȂ���΁i�ړ����Ă���΁j
    //    if(oldPosition != transform.position)
    //    {
    //        // ���̈ړ��p�^�[����ݒ肷��
    //        currentMovePattern = (currentMovePattern + 1) % movePatten.Count;
    //    }
        
    //    // Idle�X�e�[�g�ɑJ��
    //    state = NPCstate.Idle;
    //}


    // ���������_�����s
    IEnumerator RandomWalk()
    {
        // -1, 0, 1�̃����_���ȖړI�n���擾����
        Vector2 randomMovePos;
        ;
        randomMovePos.x = Random.Range(-1, 2);
        randomMovePos.y = Random.Range(-1, 2);

        // �ړ��������[���Ȃ�
        if (randomMovePos == Vector2.zero)
        {
            // idleTimer��������
            idleTimer = 0f;

            // Idle�X�e�[�g�ɑJ��
            state = NPCstate.Idle;

            // �ړ��R���[�`���𔲂���
            yield break;
        }

        // �ړ����W���[���łȂ��Ȃ�    
        // �΂ߕ����Ɉړ����Ȃ��悤�ɂ���
        if (randomMovePos.x != 0)
        {
            randomMovePos.y = 0;
        }

        // ���s����
        yield return character.Move(randomMovePos);

        // ���̈ړ��p�^�[����ݒ肷��
        // currentMovePattern = (currentMovePattern + 1) % movePattern.Count;

        // idleTimer��������
        idleTimer = 0f;

        // Idle�X�e�[�g�ɑJ��
        state = NPCstate.Idle;
    }


    // Player����̊����ɓ���
    public void Interact(Vector3 initiator)
    {
        // Idle�X�e�[�g�̂Ƃ��A
        if(state == NPCstate.Idle)
        {
            // Dialog�X�e�[�g�ɑJ��
            state = NPCstate.Dialog;

            // �b�������Ă��������ނ�
            character.LookTowards(initiator);

            // NPC�̎��_�C�A���O��\��������
            StartCoroutine(MessageDialogManager.Instance.ShowDialog(dialog, OnDialogFinished));
        }
    }

    // ��b���I�������Idle�X�e�[�g�ɖ߂�
    void OnDialogFinished()
    {
        // Idle�̏������_����X�^�[�g����悤�Ƀ^�C�}�[��������
        idleTimer = 0f;
        state = NPCstate.Idle;
    }
}
