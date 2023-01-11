using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


// デバッグ内容を表示する
public class DebugTextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI debugText1; // 表示用のテキストボックス
    [SerializeField] TextMeshProUGUI debugText2; // 表示用のテキストボックス
    [SerializeField] TextMeshProUGUI debugText3; // 表示用のテキストボックス
    [SerializeField] TextMeshProUGUI debugText4; // 表示用のテキストボックス
    [SerializeField] TextMeshProUGUI debugText5; // 表示用のテキストボックス

    public string text1; // テキストボックスに対応するテキスト
    public string text2; // テキストボックスに対応するテキスト
    public string text3; // テキストボックスに対応するテキスト
    public string text4; // テキストボックスに対応するテキスト
    public string text5; // テキストボックスに対応するテキスト


    public static DebugTextManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        debugText1.text = text1;
        debugText2.text = text2;
        debugText3.text = text3;
        debugText4.text = text4;
        debugText5.text = text5;
    }
}
