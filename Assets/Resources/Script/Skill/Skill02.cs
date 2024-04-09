using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//无双
//获得途径：完成1-2
//触发条件：敲击空格，消耗sp 3点
//具体效果：将全场所有敌人的对应按键改为 all。即无论 1/2/3 级的怪物均可以用 26 键	中的任意键位一击必杀
//持续时间：3s
//内置斩杀间隔：0.2s
//（之前作为无成本随机道具持续时间较短，但现在需要主动消耗sp点因此可以延长一下时间）

[CreateAssetMenu(fileName = "Skill02", menuName = "SkillSO/Skill02")]
public class Skill02 : BaseSkill
{
    public float duration = 3f;// 持续时间
    [Header("别修改")]
    public float curTime = 0f;// 记录当前流逝时间

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

        Effect();// 调用技能效果
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
        // 将敌人状态改回去

    }

    public override void OnReset()
    {
        base.OnReset();
        curTime = 0;
    }

    public override void Effect()
    {
        base.Effect();
        // 将全场所有敌人的对应按键改为 all。即无论 1/2/3 级的怪物均可以用 26 键中的任意键位一击必杀
    }
}
