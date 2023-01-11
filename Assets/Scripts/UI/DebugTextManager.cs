using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


// �f�o�b�O���e��\������
public class DebugTextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI debugText1; // �\���p�̃e�L�X�g�{�b�N�X
    [SerializeField] TextMeshProUGUI debugText2; // �\���p�̃e�L�X�g�{�b�N�X
    [SerializeField] TextMeshProUGUI debugText3; // �\���p�̃e�L�X�g�{�b�N�X
    [SerializeField] TextMeshProUGUI debugText4; // �\���p�̃e�L�X�g�{�b�N�X
    [SerializeField] TextMeshProUGUI debugText5; // �\���p�̃e�L�X�g�{�b�N�X

    public string text1; // �e�L�X�g�{�b�N�X�ɑΉ�����e�L�X�g
    public string text2; // �e�L�X�g�{�b�N�X�ɑΉ�����e�L�X�g
    public string text3; // �e�L�X�g�{�b�N�X�ɑΉ�����e�L�X�g
    public string text4; // �e�L�X�g�{�b�N�X�ɑΉ�����e�L�X�g
    public string text5; // �e�L�X�g�{�b�N�X�ɑΉ�����e�L�X�g


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
