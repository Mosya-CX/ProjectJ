using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : PersistentSingleton<EnemyManager>
{
    public Pool enemy01Pool;
    public Pool enemy02Pool;
    public Pool enemy03Pool;
    public Pool[] enemyPools;
    // �洢�����ڵ��˵�λ
    public List<Enemy> enemyList;
    public override void Awake()
    {
        base.Awake();
        enemyList = new List<Enemy>();
    }
    public void Init(ComponentPoolSO[] pools)
    {
        foreach (var pool in pools)
        {
            pool.SetParent(this.transform);
            pool.Prewarm();
            
        }
    }

    // ���ɵ���
    public void CreateEnemy()
    {
        // �����������

        // ���ɵ���������

    }

}
