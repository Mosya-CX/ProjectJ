using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//狂热标记
//获得途径：打败地图1战斗1内的所有怪物
//触发条件：连续击杀敌人50次后，消耗sp 0点
//具体效果：进入狂热状态，每次击杀敌人的combo计数从1变为2。 

[CreateAssetMenu(fileName = "Skill01", menuName = "SkillSO/Skill01")]
public class Skill01 : BaseSkill
{
    [Header("请勿修改")]
    public int markCombo = 0;

    public override bool OnTrigger()
    {
        if( !base.OnTrigger())
        {
            markCombo = (int)ComboManager.Instance.comboNum;
            return false;
        }
        int curCombo = (int)ComboManager.Instance.comboNum;// 记录当前连击数
        int result = curCombo - markCombo;// 计算当前从满足条件开始的连击数
        if (result < 0)// 判断是否断了连击数
        {
            markCombo = curCombo;
            return false;
        }
        if (result >= 50)
        {
            markCombo = curCombo;
            return true;
        }
        return false;
    }
    public override void OnCreate()
    {
        base.OnCreate();
        // 进行UI或者外观上的一些调整

        Effect();// 调用技能效果
    }

    public override void Effect()
    {
        base.Effect();
        // 改变每次combo计数
        playData.comboAdd = 2;
    }

    public override void OnReset()
    {
        base.OnReset();
        markCombo = 0;
    }
}
