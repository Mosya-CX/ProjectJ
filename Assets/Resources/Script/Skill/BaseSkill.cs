using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSkill : ScriptableObject
{
    // 相关变量
    public bool isUsed = false;
    public int spCost;// 技能花费
    public Image skillIcon;// 技能图标
    public PlayerController playData;// 绑定玩家数据
    public virtual bool OnTrigger()// 用于判断技能是否被触发
    {
        if (SkillManager.Instance.curSp - spCost < 0)
        {
            return false;
        }
        SkillManager.Instance.consumSP(spCost);
        return true;
    }
    public virtual void OnCreate()// 技能刚开始触发调用
    {
        isUsed = false;
        OnReset();
    }

    public virtual void OnUpdate()// 技能触发时持续调用
    {

    }

    public virtual void OnDestory()// 技能结束时调用
    {
        OnReset();
    }

    public virtual void Effect()// 技能效果
    {
        
    }

    public virtual void OnReset()// 用于重置技能某些变量
    {

    }
}
