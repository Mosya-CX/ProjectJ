using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ʯ
//���;�������2-1
//�����������ɹ����ܵ��˹�������ʱͣʱ������sp 0 ��
//����Ч�������1��Ѫ����3�㻤��


[CreateAssetMenu(fileName = "Skill05", menuName = "SkillSO/Skill05")]
public class Skill5 : BaseSkill
{
    public override bool OnTrigger()
    {
        if( !base.OnTrigger())
        {
            return false;
        }
        if (playData.isSkipSuccess )
        {
            return true;
        }
        return false;
    }

    public override void OnCreate()
    {
        base.OnCreate();
        Effect();
    }

    public override void Effect()
    {
        base.Effect();
        // ����ʱͣ

        // ���1��Ѫ����3�㻤��
        if (!(playData.curHp + 1 > playData.maxHp))
        {
            playData.curHp++;
        }
        playData.shield = 3;

        isUsed = true;
    }
}
