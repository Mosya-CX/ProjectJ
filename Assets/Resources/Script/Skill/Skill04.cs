using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ψ�첻��
//���;�������boss2
//����������������ɱ����50�κ�����sp 1��
//����Ч�����������״̬��ÿ�λ�ɱ���˵�combo������2��Ϊ4

[CreateAssetMenu(fileName = "Skill04", menuName = "SkillSO/Skill04")]
public class Skill04 : BaseSkill
{
    public override bool OnTrigger()
    {
        if( !base.OnTrigger())
        {
            return false;
        }
        if ( playData.comboAdd == 2)
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
        playData.comboAdd = 4;
    }
}
