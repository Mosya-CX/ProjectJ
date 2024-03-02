using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��˫ 2s����ʱ��,��ȫ�����е��˵Ķ�Ӧ������Ϊ all�������� 1/2/3 ���Ĺ���������� 26 �������λһ����ɱ
public class Item01 : BaseItem
{
    public float curTime = 0;
    public float totalTime = 2f;
    public int changeEnemyCount = 0;
    public override void OnCreate()
    {
        base.OnCreate();
        // ��ʼ��
        if (curTime > 0 )
        {
            changeEnemyCount = 0;// ������ǵ���Ч�����Ӿ����ø�ֵ
        }
        curTime = 0;// ����ʱ��
        // �����۷���ĵ���

        // ��������(��������˾��ȵ�)

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        // ȫ�����е��˵���ĸȫ����ɸ���״̬��һ����ɱ����������������ص���
        if (changeEnemyCount != EnemyManager.Instance.enemyList.Count)
        {
            Effect();
        }
        // �жϳ���ʱ��
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
        // �����е��˶��Ļ�ȥ

        // �����������۵ȵ�

        // ��������

        // ���ó�Ա����
        changeEnemyCount = 0;
        curTime = 0;
    }

    public override void Effect()
    {
        base.Effect();


    }

    

}
