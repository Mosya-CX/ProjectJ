using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossTurn", menuName = "LevelSO/TurnInfo/BossTurn")]
public class BossTurn : TurnData
{
    public GameObject BossPrefab;
    public Queue<EnemyOrder> enemyOrder;// ���ֵ�˳��
    public int curOrder = 0;
    public override void OnCreate()
    {
        base.OnCreate();
        curOrder = 0;
        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.Fight;
        // ������Ӧbgm

        // ����boss

    }


}

[Serializable]
public class EnemyOrder
{
    public int enemy1Num;
    public int enemy2Num;
    public int enemy3Num;
}