using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public enum Enemystate
{
    Idle,
    Walk,
    Battle,
}

public class EnemyController : MonoBehaviour
{
    [SerializeField] Dialog dialog;
    [SerializeField] List<Vector2> movePatten;

    Character character;
    Enemystate state;

    float idleTimer;
    int currentMovePattern;

    private void Awake()
    {
        currentMovePattern = 0;
        state = Enemystate.Idle;
        character = GetComponent<Character>();
    }

    private void Update()
    {
        character.HandleUpdate();

        // �����_���Ȏ��ԊԊu���擾����
        float timeBetweenPattern = Random.Range(1, 3);

        // timeBetweenPattern�̊Ԋu�ŃX�e�[�g��"Walk"�ɕύX����
        if (state == Enemystate.Idle)
        {
            // idleTimer���X�V
            idleTimer += Time.deltaTime;

            // idleTimer�����ԊԊu�𒴂��Ă����
            if (idleTimer > timeBetweenPattern)
            {
                // idleTimer����������
                idleTimer = 0f;

                // RandomWalk�̃R���[�`�����J�n
                StartCoroutine(RandomWalk());
            }
        }
    }

    // ���������_�����s
    IEnumerator RandomWalk()
    {
        // �����_���ȖړI�n���擾����
        Vector2 randomMovePos;
        ;
        randomMovePos.x = Random.Range(-1, 2);
        randomMovePos.y = Random.Range(-1, 2);

        // �����ʒu�ɕω����Ȃ���΁A
        if(randomMovePos == Vector2.zero)
        {
            // Idle�X�e�[�g�ɑJ�ڂ���
            state = Enemystate.Idle;
        }

        // �ʒu�ɕω��������
        else
        {
            // �΂ߕ����Ɉړ����Ȃ��悤�ɂ���
            if (randomMovePos.x != 0)
            {
                randomMovePos.y = 0;
            }

            // Player�Ƃ̐ڐG���肪�����
            if (TouchedPlayer(randomMovePos) != null)
            {
                // touchedPlayer�ɐڐG���肪������Player�̃Q�[���I�u�W�F�N�g���i�[
                GameObject touchedPlayer = TouchedPlayer(randomMovePos).gameObject;

                // �G�̏�Ԃ�Battle�ɕύX
                state = Enemystate.Battle;

                // �퓬�V�X�e���𗧂��グ��
                BattleSystem.Instance.HandleStart(touchedPlayer, this.gameObject);
            }

            // ���݂̃X�e�[�g��Walk�ɕύX
            state = Enemystate.Walk;

            // ���s����
            yield return character.Move(randomMovePos);

            // ���̈ړ��p�^�[����ݒ肷��
            currentMovePattern = (currentMovePattern + 1) % movePatten.Count;

            // Idle�X�e�[�g�ɑJ��
            state = Enemystate.Idle;
        }      
    }

    bool IsTouchedPlayer(Vector2 moveVec)
    {
        //�ړ���̍��W��targetPos�Ɋi�[
        Vector2 targetPos;
        targetPos = (Vector2)transform.position + moveVec;

        // �ړ���Ƀ��C���΂��AEnemyLayer�Ƃ̐ڐG����𒲂ׂ�
        return Physics2D.OverlapCircle(targetPos, 0.2f, GameLayers.Instance.PlayerLayer);
    }


    Collider2D TouchedPlayer(Vector2 moveVec)
    {
        Vector2 targetPos;
        targetPos = (Vector2)transform.position + moveVec;

        // �ړ���Ƀ��C���΂��AEnemyLayer�Ƃ̐ڐG����𒲂ׂ�
        return Physics2D.OverlapCircle(targetPos, 0.2f, GameLayers.Instance.PlayerLayer);
    }
}
