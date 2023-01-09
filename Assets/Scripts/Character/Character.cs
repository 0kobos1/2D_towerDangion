using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Windows;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] float moveSpeed; // �ړ����x
    CharacterAnimator characterAnimator; // �A�j���[�V����
    public bool IsMoving { get; set; } // �ړ������ǂ���

    public CharacterAnimator CharacterAnimator { get => characterAnimator; }


    private void Awake()
    {
        characterAnimator = GetComponent<CharacterAnimator>();
    }

    public void HandleUpdate()
    {
        // �A�j���[�V������"IsMoving"�ɂ�����"IsMoving"��ݒ肷��
        characterAnimator.IsMoving = IsMoving;
    }

    // �ړ��ƃA�j���[�V�������Ǘ�����
    public IEnumerator Move(Vector3 moveVec)
    {   
        // �A�j���[�V������MoveX��moveVec��x��ݒ肵�A-1~1�͈̔͂𒴂��Ȃ��悤�ɂ���
        characterAnimator.MoveX = Mathf.Clamp(moveVec.x, -1f, 1f);

        // �A�j���[�V������MoveY��moveVec��y��ݒ肵�A-1~1�͈̔͂𒴂��Ȃ��悤�ɂ���
        characterAnimator.MoveY = Mathf.Clamp(moveVec.y, -1f, 1f);

        // �ړI�n��ݒ肷��
        Vector3 targetPos = transform.position;
        targetPos += moveVec;

        // �ړ���ɏ�Q��������Ί֐��𔲂���
        if (!IsPathClear(targetPos))
        {
            yield break;
        }

        // �ړ���
        IsMoving = true;

        // targetPos�Ƃ̋��������X�ɂ߂�
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // �\���ɋ߂Â�����ʒu��targetPos�Ɉړ�������
        transform.position = targetPos;

        // �ړ��I��
        IsMoving = false;
    }

    bool IsPathClear(Vector3 targetPos)
    {
        // �ړI�n�ƌ��ݒn�̍����v�Z
        Vector3 diff = targetPos - transform.position;

        // ���ݒn����݂��ړI�n�̕����i����1�ɐ��K���j
        Vector3 dir = diff.normalized;

        // ���ݒn����ړI�n�Ɍ������Ĕ��^�̃��C���΂��ASolidObjectLayer��InteractableLayer�ƐڐG�����邩���m�F����i�����ւ̐ڐG��h�����߁A�ƂȂ�̃}�X�����΂��j
        return !Physics2D.BoxCast(transform.position + dir, new Vector2(0.2f, 0.2f), 0, dir, diff.magnitude - 1,
                                  GameLayers.Instance.SolidObjectLayer | GameLayers.Instance.InteractbleLayer | GameLayers.Instance.PlayerLayer | GameLayers.Instance.EnemyLayer);
    }

    // targetPos�̕���������
    public void LookTowards(Vector3 targetPos)
    {
        // ���ݒn����ړI�n�ւ̌������v�Z�i�؂�̂Ăɂ��Ă������ƂŃo�O����j
        float xDiff = Mathf.Floor(targetPos.x) - Mathf.Floor(transform.position.x);
        float yDiff = Mathf.Floor(targetPos.y) - Mathf.Floor(transform.position.y);

        // x��y���ǂ��炩���[���̂Ƃ������i�΂߂ɂȂ�Ȃ��悤�ɂ��āj
        if (xDiff == 0 || yDiff ==0)
        {
            // �A�j���[�^�[��X��x�����̌���������
            characterAnimator.MoveX = Mathf.Clamp(xDiff, -1f, 1f);

            // �A�j���[�^�[��Y��y�����̌���������
            characterAnimator.MoveY = Mathf.Clamp(yDiff, -1f, 1f);
        }
        
    }
}