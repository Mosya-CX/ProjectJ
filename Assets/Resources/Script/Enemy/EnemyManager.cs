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
    public static Dictionary<GameObject,Pool> enemyPoolDictionary;
    public bool HasEnemy =>enemyList.Count > 0;
    public override void Awake()
    {
        base.Awake();
        enemyPoolDictionary = new Dictionary<GameObject, Pool>();
        enemyList = new List<Enemy>();
        Init(enemyPools);
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
            if (enemyPoolDictionary.ContainsKey(pool.Factory.GetPrefab()))
            {
                Debug.Log("字典重复" + pool.Factory.GetPrefab().name);
                continue;
            }
            enemyPoolDictionary.Add(pool.Factory.GetPrefab(), pool);
        }
    }

    // 生成敌人
    public GameObject CreateEnemy01(Vector3 pos)
    {
        GameObject enemy = enemy01Pool.Request();
        enemy.transform.position=pos;
        return enemy;
    }
    public GameObject CreateEnemy02(Vector3 pos)
    {
        GameObject enemy = enemy02Pool.Request();
        enemy.transform.position = pos;
        return enemy;
    }
    public GameObject CreateEnemy03(Vector3 pos)
    {
        GameObject enemy = enemy03Pool.Request();
        enemy.transform.position = pos;
        return enemy;
    }
    public static GameObject CreateEnemyBasedOnPrefab(GameObject prefab, Vector3 pos)
    {
        Pool targetPool = enemyPoolDictionary.TryGetValue(prefab, out var pool) ? pool : null;
        if (targetPool == null)
        {
            return null;
        }
        else
        {
            GameObject target = targetPool.Request();
            target.transform.position = pos;
            return target;
        }
    }
}
