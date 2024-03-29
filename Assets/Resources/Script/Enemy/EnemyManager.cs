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
    public  List<Enemy> enemyList;
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
    public Enemy CreateEnemy01(Vector3 pos)
    {
        Enemy enemy = enemy01Pool.Request().GetComponent<Enemy>();
        enemy.GetComponent<EnemyController>().target = GameManager.Instance.Player.transform;
        if(!enemyList.Contains(enemy))
        {
            enemyList.Add(enemy);
        }
        else
        {
            Debug.Log("List has contain");
        }
        enemy.transform.position=pos;
        return enemy;
    }
    public Enemy CreateEnemy02(Vector3 pos)
    {
        Enemy enemy = enemy01Pool.Request().GetComponent<Enemy>();
        enemy.GetComponent<EnemyController>().target = GameManager.Instance.Player.transform;
        if (!enemyList.Contains(enemy))
        {
            enemyList.Add(enemy);
        }
        else
        {
            Debug.Log("List has contain");
        }
        enemy.transform.position = pos;
        return enemy;
    }
    public Enemy CreateEnemy03(Vector3 pos)
    {
        Enemy enemy = enemy01Pool.Request().GetComponent<Enemy>();
        enemy.GetComponent<EnemyController>().target = GameManager.Instance.Player.transform;
        if (!enemyList.Contains(enemy))
        {
            enemyList.Add(enemy);
        }
        else
        {
            Debug.Log("List has contain");
        }
        enemy.transform.position = pos;
        return enemy;
    }
    public void RemoveFromList(Enemy enemy)
    {
        enemyList.Remove(enemy);
    }
    public Enemy RandomlyGenerateEnemy(Vector3 pos,float probability01,float probability02)
    {
        if (Mathf.Abs(probability01 + probability02 - 1f) > Mathf.Epsilon)
        {
            Debug.LogError("The probabilities do not add up to 1!");
            return null;
        }

        // 随机数生成器
        Random.InitState((int)(Time.timeSinceLevelLoad * 1000f)); // 初始化随机种子以避免重复

        // 根据概率随机选择
        float randomValue = Random.Range(0f, 1f);

        Enemy enemyToSpawn = null;
        if (randomValue < probability01)
        {
            // 如果随机值小于probability01，则生成第一种类型的敌人
            enemyToSpawn = CreateEnemy01(pos);
        }
        else
        {
            // 否则，生成第二种类型的敌人（因为剩下的概率一定是probability02）
            enemyToSpawn = CreateEnemy02(pos);
        }

        return enemyToSpawn;
    }
    public  Enemy CreateEnemyBasedOnPrefab(GameObject prefab, Vector3 pos)
    {
        Pool targetPool = enemyPoolDictionary.TryGetValue(prefab, out var pool) ? pool : null;
        if (targetPool == null)
        {
            return null;
        }
        else
        {
            Enemy target = enemy01Pool.Request().GetComponent<Enemy>();
            if (!enemyList.Contains(target))
            {
                enemyList.Add(target);
            }
            else
            {
                Debug.Log("List has contain");
            }
            target.transform.position = pos;
            return target;
        }
    }
}
