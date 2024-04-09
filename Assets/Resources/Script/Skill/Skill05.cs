using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//安如磐石
//获得途径：完成2-1
//触发条件：成功闪避敌人攻击进入时停时，消耗sp 0 点
//具体效果：获得1点血量、3点护盾


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
        // 进入时停

        // 获得1点血量、3点护盾
        if (!(playData.curHp + 1 > playData.maxHp))
        {
            playData.curHp++;
        }
        playData.shield = 3;

        isUsed = true;
    }
}
