using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


// ��b��\������
public class TalkDialogManager : MonoBehaviour
{
    [SerializeField] GameObject talkDialogBox;
    [SerializeField] TextMeshProUGUI talkDialogText;
    [SerializeField] float letterPerSecond;

    public UnityAction OnCloseDialog;
    public UnityAction OnDialogFinished;

    public static TalkDialogManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    Dialog dialog;
    int currentLine = 0;
    bool isTyping;
    public bool IsShowing { get; private set; }

    // �^����ꂽ���͂�\������
    public IEnumerator ShowDialog(Dialog dialog, UnityAction onDialogFinished)
    {
        // �t���[���I���܂ł܂i�o�O����j
        yield return new WaitForEndOfFrame();

        // �_�C�A���O�I�����ɃX�e�[�g��"Idle"�ɖ߂��֐���o�^
        OnDialogFinished = onDialogFinished;

        // OnShowDialog�����s�iNPC�̃X�e�[�g��"Dialog"�ɑJ�ځj
        GameController.Instance.SetCurrentState(GameState.Dialog);
        
        // ���̃X�N���v�g��dialog�ϐ��Ɉ�����dialog��ݒ�
        this.dialog = dialog;

        // �_�C�A���O�\����
        IsShowing = true;

        // DialogBox��\��
        talkDialogBox.SetActive(true);

        // �_�C�A���O���^�C�v�\������
        StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
    }


    public void HandleUpdate()
    {
        // �^�C�s���O���łȂ��Ƃ��ɁAZ�{�^������������
        if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {
            currentLine++;

            // �܂������c���Ă����
            if (currentLine < dialog.Lines.Count) 
            {
                // ���̕���\������
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            // �c���Ă��Ȃ����
            else
            {
                // ��b�I��

                // "OnDialogFinished"�����s�i�X�e�[�g��Idle�ɖ߂��j
                OnDialogFinished?.Invoke();

                // �_�C�A���O��\��
                talkDialogBox.SetActive(false);

                // ���݂̃_�C�A���O�\�����C����0�ɖ߂�
                currentLine = 0;

                // �_�C�A���O��\����
                IsShowing = false;

                // "OnCloseDialog"�����s�i�X�e�[�g��FreeRoam�ɖ߂��j
                GameController.Instance.SetCurrentState(GameState.FreeRoam);
            }
            
        }
    }

    // �^�C�v�`���ŕ�����\��
    IEnumerator TypeDialog(string line)
    {
        // �^�C�s���O��
        isTyping = true;

        // �ŏ��͋󔒂���͂��߂�
        talkDialogText.text = "";

        // ���Ɋ܂܂�镶����
        foreach(char letter in line.ToCharArray())
        {
            // �ꕶ�����\��������
            talkDialogText.text += letter;

            // �\�����ԊԊu���ҋ@����
            yield return new WaitForSeconds(1f/letterPerSecond);
        }

        // �^�C�s���O�I��
        isTyping = false;
    }
}
