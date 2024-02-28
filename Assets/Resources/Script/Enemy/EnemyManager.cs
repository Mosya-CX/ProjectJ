using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public Pool enemy01Pool;
    public Pool enemy02Pool;
    public Pool enemy03Pool;
    public Pool[] enemyPools;
    // 存储场景内敌人单位
    public List<Enemy> enemyList;
    public override void Awake()
    {
        base.Awake();
        Init(enemyPools);
        enemyList = new List<Enemy>();
    }
    public void OnDisable()
    {
        foreach (var pool in enemyPools)
        {
            pool.OnDisable();
        }
    }
    public void Init(Pool[] pools)
    {
        foreach (var pool in pools)
        {
            pool.SetParent(this.transform);
            pool.Prewarm();
            
        }
    }

    // 生成敌人
    public void CreateEnemy()
    {
        // 赋予敌人种类

        // 生成到场景当中

    }

}
