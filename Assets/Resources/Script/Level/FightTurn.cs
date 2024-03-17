using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FightTurn", menuName = "LevelSO/TurnInfo/FightTurn")]
public class FightTurn : TurnData
{
    public GameObject enemySpwanPoints;// 记录战斗场景的所有刷怪点的父节点
    public int totalEnemyNum;// 敌人总数
    public float createDuration;// 刷怪间隔时间
    public int OnceCreateNum;// 一次生成多少敌人
    float Timer;
    int spareEnemyNum;
    public override void OnCreate()
    {
        base.OnCreate();

        spareEnemyNum = totalEnemyNum;

        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.Fight;
        // 播放相应bgm

    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        // 先判断是否是最后一次生成敌人
        if (spareEnemyNum <= OnceCreateNum )
        {
            // 生成敌人


        }

    }

    public override void OnDestory()
    {
        
        base.OnDestory();
    }
}
