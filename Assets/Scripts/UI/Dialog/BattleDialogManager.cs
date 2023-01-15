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

    GameObject playerObject;
    GameObject enemyObject;
    GameObject playerHpBar; // �v���C���[���HpBar
    GameObject enemyHpBar; // �G�l�~�[���HpBar

    PlayerStatus playerStatus;
    EnemyStatus enemyStatus;

    Vector2 playerHpBarScale;
    Vector2 enemyHpBarScale;

    // �_�C�A���O�̃Z�b�g�A�b�v(from BattleSystem)
    public void SetUp(GameObject touchedPlayerObject, GameObject touchedEnemyObject)
    {
        // �ڐG�����v���[���[�A�G�̃I�u�W�F�N�g���擾
        playerObject = touchedPlayerObject;
        enemyObject = touchedEnemyObject;

        // �I�u�W�F�N�g�ɂ��Ă���X�e�[�^�X���擾
        playerStatus = playerObject.GetComponent<PlayerStatus>();
        enemyStatus = enemyObject.GetComponent<EnemyStatus>();

        // �I�u�W�F�N�g�̎q�ł���HpBar�I�u�W�F�N�g���擾
        playerHpBar = playerObject.transform.Find("HpBarCanvas/PlayerHpBar").gameObject;
        enemyHpBar = enemyObject.transform.Find("HpBarCanvas/EnemyHpBar").gameObject;

        // HpBar�̃X�P�[�����擾
        playerHpBarScale = playerHpBar.GetComponent<RectTransform>().localScale;
        enemyHpBarScale = enemyHpBar.GetComponent<RectTransform>().localScale;

        // �v���C���[NameText���f
        //playerNameText.text = playerStatus.PlayerStatusBase.Name;

        // �G�l�~�[NameText���f
        //enemyNameText.text = enemyStatus.EnemyStatusBase.Name;

        // �v���C���[HpText���f
        statusHpText.text = $"HP {playerStatus.Hp}/{playerStatus.MaxHp}";

        // �v���C���[HpBar���f
        playerHpBarScale.x = (float)playerStatus.Hp / playerStatus.MaxHp;
        playerHpBar.GetComponent<RectTransform>().localScale = playerHpBarScale;

        // �G�l�~�[HpBar���f
        enemyHpBarScale.x = (float)enemyStatus.Hp / enemyStatus.MaxHp;
        enemyHpBar.GetComponent<RectTransform>().localScale = enemyHpBarScale;

        // �G�Ɩ�����Hp�o�[���_�C�A���O�{�b�N�X�ɕ\�������
        playerHpBar.SetActive(true);
        enemyHpBar.SetActive(true);
    }

    // �\���̍X�V
    public void HandleUpdate()
    {
        // �v���C���[HpText���f
        statusHpText.text = $"HP {playerStatus.Hp}/{playerStatus.MaxHp}";

        // �v���C���[HpBar���f
        playerHpBarScale.x = (float) playerStatus.Hp / playerStatus.MaxHp;
        playerHpBar.GetComponent<RectTransform>().localScale = playerHpBarScale;

        // �G�l�~�[HpBar���f
        enemyHpBarScale.x = (float) enemyStatus.Hp / enemyStatus.MaxHp;
        enemyHpBar.GetComponent<RectTransform>().localScale = enemyHpBarScale;
    }

    // �\���I��
    public void Close()
    {
        playerHpBar.SetActive(false);
        enemyHpBar.SetActive(false);
    }
}
