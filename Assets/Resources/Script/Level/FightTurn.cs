using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        spareEnemyNum = totalEnemyNum;
        curEnemyNum = 0;
        Timer = 0;
        isStart = false;    

        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.Fight;
        GameManager.Instance.Player.transform.Find("AttackArea").gameObject.SetActive(true);
        // ������Ӧbgm

        return obj;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        // ��ȫ�����˱�����ʱ����OnDestory����
        
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
                if (Timer >= createDuration)
                {
                    // �������ɹ���ĺ���
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
        // ���ж��Ƿ������һ�����ɵ���
        if (spareEnemyNum <= OnceCreateNum)
        {
            // ���ɵ���
            for (int i = 0; i < spareEnemyNum - 1; i++)
            {
                Transform point = RandomCreatePoint();

            }
            curEnemyNum += spareEnemyNum;
            spareEnemyNum = 0;
        }
        else
        {
            for (int i = 0; i < OnceCreateNum; i++)
            {
                // ���ɵ���
                Transform point = RandomCreatePoint();

            }
            curEnemyNum += OnceCreateNum;
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
            index = Random.Range(0, enemySpwanPoints.transform.childCount);

            Debug.Log("��Ļһ�볤:" + halfView.x);
            Debug.Log("��Ļһ���:" + halfView.y);
            Debug.Log("�����λ��:" + Camera.main.transform.position);

            point = enemySpwanPoints.transform.GetChild(index);

            // �жϸõ��Ƿ��������Ұ��
            if (Mathf.Abs(point.position.x - Camera.main.transform.position.x) <= (halfView.x + 1) && Mathf.Abs(point.position.y - Camera.main.transform.position.y) <= (halfView.y + 0.5f))
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
}
