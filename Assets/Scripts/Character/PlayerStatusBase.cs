using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �v���C���[�X�e�[�^�X�̃x�[�X
[CreateAssetMenu]
public class PlayerStatusBase : ScriptableObject
{
    // �G�l�~�[�̃X�e�[�^�X��ݒ肷��
    [SerializeField] new string name; // ���O

    [TextArea]
    [SerializeField] string description;

    [SerializeField] int maxHp; // �ő�HP
    [SerializeField] int hp; // ���݂�HP
    [SerializeField] int atk; // �U����
    [SerializeField] int def; // �h���

    public int MaxHp { get => maxHp; set => maxHp = value; }
    public int Hp { get => hp; set => hp = value; }
    public int Atk { get => atk; set => atk = value; }
    public int Def { get => def; set => def = value; }
}
