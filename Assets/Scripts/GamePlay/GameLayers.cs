using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] LayerMask solidObjectLayer; // solidObjectレイヤーをインスペクターで設定
    [SerializeField] LayerMask interactbleLayer; // interactableレイヤーをインスペクターで設定
    [SerializeField] LayerMask playerLayer;      // playerレイヤーをインスペクターで設定
    [SerializeField] LayerMask enemyLayer;       // enemyレイヤーをインスペクターで設定


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
