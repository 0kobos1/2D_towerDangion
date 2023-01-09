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
    [SerializeField] List<Vector2> movePatten;

    Character character;
    NPCstate state;

    float idleTimer;
    int currentMovePattern;

    private void Awake()
    {
        currentMovePattern = 0;
        state = NPCstate.Idle;
        character = GetComponent<Character>();
    }

    private void Update()
    {
        character.HandleUpdate();

        // 2�b�Ԋu�ŃX�e�[�g��"Walk"�ɕύX����
        if (state == NPCstate.Idle)
        {
            idleTimer += Time.deltaTime;
            
            if(idleTimer > timeBetweenPattern)
            {
                idleTimer = 0f;
                StartCoroutine(Walk());
            }
        }
    }

    // �������s
    IEnumerator Walk()
    {
        // ���݂̃X�e�[�g��Walk�ɕύX
        state = NPCstate.Walk;
        
        // ���݈ʒu��oldPosition�Ɋi�[����
        Vector3 oldPosition = transform.position;

        // ���s����
        yield return character.Move(movePatten[currentMovePattern]);

        // �ړ������O�̃|�W�V�����ƈړ�������̃|�W�V�����������łȂ���΁i�ړ����Ă���΁j
        if(oldPosition != transform.position)
        {
            // ���̈ړ��p�^�[����ݒ肷��
            currentMovePattern = (currentMovePattern + 1) % movePatten.Count;
        }
        
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
            StartCoroutine(TalkDialogManager.Instance.ShowDialog(dialog, OnDialogFinished));
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
