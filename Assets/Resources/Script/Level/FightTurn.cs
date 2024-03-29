using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FightTurn", menuName = "LevelSO/TurnInfo/FightTurn")]
public class FightTurn : TurnData
{
    [Header("可修改")]
    public Transform enemySpwanPoints;// 记录战斗场景的所有刷怪点的父节点
    public int totalEnemyNum;// 敌人总数
    public float createDuration;// 刷怪间隔时间
    public float startDuration;// 开始刷怪的时间
    public float endDuration;// 结束后持续时间
    public int OnceCreateNum;// 一次生成多少敌人
    [Header("别修改")]
    public int curEnemyNum;
    public int spareEnemyNum;
    public float Timer;// 计时器
    public bool isStart;// 判断是否开始刷怪
    public override GameObject OnCreate()
    {
        GameObject obj = base.OnCreate();
        if (enemySpwanPoints == null)
        {
            enemySpwanPoints = TurnScene.transform.Find("EnemySpwanPoints");
        }

        spareEnemyNum = totalEnemyNum;
        curEnemyNum = 0;
        Timer = 0;
        isStart = false;    

        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.Fight;
        GameManager.Instance.Player.transform.Find("AttackArea").gameObject.SetActive(true);
        // 播放相应bgm

        return obj;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        // 当全部敌人被清完时调用OnDestory函数
        
        if (!isOver)
        {
            if (curEnemyNum == 0 && spareEnemyNum == 0)
            {
                isOver = true;
                Time.timeScale = 0.5f;
                Timer = 0;
            }
            if (!isStart)
            {
                if (Timer >= startDuration)
                {
                    isStart = true;
                    Timer = 0;
                }
            }
            else
            {
                curEnemyNum = EnemyManager.Instance.enemyList.Count;
                if (Timer >= createDuration)
                {
                    // 调用生成怪物的函数
                    CreateEnemy();
                    Timer = 0;
                }
            }
            Timer += Time.deltaTime;
        }
        else
        {
            if (Timer >= endDuration)
            {
                OnDestory();
            }
            Timer += Time.unscaledDeltaTime;
        }
        
        
    }


    public override void OnDestory()
    {
        Time.timeScale = 1;
        base.OnDestory();
    }

    public void CreateEnemy()
    {
        // 先判断是否是最后一次生成敌人
        if (spareEnemyNum <= OnceCreateNum)
        {
            int index;
            // 生成敌人
            for (int i = 0; i < spareEnemyNum - 1; i++)
            {
                Transform point = RandomCreatePoint();
                index = UnityEngine.Random.Range(0, 3);
                switch (index)
                {
                    case 0:
                        
                        EnemyManager.Instance.CreateEnemy01(point.position);
                        Debug.Log("生成1级敌人");
                        break;
                    case 1:
                        
                        EnemyManager.Instance.CreateEnemy02(point.position);
                        Debug.Log("生成2级敌人");
                        break;
                    case 2:
                        
                        EnemyManager.Instance.CreateEnemy03(point.position);
                        Debug.Log("生成3级敌人");
                        break;
                }

            }
            //curEnemyNum += spareEnemyNum;
            spareEnemyNum = 0;
        }
        else
        {
            int index;
            for (int i = 0; i < OnceCreateNum; i++)
            {
                // 生成敌人
                Transform point = RandomCreatePoint();

                index = UnityEngine.Random.Range(0, 3);
                switch (index)
                {
                    case 0:

                        EnemyManager.Instance.CreateEnemy01(point.position);
                        Debug.Log("生成1级敌人");
                        break;
                    case 1:

                        EnemyManager.Instance.CreateEnemy02(point.position);
                        Debug.Log("生成2级敌人");
                        break;
                    case 2:

                        EnemyManager.Instance.CreateEnemy03(point.position);
                        Debug.Log("生成3级敌人");
                        break;
                }
            }
            //curEnemyNum += OnceCreateNum;
            spareEnemyNum -= OnceCreateNum;
        }
    }

    public Transform RandomCreatePoint()
    {
        int count = 0;// 计数
        int index;
        Transform point;
        Vector2 halfView = new Vector2(GameManager.Instance.viewWidth/2, GameManager.Instance.viewHeight/2);
        do
        {
            count++;
            // 随机刷怪点
            index = UnityEngine.Random.Range(0, enemySpwanPoints.transform.childCount);
            point = enemySpwanPoints.transform.GetChild(index);

            // 判断该点是否在玩家视野内
            if (Mathf.Abs(point.position.x - Camera.main.transform.position.x) <= (halfView.x + 1) && Mathf.Abs(point.position.y - Camera.main.transform.position.y) <= (halfView.y + 1f))
            {
                Debug.Log("无效刷怪点【" + point.name + ":" + point.position + "】");
                Debug.Log("X的差距:" + Mathf.Abs(point.position.x - Camera.main.transform.position.x));
                Debug.Log("Y的差距:" + Mathf.Abs(point.position.y - Camera.main.transform.position.y));
                if (count >= 10)
                {
                    Debug.Log("测试失败");
                    break;
                }
                continue;
            }
            else
            {
                Debug.Log("随机刷怪点测试成功:"+point.position);
                break;
            }
        } while (true);


        return point;
    }
}
