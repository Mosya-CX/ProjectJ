using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 无双 2s持续时间,将全场所有敌人的对应按键改为 all。即无论 1/2/3 级的怪物均可以用 26 键任意键位一击必杀
public class Item01 : BaseItem
{
    public float curTime = 0;
    public float totalTime = 2f;
    public int changeEnemyCount = 0;
    public override void OnCreate()
    {
        base.OnCreate();
        // 初始化
        if (curTime > 0 )
        {
            changeEnemyCount = 0;// 如果不是道具效果叠加就重置该值
        }
        curTime = 0;// 重置时间
        // 玩家外观方面的调整

        // 其它调整(如摄像机滤镜等等)

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        // 全场所有敌人的字母全部变成高亮状态且一击必杀，并做其它敌人相关调整
        if (changeEnemyCount != EnemyManager.Instance.enemyList.Count)
        {
            Effect();
        }
        // 判断持续时间
        if (curTime >= totalTime)
        {
            isUsed = true;
        }
        else
        {
            curTime += Time.deltaTime;
        }
    }

    public override void OnDestory()
    {
        base.OnDestory();
        // 将所有敌人都改回去

        // 并调整玩家外观等等

        // 其它调整

        // 重置成员变量
        changeEnemyCount = 0;
        curTime = 0;
    }

    public override void Effect()
    {
        base.Effect();


    }

    

}
