using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����Ϊ��
//���;�������boss1
//����������ÿ���100��combo������sp 0��
//����Ч���� ʱͣ��ȫ�����
[CreateAssetMenu(fileName = "Skill03", menuName = "SkillSO/Skill03")]
public class Skill03 : BaseSkill
{
    // �󶨶�Ӧ�Ķ���

    public override bool OnTrigger()
    {
        if (!base.OnTrigger())
        {
            return false;
        }
        int curCombo = (int)ComboManager.Instance.comboNum;
        if (curCombo > 0 && curCombo % 100 == 0)
        {
            return true;
        }
        return false;
    }

    public override void OnCreate()
    {
        base.OnCreate();
        
        Effect();// ���ü���Ч��
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
        // ����ʱͣ����������Ұ�ڵ�С�ֵĶ���

    }
}
