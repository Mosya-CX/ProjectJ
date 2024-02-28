using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    // 存储场景内敌人单位
    public List<Enemy> enemyList;
    private void Awake()
    {
        Instance = this;
        enemyList = new List<Enemy>();
    }


    // 生成敌人
    public void CreateEnemy()
    {
        // 赋予敌人种类

        // 生成到场景当中

    }

}
