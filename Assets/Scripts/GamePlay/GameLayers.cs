using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] LayerMask solidObjectLayer; // solidObject���C���[���C���X�y�N�^�[�Őݒ�
    [SerializeField] LayerMask interactbleLayer; // interactable���C���[���C���X�y�N�^�[�Őݒ�
    [SerializeField] LayerMask playerLayer;      // player���C���[���C���X�y�N�^�[�Őݒ�
    [SerializeField] LayerMask enemyLayer;       // enemy���C���[���C���X�y�N�^�[�Őݒ�


    public LayerMask SolidObjectLayer { get => solidObjectLayer; }
    public LayerMask InteractbleLayer { get => interactbleLayer; }
    public LayerMask PlayerLayer { get => playerLayer; }
    public LayerMask EnemyLayer { get => enemyLayer; }



    public static GameLayers Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }
}
