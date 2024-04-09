using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Բ�λ���
//���;�������2-2
//������������ 3s ���������� 7 ����ȷ��������sp 1��
//����Ч����ʱͣ�������߳����ڹ�����Χ������ͻ�״ԲȦ����ʱ����һȦ��

[CreateAssetMenu(fileName = "Skill06", menuName = "SkillSO/Skill06")]
public class Skill06 : BaseSkill
{
    public float curTime = 0;// ��ʱ��
    public float setPeriod = 3;
    public int markCombo = 0;
    public int standardCombo = 7;

    public override bool OnTrigger()
    {
        if (!base.OnTrigger())
        {
            markCombo = (int)ComboManager.Instance.comboNum;
            return false;
        }
        if (curTime >= setPeriod)// �ж��Ƿ��ڹ涨ʱ����
        {
            curTime = 0;
            markCombo = (int)ComboManager.Instance.comboNum;
            return false;
        }
        else
        {
            int curCombo = (int)ComboManager.Instance.comboNum;
            if (curCombo - markCombo < 0)// �ж��Ƿ��������
            {
                markCombo = curCombo;
                curTime = 0;
                return false;
            }
            if (curCombo - markCombo >= standardCombo)// �ж��Ƿ�ﵽ��׼
            {
                markCombo = curCombo;
                curTime = 0;
                return true;
            }
            curTime += Time.deltaTime;
            return false;
        }
    }
    public override void OnCreate()
    {
        base.OnCreate();
        Effect();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        // �ж϶����Ƿ񲥷Ž���

    }

    public override void OnDestory()
    {
        base.OnDestory();
        // ����ʱͣ

        // �л��ش�������


    }
    public override void Effect()
    {
        base.Effect();

        // ʱͣ

        // �����Ŷ���

        // ������ɱ����Χ�ڵ���

    }
}
