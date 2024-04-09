using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//圆形环游
//获得途径：完成2-2
//触发条件：在 3s 内连续输入 7 个正确键，消耗sp 1点
//具体效果：时停，引导者出现在攻击范围外的中型环状圆圈并逆时针清一圈怪

[CreateAssetMenu(fileName = "Skill06", menuName = "SkillSO/Skill06")]
public class Skill06 : BaseSkill
{
    public float curTime = 0;// 计时器
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
        if (curTime >= setPeriod)// 判断是否在规定时间内
        {
            curTime = 0;
            markCombo = (int)ComboManager.Instance.comboNum;
            return false;
        }
        else
        {
            int curCombo = (int)ComboManager.Instance.comboNum;
            if (curCombo - markCombo < 0)// 判断是否断了连击
            {
                markCombo = curCombo;
                curTime = 0;
                return false;
            }
            if (curCombo - markCombo >= standardCombo)// 判断是否达到标准
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

        // 时停

        // 并播放动画

        // 并依次杀死范围内敌人

    }
}
