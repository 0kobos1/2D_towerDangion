using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum MenuState
{
    Menu1,
    Menu2,
    ItemUse,
}

// メニュー操作を管理
public class MenuDialogManager : MonoBehaviour
{
    [SerializeField] PlayerStatus playerStatus;

    [SerializeField] GameObject menuDialogBox1;
    [SerializeField] GameObject menuDialogBox2;
    [SerializeField] GameObject menuDialogBox3;

    [SerializeField] GameObject cursor1;
    [SerializeField] GameObject cursor2;

    [SerializeField] GameObject itemText;
    [SerializeField] GameObject equipmentText;
    [SerializeField] GameObject itemContentText1;
    [SerializeField] GameObject itemContentText2;
    [SerializeField] GameObject itemContentText3;
    [SerializeField] GameObject itemContentText4;
    [SerializeField] GameObject itemContentText5;
    [SerializeField] GameObject itemContentText6;

    [SerializeField] GameObject descriptionText;


    int currentPos1; // Menu1のカーソルの現在位置
    int currentPos2; // Menu2のカーソルの現在位置
    int currentItemSelection; // 選択中のアイテム番号
    int itemCountMax; // アイテム所持数の上限値
    float cursorMovingTimer = 0; // カーソル移動のキーボード受付入力タイマー
    float cursorMovingBetweenTime = 0.2f; // カーソル移動のキーボード受付入力間隔 
    bool isInMenu; // メニュー画面に入っているかどうか
    bool isCursorMoving; // カーソル移動中かどうか
    MenuState menuState;

    List<HealingItemBase> healingItems;
    List<WeaponBase> weapons;
    List<GameObject> itemContentTexts;

    private void Awake()
    {
        // MenuDialogBoxを非アクティブ状態にする
        menuDialogBox1.SetActive(false);
        menuDialogBox2.SetActive(false);
        menuDialogBox3.SetActive(false);

        // itemContentsTextのリストを作成
        itemContentTexts = new List<GameObject>(){itemContentText1, itemContentText2, itemContentText3, itemContentText4, itemContentText5, itemContentText6};
    }

    // メニューUIを立ち上げる（from GameController）
    public void SetUp()
    {
        // 手持ち回復アイテム、武器の情報を取得する
        healingItems = playerStatus.HealingItems;
        weapons = playerStatus.Weapons;

        // メニューを開いている状態であれば、
        if (isInMenu)
        {
            // SetUpを抜ける（繰り返しSetUpされないようにする）
            return;
        }

        menuState = MenuState.Menu1;

        // 現在のカーソル位置を初期化する
        currentPos1 = 0;

        // ハンドカーソルの位置を設定
        CursorUpdate1(currentPos1);

        // MenuDialogBoxをアクティブ状態にする
        menuDialogBox1.SetActive(true);

        // メニューオープン状態にする
        isInMenu = true;
    }

    private void Update()
    {
        // メニューを開いていない状態であれば
        if(!isInMenu)
        {
            // Updateを抜ける
            return;
        }

        // カーソル移動中であれば
        if (isCursorMoving)
        {
            // タイマーが時間間隔より小さいなら
            if(cursorMovingTimer < cursorMovingBetweenTime) 
            {
                // タイマーに経過時間をたして
                cursorMovingTimer += Time.deltaTime;

                // Updateをぬける
                return;
            }

            // タイマーが時間間隔より大きいなら
            else
            {
                // タイマーから時間間隔を引いて
                cursorMovingTimer -= cursorMovingBetweenTime;

                // カーソル移動状態を解除する
                isCursorMoving = false;
            }
        }

        // メニュー1のステートのとき
        if(menuState == MenuState.Menu1) 
        {
            // Xボタン（キャンセルキー）が押されたら
            if(Input.GetKeyDown(KeyCode.X)) 
            {
                // ダイアログボックスを閉じる
                menuDialogBox1.SetActive(false);
                
                // メニューオープン状態を解除
                isInMenu = false;

                // 自由移動状態に戻す
                GameController.Instance.SetCurrentState(GameState.FreeRoam);

                // Updateを抜ける
                return;
            }

            // Zボタン（決定キー）が押されたら、
            if (Input.GetKeyDown(KeyCode.Z))
            {
                // 次の入力が一定時間入らないようにして
                isCursorMoving = true;

                // currentPos1が0の時（回復アイテムのとき）
                if(currentPos1 == 0)
                {
                    // 手持ちの回復アイテムの個数が6個以下であれば、（表示限界数以下なら）
                    if(healingItems.Count <= 6)
                    {
                        // 手持ちアイテムをメニューボックスに反映させる
                        for (int i = 0; i < healingItems.Count; i++)
                        {
                            itemContentTexts[i].GetComponent<TextMeshProUGUI>().text = healingItems[i].Name;
                        }

                        // 残りを空白で埋める
                        for (int i = healingItems.Count; i < 6; i++)
                        {
                            itemContentTexts[i].GetComponent<TextMeshProUGUI>().text = "";
                        }
                    }

                    // 手持ちの回復アイテムの個数が7個以上であれば、（表示限界数を超えていれば）
                    else
                    {
                        // 手持ちアイテムをメニューボックスに反映させる
                        for (int i = 0; i < 6; i++)
                        {
                            itemContentTexts[i].GetComponent<TextMeshProUGUI>().text = healingItems[i].Name;
                        }
                    }
                }

                // Menu2ステートにうつる
                menuState = MenuState.Menu2;

                // Menu2のダイアログを開く
                menuDialogBox2.SetActive(true);

                // Menu3のダイアログを開く
                menuDialogBox3.SetActive(true);

                // Updateを抜ける
                return;
            }

            // 下矢印の入力があれば
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // 次の入力が一定時間入らないようにして
                isCursorMoving = true;

                // currentPosに1を足す
                currentPos1++;

                // 2で割った余りをもとめることでcurrentPosを0か1にする
                currentPos1 %= 2;
                currentPos1 = Mathf.Abs(currentPos1);

                // ハンドカーソルの位置を設定
                CursorUpdate1(currentPos1);

                // Updateを抜ける
                return;
            }

            // 上矢印の入力があれば        
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // 次の入力が一定時間入らないようにして
                isCursorMoving = true;

                // currentPosから1を引く
                currentPos1--;

                // 2で割った余りをもとめることでcurrentPosを0か1にする
                currentPos1 %= 2;
                currentPos1 = Mathf.Abs(currentPos1);

                // ハンドカーソルの位置を設定
                CursorUpdate1(currentPos1);

                // Updateを抜ける
                return;
            }
        }

        // メニュー2のステートのとき
        if (menuState == MenuState.Menu2)
        {
            // Zボタン（決定キー）が押されたら
            if (Input.GetKeyDown(KeyCode.Z))
            {
                // カーソル移動状態にする
                isCursorMoving = true;

                Debug.Log("アイテムをつかった！");

                int oldHealingItemsCount = healingItems.Count;

                // アイテムを使用する
                playerStatus.UseHealingItem(currentItemSelection);

                // StatusDialogを更新する
                StatusDialogManager.Instance.DialogUpdate();


                // currentItemSelectionが最大値であれば
                if (currentItemSelection == oldHealingItemsCount - 1)
                {
                    // currentItemSelectionが6以上の時
                    if(currentItemSelection >= 6)
                    {
                        // アイテムを使用した分だけ一つ減らしておく
                        currentItemSelection--; 
                    }

                    // currentItemSelectionが5以下の時
                    else
                    {
                        // アイテムを使用した分だけ一つ減らしておく
                        currentItemSelection--;

                        // カーソルポジションも一つ上にあげる
                        currentPos2--;
                    }
                }

                // currentItemSelectionが最大値でなければ
                else
                {
                    // currentItemPosition、currentPosともにそのまま

                    
                }

                // playerStatusのHealingItemsを再読み込み
                healingItems = playerStatus.HealingItems;

                // 描画テキストを一度空にする
                ClearText();

                // 0から5（もしくはアイテム個数の最大値）までの範囲のテキストに
                for (int i = 0; i <= Mathf.Min(healingItems.Count, 5); i++)
                {
                    // currentPosにcurrentItemSelectionを書き込む
                    // .....
                    // itemContentText[currentPos2 -1] = healingItems[currentItemSelection -1]
                    // itemContentText[currentPos2] = healingItems[currentItemSelection]
                    // itemContentText[currentPos2 +1] = healingItems[currentItemSelection +1]
                    itemContentTexts[i].GetComponent<TextMeshProUGUI>().text = healingItems[currentItemSelection - currentPos2 + i].Name;
                }


                //描画テキストを更新
                // ChooseText(currentItemSelection, currentPos2);

                return;
            }

            // Xボタン（キャンセルキー）が押されたら
            else if (Input.GetKeyDown(KeyCode.X))
            {
                // ダイアログボックスを閉じる
                menuDialogBox2.SetActive(false);
                menuDialogBox3.SetActive(false);

                // menuStateを1にする
                menuState = MenuState.Menu1;

                // カーソル移動状態にする
                isCursorMoving = true;

                // Updateを抜ける
                return;
            }

            // 下矢印の入力があれば
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // 次の入力が一定時間入らないようにして
                isCursorMoving = true;

                // 回復アイテムの所持数が6以下のとき
                if (healingItems.Count <= 6)
                {
                    // currentItemSelectionが最大値ではないとき
                    if (currentItemSelection != healingItems.Count - 1)
                    {
                        // currentItemSelectionに1を足す
                        currentItemSelection++;
                    }

                    // currentItemSelectionが最大値のとき
                    else
                    {
                        // currentItemSelectionを0に戻す
                        currentItemSelection = 0;
                    }

                    // カーソルポジションはcurrentItemSelectionと同じに設定
                    currentPos2 = currentItemSelection;
                }

                // 回復アイテムの所持数が6以上のとき
                else
                {
                    // currentItemSelectionが最大値ではないとき
                    if (currentItemSelection != healingItems.Count - 1)
                    {
                        currentItemSelection++;

                        // cursorPos2が5ではないとき
                        if (currentPos2 != 5)
                        {
                            // currentPos2に1を足す
                            currentPos2++;
                        }
                        // cursorPos2が5のとき
                        else
                        {
                            // currentPos2をそのまま5に設定する
                            currentPos2 = 5;
                        }
                    }
                    // currentItemSelectionが最大値のとき
                    else
                    {
                        // currentItemSelectionを0にもどす
                        currentItemSelection = 0;
                        // currentPosを0に戻す
                        currentPos2 = 0;
                    }
                }
                //Menu2を更新する
                Menu2Update();

                // Updateを抜ける
                return;
            }

            // 上矢印の入力があれば        
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // 次の入力が一定時間入らないようにして
                isCursorMoving = true;

                // 回復アイテムの所持数が6以下のとき
                if (healingItems.Count <= 6)
                {
                    // currentItemSelectionが0ではないとき
                    if (currentItemSelection != 0)
                    {
                        // currentItemSelectionから1を引く
                        currentItemSelection--;
                    }

                    // currentItemSelectionが0のとき
                    else
                    {
                        // currentItemSelectionを最大値にする
                        currentItemSelection = healingItems.Count-1;
                    }
                    // カーソルポジションはcurrentItemSelectionと同じに設定
                    currentPos2 = currentItemSelection;
                }

                // 回復アイテムの所持数が6以上のとき
                else
                {
                    // currentItemSelectionが0ではないとき
                    if (currentItemSelection != 0)
                    {
                        // currentItemSelectionから1を引く
                        currentItemSelection--;

                        // cursorPos2が0ではないとき
                        if (currentPos2 != 0)
                        {
                            // currentPos2から1を引く
                            currentPos2--;
                        }
                        // cursorPos2が0のとき
                        else
                        {
                            // currentPos2を0のままにする
                            currentPos2 = 0;
                        }
                    }
                    // currentItemSelectionが0のとき
                    else
                    {
                        // currentItemSelectionを最大値に設定
                        currentItemSelection = healingItems.Count - 1;

                        // currentPos2は一番下（5）に設定
                        currentPos2 = 5;
                    }
                }
                //Menu2を更新する
                Menu2Update();
            }       
        }
    }

    // Menu2の描画を更新する
    void Menu2Update()
    {
        //描画テキストを更新
        ChooseText(currentItemSelection, currentPos2);

        // ハンドカーソルの位置を更新
        CursorUpdate2(currentPos2);

        // descriptionTextを更新
        descriptionText.GetComponent<TextMeshProUGUI>().text = healingItems[currentItemSelection].Description;
    }

    // 描画テキストをクリアする
    void ClearText()
    {
        foreach (GameObject itemContentText in itemContentTexts)
        {
            itemContentText.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    // 現在のカーソル位置とアイテム選択番号からダイアログに表示するテキストを選択する
    void ChooseText(int currentItemSelection, int currentPos2)
    {
        for (int i = 0; i <= currentPos2 - 1; i++)
        {
            itemContentTexts[i].GetComponent<TextMeshProUGUI>().text = healingItems[currentItemSelection - currentPos2 + i].Name;
        }
        for (int i = currentPos2; i <= Mathf.Min(healingItems.Count - 1, 5); i++)
        {
            itemContentTexts[i].GetComponent<TextMeshProUGUI>().text = healingItems[currentItemSelection - currentPos2 + i].Name;
        }
    }

    void CursorUpdate1(int currentPos1)
    {
        // currentPosがゼロの時は
        if (currentPos1 == 0)
        {
            // ハンドカーソルをitemTextの左におく
            cursor1.transform.localPosition = new Vector3(itemText.transform.localPosition.x - 130, itemText.transform.localPosition.y, -10);
        }

        else if (currentPos1 == 1)
        {
            // ハンドカーソルをequipmentTextの横におく
            cursor1.transform.localPosition = new Vector3(equipmentText.transform.localPosition.x - 130, equipmentText.transform.localPosition.y, -10);
        }
    }

    void CursorUpdate2(int currentPos2)
    {
        // currentPosがゼロの時は
        if (currentPos2 == 0)
        {
            // ハンドカーソルをitemContentText1の左におく
            cursor2.transform.localPosition = new Vector3(itemContentText1.transform.localPosition.x - 130, itemContentText1.transform.localPosition.y, -10);
        }

        else if (currentPos2 == 1)
        {
            // ハンドカーソルをitemContentText2の横におく
            cursor2.transform.localPosition = new Vector3(itemContentText2.transform.localPosition.x - 130, itemContentText2.transform.localPosition.y, -10);
        }

        else if (currentPos2 == 2)
        {
            // ハンドカーソルをitemContentText3の横におく
            cursor2.transform.localPosition = new Vector3(itemContentText3.transform.localPosition.x - 130, itemContentText3.transform.localPosition.y, -10);
        }

        else if (currentPos2 == 3)
        {
            // ハンドカーソルをitemContentText4の横におく
            cursor2.transform.localPosition = new Vector3(itemContentText4.transform.localPosition.x - 130, itemContentText4.transform.localPosition.y, -10);
        }

        else if (currentPos2 == 4)
        {
            // ハンドカーソルをitemContentText5の横におく
            cursor2.transform.localPosition = new Vector3(itemContentText5.transform.localPosition.x - 130, itemContentText5.transform.localPosition.y, -10);
        }

        else if (currentPos2 == 5)
        {
            // ハンドカーソルをitemContentText6の横におく
            cursor2.transform.localPosition = new Vector3(itemContentText6.transform.localPosition.x - 130, itemContentText6.transform.localPosition.y, -10);
        }
    }

}
