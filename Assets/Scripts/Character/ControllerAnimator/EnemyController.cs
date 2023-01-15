using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.Events;

public enum Enemystate
{
    Idle,
    Walk,
}

public class EnemyController : MonoBehaviour
{
    

    [SerializeField] Dialog dialog;

    Character character;
    Enemystate state;
    
    float idleTimer;

    public UnityAction onBattleStart; // �o�g���J�n���ɌĂяo�����

    private void Awake()
    {
        state = Enemystate.Idle;
        character = GetComponent<Character>();
    }

    private void Update()
    {
        // Battle�X�e�[�g����Update���Ȃ�(�SEnemy���~�܂�j
        if(GameController.Instance.GameState == GameState.Battle)
        {
            return;
        }

        // Menu�X�e�[�g����Update���Ȃ�(�SEnemy���~�܂�j
        if (GameController.Instance.GameState == GameState.Menu)
        {
            return;
        }

        character.HandleUpdate();

        // �����_���Ȏ��ԊԊu���擾����
        float timeBetweenPattern = Random.Range(1f, 3f);

        // Idel�X�e�[�g�̏ꍇ�A
        if (state == Enemystate.Idle)
        {
            // idleTimer���X�V
            idleTimer += Time.deltaTime;

            // idleTimer�����ԊԊu�𒴂��Ă����
            if (idleTimer > timeBetweenPattern)
            {
                // idleTimer����������
                idleTimer = 0f;

                // �X�e�[�g��Walk�ɕς�
                state= Enemystate.Walk;

                // RandomWalk�̃R���[�`�����J�n
                StartCoroutine(RandomWalk());
            }
        }
    }

    // ���������_�����s
    IEnumerator RandomWalk()
    {
        // -1, 0, 1�̃����_���ȖړI�n���擾����
        Vector2 randomMovePos;
        ;
        randomMovePos.x = Random.Range(-1, 2);
        randomMovePos.y = Random.Range(-1, 2);

        // �ړ��������[���Ȃ�
        if(randomMovePos == Vector2.zero)
        {
            // idleTimer��������
            idleTimer = 0f;

            // Idle�X�e�[�g�ɑJ��
            state = Enemystate.Idle;

            // �ړ��R���[�`���𔲂���
            yield break;
        }

        // �ړ����W���[���łȂ��Ȃ�    
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

            // Battle�X�e�[�g�ɑJ��
            GameController.Instance.SetCurrentState(GameState.Battle);

            // �����̏���BattleSystem�ɂ�����
            BattleSystem.Instance.GetBattleObjects(touchedPlayer, this.gameObject);

            // ���s�R���[�`�����I������
            yield break;
        }

        // ���s����
        yield return character.Move(randomMovePos);


        // idleTimer��������
        idleTimer = 0f;

        // Idle�X�e�[�g�ɑJ��
        state = Enemystate.Idle;    
    }

    // �v���C���[�Ƃ̐ڐG�𒲂ׂ�
    Collider2D TouchedPlayer(Vector2 moveVec)
    {
        Vector2 targetPos;
        targetPos = (Vector2)transform.position + moveVec;

        // �ړ���Ƀ��C���΂��AEnemyLayer�Ƃ̐ڐG����𒲂ׂ�
        return Physics2D.OverlapCircle(targetPos, 0.2f, GameLayers.Instance.PlayerLayer);
    }
}
