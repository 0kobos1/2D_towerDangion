using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// ステータスダイアログを管理する
public class StatusDialogManager : MonoBehaviour
{
    [SerializeField] PlayerStatus playerStatus;

    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] GameObject playerHpBar;
    [SerializeField] TextMeshProUGUI atkText;
    [SerializeField] TextMeshProUGUI defText;

    Vector2 hpBarScale = new Vector2(1f, 1f);

    int level;
    int exp;
    int money;
    int hp;
    int maxHp;
    int atk;
    int def;


    public static StatusDialogManager Instance;

    private void Awake()
    {
        // インスタンス化
        Instance = this;
    }

    private void Start()
    {
        DialogUpdate();
    }

    // StatusDialogの表示を更新する
    public void DialogUpdate()
    {
        level = playerStatus.Level;
        exp = playerStatus.Exp;
        money = playerStatus.Money;
        hp = playerStatus.Hp;
        maxHp= playerStatus.MaxHp;
        atk= playerStatus.Atk;
        def= playerStatus.Def;

        levelText.text = $"LEVEL\n{level}";
        expText.text = $"EXP\n{exp}";
        moneyText.text = $"Money\n{money}";
        hpText.text = $"HP   {hp}/{maxHp}";
        hpBarScale.x = (float) hp / maxHp;
        playerHpBar.transform.localScale = hpBarScale;
        atkText.text = $"ATK\n{atk}";
        defText.text = $"DEF\n{def}";
    }
}
