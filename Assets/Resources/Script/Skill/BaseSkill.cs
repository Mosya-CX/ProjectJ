using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : ScriptableObject
{
    // 相关变量
    public bool isUsed = false;
    public int spCost;
    public virtual bool OnTrigger()
    {
        if (SkillManager.Instance.curSp - spCost < 0)
        {
            return false;
        }
        SkillManager.Instance.consumSP(spCost);
        return true;
    }
    public virtual void OnCreate()
    {
        
    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnDestory()
    {
        
    }

    public virtual void Effect()
    {
        
    }
}
