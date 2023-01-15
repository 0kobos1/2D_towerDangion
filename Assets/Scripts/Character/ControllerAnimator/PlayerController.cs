using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �C��
// �Z�G�ƐڐG�����^�C�~���O��GameController��state��Battle�ɕς���

// �v���C���[�̓��쐧��
public class PlayerController : MonoBehaviour
{
    
    Vector2 input; // �L�[�{�[�h���͂��i�[
    
    Character character; // �L�����N�^�[
  
    private void Awake()
    {
        character = GetComponent<Character>();
    }

    // �L�[�{�[�h���͂��󂯕t���āA�ʒu��ς���
    public void HandleUpdate()
    {
        // �ړ����łȂ���΁A
        // V�{�^�����������Ƃ��Ƀ��j���[��ʂ��J��
        if(!character.IsMoving)
        {
            if(Input.GetKeyDown(KeyCode.V)) 
            {
                Debug.Log("���j���[���J�����I");
                GameController.Instance.SetCurrentState(GameState.Menu);
                
            }
        }

        // �ړ����łȂ���΁A
        if (!character.IsMoving)
        {
            // �ړ����͂��󂯕t����
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // �΂ߓ��͂̋֎~
            if (input.x != 0)
            {
                input.y = 0;
            }

            // ���̓��͂�����Ƃ�
            if (input != Vector2.zero)
            {
                // �ړI�n�ɓG�������
                if (TouchedEnemy(input) != null) 
                {
                    // touchedEnemy�ɐڐG���������G�̃Q�[���I�u�W�F�N�g���i�[
                    GameObject touchedEnemy = TouchedEnemy(input).gameObject;

                    // �^�b�`�����G��BattleStatus��BattleSystem�ɑ���
                    BattleSystem.Instance.GetBattleObjects(this.gameObject, touchedEnemy);


                    // GameState��Battle�ɐݒ�
                    GameController.Instance.SetCurrentState(GameState.Battle);
                }

                // �ړ��i�R���[�`���j
                StartCoroutine(character.Move(input));
            }
        }
        character.HandleUpdate();

        Interact();
    }

    void Interact()
    {
        // Z�{�^����������Interact
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // �����Ă������
            Vector3 faceDirection = new Vector3(character.CharacterAnimator.MoveX, character.CharacterAnimator.MoveY);
            // ������ꏊ
            Vector3 interactPos = transform.position + faceDirection;

            // ������ꏊ��Ray���΂�
            Collider2D collider2D = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayers.Instance.InteractbleLayer);

            if (collider2D)
            {
                collider2D.GetComponent<IInteractable>()?.Interact(transform.position);
            }

        }
    }

    // �G�l�~�[�Ƃ̐ڐG�𒲂ׂ�
    Collider2D TouchedEnemy(Vector2 moveVec)
    {
        Vector2 targetPos;
        targetPos = (Vector2)transform.position + moveVec;

        // �ړ���Ƀ��C���΂��AEnemyLayer�Ƃ̐ڐG����𒲂ׂ�
        return Physics2D.OverlapCircle(targetPos, 0.2f, GameLayers.Instance.EnemyLayer);
    }

}