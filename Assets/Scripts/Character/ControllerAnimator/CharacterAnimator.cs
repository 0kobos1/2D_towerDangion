using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] List<Sprite> walkDownSprites;
    [SerializeField] List<Sprite> walkUpSprites;
    [SerializeField] List<Sprite> walkRightSprites;
    [SerializeField] List<Sprite> walkLeftSprites;

    [SerializeField] SpriteAnimator walkDownAnim;
    [SerializeField] SpriteAnimator walkUpAnim;
    [SerializeField] SpriteAnimator walkRightAnim;
    [SerializeField] SpriteAnimator walkLeftAnim;

    public float MoveX { get; set; }
    public float MoveY { get; set; }
    public bool IsMoving { get; set; }

    SpriteAnimator currentAnim;
    SpriteRenderer spriteRender;

    bool wasPreviouslyMoving;

    private void Start()
    {
        spriteRender= GetComponent<SpriteRenderer>();

        walkDownAnim = new SpriteAnimator(spriteRender, walkDownSprites);
        walkUpAnim = new SpriteAnimator(spriteRender, walkUpSprites);
        walkRightAnim = new SpriteAnimator(spriteRender, walkRightSprites);
        walkLeftAnim = new SpriteAnimator(spriteRender, walkLeftSprites);

        currentAnim = walkDownAnim;
    }

    private void Update()
    {
        SpriteAnimator previousAnim = currentAnim;


        if (MoveX == 1)
        {
            currentAnim = walkRightAnim;
        }
        else if (MoveX == -1)
        {
            currentAnim = walkLeftAnim;
        }
        else if (MoveY == 1)
        {
            currentAnim = walkUpAnim;
        }
        else if (MoveY == -1)
        {
            currentAnim = walkDownAnim;
        }

        // �O�̃A�j���[�V�����Ə�Ԃ��Ⴄ�A�܂��͒�~�̈ړ��̑J�ڂ�����΃X�^�[�g
        if(previousAnim != currentAnim || wasPreviouslyMoving != IsMoving)
        {
            currentAnim.Start();
        }

        // �����Ă���Ƃ��̓t���[�����X�V
        if(IsMoving)
        {
            currentAnim.HandleUpdate();
        }

        // �����Ă��Ȃ��Ƃ��͍ŏ��̃t���[����`��
        else
        {
            spriteRender.sprite = currentAnim.Frames[0];
        }

        wasPreviouslyMoving = IsMoving;
        
    }
}
