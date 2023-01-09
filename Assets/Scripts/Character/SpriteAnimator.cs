using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator
{
    SpriteRenderer spriteRenderer;�@// �`�悷�邽�߂�SpriteRender

    List<Sprite> frames; // �A�j���[�V�����̊e�t���[��
    public List<Sprite> Frames { get => frames; }

    float framerate; // �`��Ԋu

    int currentFrame; // ���݂̃t���[��
    float timer; // ���Ԍv���p


    // �����ݒ�
    public SpriteAnimator(SpriteRenderer spriteRenderer, List<Sprite> frames, float framerate = 0.16f)
    {
        this.spriteRenderer = spriteRenderer;
        this.frames = frames;
        this.framerate = framerate;
    }

    // �A�j���[�V�����J�n
    public void Start()
    {
        currentFrame = 0;
        timer = 0;
        spriteRenderer.sprite = frames[currentFrame];
    }

    // �A�j���[�V�������X�V����
    public void HandleUpdate()
    {
        timer += Time.deltaTime;

        // timer��frameRate�𒴂����玟�̉摜��`�悷��
        if(timer>framerate)
        {
            currentFrame = (currentFrame + 1)%frames.Count; // ���݂̃t���[����1�𑫂��i���[�v�j
            spriteRenderer.sprite = frames[currentFrame]; // �V�����t���[����`��
            timer -= framerate; // timer�̍ď�����
        }
    }
}
