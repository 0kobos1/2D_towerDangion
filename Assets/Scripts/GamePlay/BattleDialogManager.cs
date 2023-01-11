using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// �o�g�����̃_�C�A���O�̕ύX�𐧌䂷��
public class BattleDialogManager : MonoBehaviour
{
    [SerializeField] GameObject battleDialogBox;
    [SerializeField] GameObject battleHPBox;
    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] TextMeshProUGUI enemyNameText;
    [SerializeField] TextMeshProUGUI playerHpText;
    [SerializeField] TextMeshProUGUI enemyHpText;
    [SerializeField] GameObject playerHpBar;
    [SerializeField] GameObject enemyHpBar;

    GameObject playerBattleObject;
    GameObject enemyBattleObject;

    BattleStatus playerBattleStatus;
    BattleStatus enemyBattleStatus;

    Vector2 playerHpBarScale;
    Vector2 enemyHpBarScale;

    // �_�C�A���O�̃Z�b�g�A�b�v(from BattleSystem)
    public void SetUp(GameObject gotPlayerBattleObject, GameObject gotEnemyBattleObject)
    {
        playerBattleObject = gotPlayerBattleObject;
        enemyBattleObject = gotEnemyBattleObject;

        playerBattleStatus = playerBattleObject.GetComponent<BattleStatus>();
        enemyBattleStatus = enemyBattleObject.GetComponent<BattleStatus>();

        playerHpBarScale = playerHpBar.GetComponent<RectTransform>().localScale;
        enemyHpBarScale = enemyHpBar.GetComponent<RectTransform>().localScale;

        // �v���C���[NameText���f
        playerNameText.text = playerBattleStatus.Status.StatusBase.Name;

        // �G�l�~�[NameText���f
        enemyNameText.text = enemyBattleStatus.Status.StatusBase.Name;

        // �v���C���[HpText���f
        playerHpText.text = $"HP {playerBattleStatus.Hp}/{playerBattleStatus.Status.MaxHp}";

        // �G�l�~�[HpText���f
        enemyHpText.text = $"HP {enemyBattleStatus.Hp}/{enemyBattleStatus.Status.MaxHp}";

        // �v���C���[HpBar���f
        playerHpBarScale.x = (float)playerBattleStatus.Status.Hp / playerBattleStatus.Status.MaxHp;
        playerHpBar.GetComponent<RectTransform>().localScale = playerHpBarScale;

        // �G�l�~�[HpBar���f
        enemyHpBarScale.x = (float)enemyBattleStatus.Status.Hp / enemyBattleStatus.Status.MaxHp;
        enemyHpBar.GetComponent<RectTransform>().localScale = enemyHpBarScale;

        // �G�Ɩ�����Hp�o�[���_�C�A���O�{�b�N�X�ɕ\�������
        battleDialogBox.SetActive(true);
        battleHPBox.SetActive(true);
    }

    // �\���̍X�V
    public void HandleUpdate(BattleStatus gotPlayerBattleStatus, BattleStatus gotEnemyBattleStatus)
    {
        // �v���C���[HpText���f
        playerHpText.text = $"HP {playerBattleStatus.Hp}/{playerBattleStatus.Status.MaxHp}";

        // �v���C���[HpBar���f
        playerHpBarScale.x = (float) playerBattleStatus.Hp / playerBattleStatus.Status.MaxHp;
        playerHpBar.GetComponent<RectTransform>().localScale = playerHpBarScale;

        // �G�l�~�[HpText���f
        enemyHpText.text = $"HP {enemyBattleStatus.Hp}/{enemyBattleStatus.Status.MaxHp}";

        // �G�l�~�[HpBar���f
        enemyHpBarScale.x = (float) enemyBattleStatus.Hp / enemyBattleStatus.Status.MaxHp;
        enemyHpBar.GetComponent<RectTransform>().localScale = enemyHpBarScale;
    }

    // �\���I��
    public void Close()
    {
        battleDialogBox.SetActive(false);
        battleHPBox.SetActive(false);
    }
}
