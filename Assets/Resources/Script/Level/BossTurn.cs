using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossTurn", menuName = "LevelSO/TurnInfo/BossTurn")]
public class BossTurn : TurnData
{
    public GameObject BossPrefab;
    public Queue<EnemyOrder> enemyOrder;// 出怪的顺序
    public int curOrder = 0;
    public override void OnCreate()
    {
        base.OnCreate();
        curOrder = 0;
        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.Fight;
        // 播放相应bgm

        // 加载boss

    }


}

[Serializable]
public class EnemyOrder
{
    public int enemy1Num;
    public int enemy2Num;
    public int enemy3Num;
}