using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[CreateAssetMenu(fileName = "FightTurn", menuName = "LevelSO/TurnInfo/FightTurn")]
public class FightTurn : TurnData
{
    [Header("���޸�")]
    public Transform enemySpwanPoints;// ��¼ս������������ˢ�ֵ�ĸ��ڵ�
    public int totalEnemyNum;// ��������
    public float createDuration;// ˢ�ּ��ʱ��
    public float startDuration;// ��ʼˢ�ֵ�ʱ��
    public float endDuration;// ���������ʱ��
    public int OnceCreateNum;// һ�����ɶ��ٵ���
    public float createEnemy01Probability;// ����һ�����˵ĸ���
    public float createEnemy02Probability;// ���ɶ������˵ĸ���
    public float offset;
    [Header("���޸�")]
    public int curEnemyNum;
    public int spareEnemyNum;
    public float Timer;// ��ʱ��
    public bool isStart;// �ж��Ƿ�ʼˢ��
    public override GameObject OnCreate()
    {
        GameObject obj = base.OnCreate();
        if (enemySpwanPoints == null)
        {
            enemySpwanPoints = TurnScene.transform.Find("EnemySpwanPoints");
        }

        if (UIManager.Instance == null)
        {
            Debug.LogWarning("UIManagerΪ��");
        }
        UIManager.Instance.OpenPanel(UIConst.FightUI);// ��ս��UI

        spareEnemyNum = totalEnemyNum;
        curEnemyNum = 0;
        Timer = 0;
        isStart = false;

        createEnemy01Probability = 1f;
        createEnemy02Probability = 0f;
        offset = 2.5f;

        //RefreshAStarGraph(obj);// ����������Ϣ
        CameraControl.Instance.Player = GameManager.Instance.Player;
        CameraControl.Instance.GetCursceneAndEnable(obj);// ������������� 

        playerData.playerState = PlayerState.Fight;
        playerData.transform.Find("AttackArea").gameObject.SetActive(true);// ����������Χ���
        // ������Ӧbgm

        return obj;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        // ��ȫ�����˱�����ʱ����OnDestory����

        
        if (!isOver)
        {
            //Debug.Log("�����ж��Ƿ����");
            if (curEnemyNum == 0 && spareEnemyNum == 0)
            {
                isOver = true;
                Time.timeScale = 0.5f;
                Timer = 0;
            }
            //Debug.Log("�����ж��Ƿ�ʼ");
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
                //Debug.Log("�������ɵ���");
                curEnemyNum = EnemyManager.Instance.enemyList.Count;
                if (Timer >= createDuration)
                {
                    Debug.Log("���ɵ���");
                    // �������ɹ���ĺ���
                    CreateEnemy();
                    Timer = 0;
                }
            }
            //Debug.Log("�ۼӼ�ʱ��");
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

        CameraControl.Instance.DeleteCursceneAndStop();//  �������������
        ComboManager.Instance.ReSetComboNum();// ����combo��
        UIManager.Instance.ClosePanel(UIConst.FightUI);// �ر�ս��UI
        playerData.transform.Find("AttackArea").gameObject.SetActive(false);// �رչ�����Χ���

        base.OnDestory();
    }

    public void CreateEnemy()
    {
        // ���ж��Ƿ������һ�����ɵ���
        if (spareEnemyNum <= OnceCreateNum)
        {
            int index;
            // ���ɵ���
            for (int i = 0; i < spareEnemyNum - 1; i++)
            {
                Transform point = RandomCreatePoint();
                index = UnityEngine.Random.Range(0, 2);
                switch (index)
                {
                    case 0:
                        
                        EnemyManager.Instance.CreateEnemy01(point.position);
                        Debug.Log("����1������");
                        break;
                    case 1:
                        
                        EnemyManager.Instance.CreateEnemy02(point.position);
                        Debug.Log("����2������");
                        break;
                    case 2:
                        
                        EnemyManager.Instance.CreateEnemy03(point.position);
                        Debug.Log("����3������");
                        break;
                }

            }
            //curEnemyNum += spareEnemyNum;
            spareEnemyNum = 0;
        }
        else
        {
            for (int i = 0; i < OnceCreateNum; i++)
            {
                // ���ɵ���
                Transform point = RandomCreatePoint();

                EnemyManager.Instance.RandomlyGenerateEnemy(point.position, createEnemy01Probability, createEnemy02Probability);
            }
            //curEnemyNum += OnceCreateNum;
            spareEnemyNum -= OnceCreateNum;
        }
    }

    public Transform RandomCreatePoint()
    {
        int count = 0;// ����
        int index;
        Transform point;
        Vector2 halfView = new Vector2(GameManager.Instance.viewWidth/2, GameManager.Instance.viewHeight/2);
        do
        {
            count++;
            // ���ˢ�ֵ�
            index = UnityEngine.Random.Range(0, enemySpwanPoints.transform.childCount);
            point = enemySpwanPoints.transform.GetChild(index);

            // �жϸõ��Ƿ��������Ұ��
            if (Mathf.Abs(point.position.x - Camera.main.transform.position.x) <= (halfView.x + offset) || Mathf.Abs(point.position.y - Camera.main.transform.position.y) <= (halfView.y + offset))
            {
                Debug.Log("��Чˢ�ֵ㡾" + point.name + ":" + point.position + "��");
                Debug.Log("X�Ĳ��:" + Mathf.Abs(point.position.x - Camera.main.transform.position.x));
                Debug.Log("Y�Ĳ��:" + Mathf.Abs(point.position.y - Camera.main.transform.position.y));
                if (count >= 10)
                {
                    Debug.Log("����ʧ��");
                    break;
                }
                continue;
            }
            else
            {
                Debug.Log("���ˢ�ֵ���Գɹ�:"+point.position);
                break;
            }
        } while (true);


        return point;
    }

    public void RefreshAStarGraph(GameObject scene)
    {
        Sprite mapSprite = scene.GetComponent<SpriteRenderer>().sprite;
        int width = (int)mapSprite.bounds.size.x;
        int depth = (int)mapSprite.bounds.size.y;
        var gridGraph = AstarPath.active.data.gridGraph;// �õ���һ��Grid����
        gridGraph.width = width;
        gridGraph.depth = depth;
        AstarPath.active.Scan(gridGraph);
    }
}
