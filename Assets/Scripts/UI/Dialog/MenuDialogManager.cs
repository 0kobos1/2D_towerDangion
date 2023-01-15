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

// ���j���[������Ǘ�
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


    int currentPos1; // Menu1�̃J�[�\���̌��݈ʒu
    int currentPos2; // Menu2�̃J�[�\���̌��݈ʒu
    int currentItemSelection; // �I�𒆂̃A�C�e���ԍ�
    int itemCountMax; // �A�C�e���������̏���l
    float cursorMovingTimer = 0; // �J�[�\���ړ��̃L�[�{�[�h��t���̓^�C�}�[
    float cursorMovingBetweenTime = 0.2f; // �J�[�\���ړ��̃L�[�{�[�h��t���͊Ԋu 
    bool isInMenu; // ���j���[��ʂɓ����Ă��邩�ǂ���
    bool isCursorMoving; // �J�[�\���ړ������ǂ���
    MenuState menuState;

    List<HealingItemBase> healingItems;
    List<WeaponBase> weapons;
    List<GameObject> itemContentTexts;

    private void Awake()
    {
        // MenuDialogBox���A�N�e�B�u��Ԃɂ���
        menuDialogBox1.SetActive(false);
        menuDialogBox2.SetActive(false);
        menuDialogBox3.SetActive(false);

        // itemContentsText�̃��X�g���쐬
        itemContentTexts = new List<GameObject>(){itemContentText1, itemContentText2, itemContentText3, itemContentText4, itemContentText5, itemContentText6};
    }

    // ���j���[UI�𗧂��グ��ifrom GameController�j
    public void SetUp()
    {
        // �莝���񕜃A�C�e���A����̏����擾����
        healingItems = playerStatus.HealingItems;
        weapons = playerStatus.Weapons;

        // ���j���[���J���Ă����Ԃł���΁A
        if (isInMenu)
        {
            // SetUp�𔲂���i�J��Ԃ�SetUp����Ȃ��悤�ɂ���j
            return;
        }

        menuState = MenuState.Menu1;

        // ���݂̃J�[�\���ʒu������������
        currentPos1 = 0;

        // �n���h�J�[�\���̈ʒu��ݒ�
        CursorUpdate1(currentPos1);

        // MenuDialogBox���A�N�e�B�u��Ԃɂ���
        menuDialogBox1.SetActive(true);

        // ���j���[�I�[�v����Ԃɂ���
        isInMenu = true;
    }

    private void Update()
    {
        // ���j���[���J���Ă��Ȃ���Ԃł����
        if(!isInMenu)
        {
            // Update�𔲂���
            return;
        }

        // �J�[�\���ړ����ł����
        if (isCursorMoving)
        {
            // �^�C�}�[�����ԊԊu��菬�����Ȃ�
            if(cursorMovingTimer < cursorMovingBetweenTime) 
            {
                // �^�C�}�[�Ɍo�ߎ��Ԃ�������
                cursorMovingTimer += Time.deltaTime;

                // Update���ʂ���
                return;
            }

            // �^�C�}�[�����ԊԊu���傫���Ȃ�
            else
            {
                // �^�C�}�[���玞�ԊԊu��������
                cursorMovingTimer -= cursorMovingBetweenTime;

                // �J�[�\���ړ���Ԃ���������
                isCursorMoving = false;
            }
        }

        // ���j���[1�̃X�e�[�g�̂Ƃ�
        if(menuState == MenuState.Menu1) 
        {
            // X�{�^���i�L�����Z���L�[�j�������ꂽ��
            if(Input.GetKeyDown(KeyCode.X)) 
            {
                // �_�C�A���O�{�b�N�X�����
                menuDialogBox1.SetActive(false);
                
                // ���j���[�I�[�v����Ԃ�����
                isInMenu = false;

                // ���R�ړ���Ԃɖ߂�
                GameController.Instance.SetCurrentState(GameState.FreeRoam);

                // Update�𔲂���
                return;
            }

            // Z�{�^���i����L�[�j�������ꂽ��A
            if (Input.GetKeyDown(KeyCode.Z))
            {
                // ���̓��͂���莞�ԓ���Ȃ��悤�ɂ���
                isCursorMoving = true;

                // currentPos1��0�̎��i�񕜃A�C�e���̂Ƃ��j
                if(currentPos1 == 0)
                {
                    // �莝���̉񕜃A�C�e���̌���6�ȉ��ł���΁A�i�\�����E���ȉ��Ȃ�j
                    if(healingItems.Count <= 6)
                    {
                        // �莝���A�C�e�������j���[�{�b�N�X�ɔ��f������
                        for (int i = 0; i < healingItems.Count; i++)
                        {
                            itemContentTexts[i].GetComponent<TextMeshProUGUI>().text = healingItems[i].Name;
                        }

                        // �c����󔒂Ŗ��߂�
                        for (int i = healingItems.Count; i < 6; i++)
                        {
                            itemContentTexts[i].GetComponent<TextMeshProUGUI>().text = "";
                        }
                    }

                    // �莝���̉񕜃A�C�e���̌���7�ȏ�ł���΁A�i�\�����E���𒴂��Ă���΁j
                    else
                    {
                        // �莝���A�C�e�������j���[�{�b�N�X�ɔ��f������
                        for (int i = 0; i < 6; i++)
                        {
                            itemContentTexts[i].GetComponent<TextMeshProUGUI>().text = healingItems[i].Name;
                        }
                    }
                }

                // Menu2�X�e�[�g�ɂ���
                menuState = MenuState.Menu2;

                // Menu2�̃_�C�A���O���J��
                menuDialogBox2.SetActive(true);

                // Menu3�̃_�C�A���O���J��
                menuDialogBox3.SetActive(true);

                // Update�𔲂���
                return;
            }

            // �����̓��͂������
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // ���̓��͂���莞�ԓ���Ȃ��悤�ɂ���
                isCursorMoving = true;

                // currentPos��1�𑫂�
                currentPos1++;

                // 2�Ŋ������]������Ƃ߂邱�Ƃ�currentPos��0��1�ɂ���
                currentPos1 %= 2;
                currentPos1 = Mathf.Abs(currentPos1);

                // �n���h�J�[�\���̈ʒu��ݒ�
                CursorUpdate1(currentPos1);

                // Update�𔲂���
                return;
            }

            // ����̓��͂������        
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // ���̓��͂���莞�ԓ���Ȃ��悤�ɂ���
                isCursorMoving = true;

                // currentPos����1������
                currentPos1--;

                // 2�Ŋ������]������Ƃ߂邱�Ƃ�currentPos��0��1�ɂ���
                currentPos1 %= 2;
                currentPos1 = Mathf.Abs(currentPos1);

                // �n���h�J�[�\���̈ʒu��ݒ�
                CursorUpdate1(currentPos1);

                // Update�𔲂���
                return;
            }
        }

        // ���j���[2�̃X�e�[�g�̂Ƃ�
        if (menuState == MenuState.Menu2)
        {
            // Z�{�^���i����L�[�j�������ꂽ��
            if (Input.GetKeyDown(KeyCode.Z))
            {
                // �J�[�\���ړ���Ԃɂ���
                isCursorMoving = true;

                Debug.Log("�A�C�e�����������I");

                int oldHealingItemsCount = healingItems.Count;

                // �A�C�e�����g�p����
                playerStatus.UseHealingItem(currentItemSelection);

                // StatusDialog���X�V����
                StatusDialogManager.Instance.DialogUpdate();


                // currentItemSelection���ő�l�ł����
                if (currentItemSelection == oldHealingItemsCount - 1)
                {
                    // currentItemSelection��6�ȏ�̎�
                    if(currentItemSelection >= 6)
                    {
                        // �A�C�e�����g�p��������������炵�Ă���
                        currentItemSelection--; 
                    }

                    // currentItemSelection��5�ȉ��̎�
                    else
                    {
                        // �A�C�e�����g�p��������������炵�Ă���
                        currentItemSelection--;

                        // �J�[�\���|�W�V���������ɂ�����
                        currentPos2--;
                    }
                }

                // currentItemSelection���ő�l�łȂ����
                else
                {
                    // currentItemPosition�AcurrentPos�Ƃ��ɂ��̂܂�

                    
                }

                // playerStatus��HealingItems���ēǂݍ���
                healingItems = playerStatus.HealingItems;

                // �`��e�L�X�g����x��ɂ���
                ClearText();

                // 0����5�i�������̓A�C�e�����̍ő�l�j�܂ł͈̔͂̃e�L�X�g��
                for (int i = 0; i <= Mathf.Min(healingItems.Count, 5); i++)
                {
                    // currentPos��currentItemSelection����������
                    // .....
                    // itemContentText[currentPos2 -1] = healingItems[currentItemSelection -1]
                    // itemContentText[currentPos2] = healingItems[currentItemSelection]
                    // itemContentText[currentPos2 +1] = healingItems[currentItemSelection +1]
                    itemContentTexts[i].GetComponent<TextMeshProUGUI>().text = healingItems[currentItemSelection - currentPos2 + i].Name;
                }


                //�`��e�L�X�g���X�V
                // ChooseText(currentItemSelection, currentPos2);

                return;
            }

            // X�{�^���i�L�����Z���L�[�j�������ꂽ��
            else if (Input.GetKeyDown(KeyCode.X))
            {
                // �_�C�A���O�{�b�N�X�����
                menuDialogBox2.SetActive(false);
                menuDialogBox3.SetActive(false);

                // menuState��1�ɂ���
                menuState = MenuState.Menu1;

                // �J�[�\���ړ���Ԃɂ���
                isCursorMoving = true;

                // Update�𔲂���
                return;
            }

            // �����̓��͂������
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // ���̓��͂���莞�ԓ���Ȃ��悤�ɂ���
                isCursorMoving = true;

                // �񕜃A�C�e���̏�������6�ȉ��̂Ƃ�
                if (healingItems.Count <= 6)
                {
                    // currentItemSelection���ő�l�ł͂Ȃ��Ƃ�
                    if (currentItemSelection != healingItems.Count - 1)
                    {
                        // currentItemSelection��1�𑫂�
                        currentItemSelection++;
                    }

                    // currentItemSelection���ő�l�̂Ƃ�
                    else
                    {
                        // currentItemSelection��0�ɖ߂�
                        currentItemSelection = 0;
                    }

                    // �J�[�\���|�W�V������currentItemSelection�Ɠ����ɐݒ�
                    currentPos2 = currentItemSelection;
                }

                // �񕜃A�C�e���̏�������6�ȏ�̂Ƃ�
                else
                {
                    // currentItemSelection���ő�l�ł͂Ȃ��Ƃ�
                    if (currentItemSelection != healingItems.Count - 1)
                    {
                        currentItemSelection++;

                        // cursorPos2��5�ł͂Ȃ��Ƃ�
                        if (currentPos2 != 5)
                        {
                            // currentPos2��1�𑫂�
                            currentPos2++;
                        }
                        // cursorPos2��5�̂Ƃ�
                        else
                        {
                            // currentPos2�����̂܂�5�ɐݒ肷��
                            currentPos2 = 5;
                        }
                    }
                    // currentItemSelection���ő�l�̂Ƃ�
                    else
                    {
                        // currentItemSelection��0�ɂ��ǂ�
                        currentItemSelection = 0;
                        // currentPos��0�ɖ߂�
                        currentPos2 = 0;
                    }
                }
                //Menu2���X�V����
                Menu2Update();

                // Update�𔲂���
                return;
            }

            // ����̓��͂������        
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // ���̓��͂���莞�ԓ���Ȃ��悤�ɂ���
                isCursorMoving = true;

                // �񕜃A�C�e���̏�������6�ȉ��̂Ƃ�
                if (healingItems.Count <= 6)
                {
                    // currentItemSelection��0�ł͂Ȃ��Ƃ�
                    if (currentItemSelection != 0)
                    {
                        // currentItemSelection����1������
                        currentItemSelection--;
                    }

                    // currentItemSelection��0�̂Ƃ�
                    else
                    {
                        // currentItemSelection���ő�l�ɂ���
                        currentItemSelection = healingItems.Count-1;
                    }
                    // �J�[�\���|�W�V������currentItemSelection�Ɠ����ɐݒ�
                    currentPos2 = currentItemSelection;
                }

                // �񕜃A�C�e���̏�������6�ȏ�̂Ƃ�
                else
                {
                    // currentItemSelection��0�ł͂Ȃ��Ƃ�
                    if (currentItemSelection != 0)
                    {
                        // currentItemSelection����1������
                        currentItemSelection--;

                        // cursorPos2��0�ł͂Ȃ��Ƃ�
                        if (currentPos2 != 0)
                        {
                            // currentPos2����1������
                            currentPos2--;
                        }
                        // cursorPos2��0�̂Ƃ�
                        else
                        {
                            // currentPos2��0�̂܂܂ɂ���
                            currentPos2 = 0;
                        }
                    }
                    // currentItemSelection��0�̂Ƃ�
                    else
                    {
                        // currentItemSelection���ő�l�ɐݒ�
                        currentItemSelection = healingItems.Count - 1;

                        // currentPos2�͈�ԉ��i5�j�ɐݒ�
                        currentPos2 = 5;
                    }
                }
                //Menu2���X�V����
                Menu2Update();
            }       
        }
    }

    // Menu2�̕`����X�V����
    void Menu2Update()
    {
        //�`��e�L�X�g���X�V
        ChooseText(currentItemSelection, currentPos2);

        // �n���h�J�[�\���̈ʒu���X�V
        CursorUpdate2(currentPos2);

        // descriptionText���X�V
        descriptionText.GetComponent<TextMeshProUGUI>().text = healingItems[currentItemSelection].Description;
    }

    // �`��e�L�X�g���N���A����
    void ClearText()
    {
        foreach (GameObject itemContentText in itemContentTexts)
        {
            itemContentText.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    // ���݂̃J�[�\���ʒu�ƃA�C�e���I��ԍ�����_�C�A���O�ɕ\������e�L�X�g��I������
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
        // currentPos���[���̎���
        if (currentPos1 == 0)
        {
            // �n���h�J�[�\����itemText�̍��ɂ���
            cursor1.transform.localPosition = new Vector3(itemText.transform.localPosition.x - 130, itemText.transform.localPosition.y, -10);
        }

        else if (currentPos1 == 1)
        {
            // �n���h�J�[�\����equipmentText�̉��ɂ���
            cursor1.transform.localPosition = new Vector3(equipmentText.transform.localPosition.x - 130, equipmentText.transform.localPosition.y, -10);
        }
    }

    void CursorUpdate2(int currentPos2)
    {
        // currentPos���[���̎���
        if (currentPos2 == 0)
        {
            // �n���h�J�[�\����itemContentText1�̍��ɂ���
            cursor2.transform.localPosition = new Vector3(itemContentText1.transform.localPosition.x - 130, itemContentText1.transform.localPosition.y, -10);
        }

        else if (currentPos2 == 1)
        {
            // �n���h�J�[�\����itemContentText2�̉��ɂ���
            cursor2.transform.localPosition = new Vector3(itemContentText2.transform.localPosition.x - 130, itemContentText2.transform.localPosition.y, -10);
        }

        else if (currentPos2 == 2)
        {
            // �n���h�J�[�\����itemContentText3�̉��ɂ���
            cursor2.transform.localPosition = new Vector3(itemContentText3.transform.localPosition.x - 130, itemContentText3.transform.localPosition.y, -10);
        }

        else if (currentPos2 == 3)
        {
            // �n���h�J�[�\����itemContentText4�̉��ɂ���
            cursor2.transform.localPosition = new Vector3(itemContentText4.transform.localPosition.x - 130, itemContentText4.transform.localPosition.y, -10);
        }

        else if (currentPos2 == 4)
        {
            // �n���h�J�[�\����itemContentText5�̉��ɂ���
            cursor2.transform.localPosition = new Vector3(itemContentText5.transform.localPosition.x - 130, itemContentText5.transform.localPosition.y, -10);
        }

        else if (currentPos2 == 5)
        {
            // �n���h�J�[�\����itemContentText6�̉��ɂ���
            cursor2.transform.localPosition = new Vector3(itemContentText6.transform.localPosition.x - 130, itemContentText6.transform.localPosition.y, -10);
        }
    }

}
