using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


// 会話を表示する
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

    // 与えられた文章を表示する
    public IEnumerator ShowDialog(Dialog dialog, UnityAction onDialogFinished)
    {
        // フレーム終わりまでまつ（バグ回避）
        yield return new WaitForEndOfFrame();

        // ダイアログ終了時にステートを"Idle"に戻す関数を登録
        OnDialogFinished = onDialogFinished;

        // OnShowDialogを実行（NPCのステートを"Dialog"に遷移）
        GameController.Instance.SetCurrentState(GameState.Dialog);
        
        // このスクリプトのdialog変数に引数のdialogを設定
        this.dialog = dialog;

        // ダイアログ表示中
        IsShowing = true;

        // DialogBoxを表示
        talkDialogBox.SetActive(true);

        // ダイアログをタイプ表示する
        StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
    }


    public void HandleUpdate()
    {
        // タイピング中でないときに、Zボタンを押したら
        if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {
            currentLine++;

            // まだ文が残っていれば
            if (currentLine < dialog.Lines.Count) 
            {
                // 次の文を表示する
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            // 残っていなければ
            else
            {
                // 会話終了

                // "OnDialogFinished"を実行（ステートをIdleに戻す）
                OnDialogFinished?.Invoke();

                // ダイアログ非表示
                talkDialogBox.SetActive(false);

                // 現在のダイアログ表示ラインを0に戻す
                currentLine = 0;

                // ダイアログ非表示中
                IsShowing = false;

                // "OnCloseDialog"を実行（ステートをFreeRoamに戻す）
                GameController.Instance.SetCurrentState(GameState.FreeRoam);
            }
            
        }
    }

    // タイプ形式で文字を表示
    IEnumerator TypeDialog(string line)
    {
        // タイピング中
        isTyping = true;

        // 最初は空白からはじめる
        talkDialogText.text = "";

        // 文に含まれる文字を
        foreach(char letter in line.ToCharArray())
        {
            // 一文字ずつ表示させる
            talkDialogText.text += letter;

            // 表示時間間隔分待機する
            yield return new WaitForSeconds(1f/letterPerSecond);
        }

        // タイピング終了
        isTyping = false;
    }
}
