using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��˫
//���;�������1-2
//�����������û��ո�����sp 3��
//����Ч������ȫ�����е��˵Ķ�Ӧ������Ϊ all�������� 1/2/3 ���Ĺ���������� 26 ��	�е������λһ����ɱ
//����ʱ�䣺3s
//����նɱ�����0.2s
//��֮ǰ��Ϊ�޳ɱ�������߳���ʱ��϶̣���������Ҫ��������sp����˿����ӳ�һ��ʱ�䣩

[CreateAssetMenu(fileName = "Skill02", menuName = "SkillSO/Skill02")]
public class Skill02 : BaseSkill
{
    public float duration = 3f;// ����ʱ��
    [Header("���޸�")]
    public float curTime = 0f;// ��¼��ǰ����ʱ��

    public override bool OnTrigger()
    {
        if (!base.OnTrigger())
        {
            return false;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            return true;
        }
        return false;
    }

    public override void OnCreate()
    {
        base.OnCreate();
        OnReset();

        Effect();// ���ü���Ч��
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (curTime >= duration)
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
        // ������״̬�Ļ�ȥ

    }

    public override void OnReset()
    {
        base.OnReset();
        curTime = 0;
    }

    public override void Effect()
    {
        base.Effect();
        // ��ȫ�����е��˵Ķ�Ӧ������Ϊ all�������� 1/2/3 ���Ĺ���������� 26 ���е������λһ����ɱ
    }
}
