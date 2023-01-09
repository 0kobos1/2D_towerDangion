using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator
{
    SpriteRenderer spriteRenderer;　// 描画するためのSpriteRender

    List<Sprite> frames; // アニメーションの各フレーム
    public List<Sprite> Frames { get => frames; }

    float framerate; // 描画間隔

    int currentFrame; // 現在のフレーム
    float timer; // 時間計測用


    // 初期設定
    public SpriteAnimator(SpriteRenderer spriteRenderer, List<Sprite> frames, float framerate = 0.16f)
    {
        this.spriteRenderer = spriteRenderer;
        this.frames = frames;
        this.framerate = framerate;
    }

    // アニメーション開始
    public void Start()
    {
        currentFrame = 0;
        timer = 0;
        spriteRenderer.sprite = frames[currentFrame];
    }

    // アニメーションを更新する
    public void HandleUpdate()
    {
        timer += Time.deltaTime;

        // timerがframeRateを超えたら次の画像を描画する
        if(timer>framerate)
        {
            currentFrame = (currentFrame + 1)%frames.Count; // 現在のフレームに1を足す（ループ）
            spriteRenderer.sprite = frames[currentFrame]; // 新しいフレームを描画
            timer -= framerate; // timerの再初期化
        }
    }
}
