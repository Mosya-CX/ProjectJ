using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ʯ��
//Ϊ��ɫ���Ӷ��� 3 ������ʧ��ܣ�ֻҪ�������ľͻ�һֱ���ڣ�����ֻ�ɵ���ʧ���ֹ
//���������ɵ����˺���

public class Item03 : BaseItem
{
    public int curShield = 3;
    public Transform ShieldArea;

    public override void OnCreate()
    {
        base.OnCreate();
        // ��ʼ������
        curShield = 3;
        // ͨ��UIManager�󶨻�������

        // ��������

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        // ͨ���ж�ShieldArea��������������curShield������UI
        if (ShieldArea.childCount != curShield)
        {

        }
    }

    public override void OnDestory()
    {
        base.OnDestory();
        // ���ShieldArea��������

        
    }

    public override void Effect()
    {
        base.Effect();
        curShield--;
        if (curShield <= 0 )
        {
            isUsed = true;
        }
    }
}
