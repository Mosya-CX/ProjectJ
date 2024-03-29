using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public Pool enemy01Pool;
    public Pool enemy02Pool;
    public Pool enemy03Pool;
    public Pool[] enemyPools;
    // �洢�����ڵ��˵�λ
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
                Debug.Log("�ֵ��ظ�" + pool.Factory.GetPrefab().name);
                continue;
            }
            enemyPoolDictionary.Add(pool.Factory.GetPrefab(), pool);
        }
    }

    // ���ɵ���
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

        // �����������
        Random.InitState((int)(Time.timeSinceLevelLoad * 1000f)); // ��ʼ����������Ա����ظ�

        // ���ݸ������ѡ��
        float randomValue = Random.Range(0f, 1f);

        Enemy enemyToSpawn = null;
        if (randomValue < probability01)
        {
            // ������ֵС��probability01�������ɵ�һ�����͵ĵ���
            enemyToSpawn = CreateEnemy01(pos);
        }
        else
        {
            // �������ɵڶ������͵ĵ��ˣ���Ϊʣ�µĸ���һ����probability02��
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
