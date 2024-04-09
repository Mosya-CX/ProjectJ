using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//此身为键
//获得途径：打败boss1
//触发条件：每达成100次combo，消耗sp 0点
//具体效果： 时停并全屏清怪
[CreateAssetMenu(fileName = "Skill03", menuName = "SkillSO/Skill03")]
public class Skill03 : BaseSkill
{
    // 绑定对应的动画

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
        
        Effect();// 调用技能效果
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        // 判断动画是否播放结束

    }

    public override void OnDestory()
    {
        base.OnDestory();
        // 结束时停

        // 切换回待机动画

        
    }

    public override void Effect()
    {
        base.Effect();
        // 播放时停并清除玩家视野内的小怪的动画

    }
}
