using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// �o�g�����̃_�C�A���O�̕ύX�𐧌䂷��
public class BattleDialogManager : MonoBehaviour
{
    [SerializeField] GameObject statusPlayerHpBar;
    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] TextMeshProUGUI enemyNameText;

    [SerializeField] TextMeshProUGUI statusHpText;

    GameObject playerBattleObject;
    GameObject enemyBattleObject;
    GameObject playerHpBar; // �v���C���[���HpBar
    GameObject enemyHpBar; // �G�l�~�[���HpBar

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

        playerHpBar = playerBattleObject.transform.Find("HpBarCanvas/PlayerHpBar").gameObject;
        enemyHpBar = enemyBattleObject.transform.Find("HpBarCanvas/EnemyHpBar").gameObject;

        playerHpBarScale = playerHpBar.GetComponent<RectTransform>().localScale;
        enemyHpBarScale = enemyHpBar.GetComponent<RectTransform>().localScale;

        // �v���C���[NameText���f
        playerNameText.text = playerBattleStatus.Status.StatusBase.Name;

        // �G�l�~�[NameText���f
        enemyNameText.text = enemyBattleStatus.Status.StatusBase.Name;

        // �v���C���[HpText���f
        statusHpText.text = $"HP {playerBattleStatus.Hp}/{playerBattleStatus.Status.MaxHp}";

        // �v���C���[HpBar���f
        playerHpBarScale.x = (float)playerBattleStatus.Status.Hp / playerBattleStatus.Status.MaxHp;
        playerHpBar.GetComponent<RectTransform>().localScale = playerHpBarScale;

        // �G�l�~�[HpBar���f
        enemyHpBarScale.x = (float)enemyBattleStatus.Status.Hp / enemyBattleStatus.Status.MaxHp;
        enemyHpBar.GetComponent<RectTransform>().localScale = enemyHpBarScale;

        // �G�Ɩ�����Hp�o�[���_�C�A���O�{�b�N�X�ɕ\�������
        playerHpBar.SetActive(true);
        enemyHpBar.SetActive(true);
    }

    // �\���̍X�V
    public void HandleUpdate(BattleStatus gotPlayerBattleStatus, BattleStatus gotEnemyBattleStatus)
    {
        // �v���C���[HpText���f
        statusHpText.text = $"HP {playerBattleStatus.Hp}/{playerBattleStatus.Status.MaxHp}";

        // �v���C���[HpBar���f
        playerHpBarScale.x = (float) playerBattleStatus.Hp / playerBattleStatus.Status.MaxHp;
        playerHpBar.GetComponent<RectTransform>().localScale = playerHpBarScale;

        // �G�l�~�[HpBar���f
        enemyHpBarScale.x = (float) enemyBattleStatus.Hp / enemyBattleStatus.Status.MaxHp;
        enemyHpBar.GetComponent<RectTransform>().localScale = enemyHpBarScale;
    }

    // �\���I��
    public void Close()
    {
        playerHpBar.SetActive(false);
        enemyHpBar.SetActive(false);
    }
}
