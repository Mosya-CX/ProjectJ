using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//磐石：
//为角色增加额外 3 格永久失误盾，只要不被消耗就会一直存在，盾条只可抵消失误防止
//断连，不可抵消伤害。

public class Item03 : BaseItem
{
    public int curShield = 3;
    public Transform ShieldArea;

    public override void OnCreate()
    {
        base.OnCreate();
        // 初始化数据
        curShield = 3;
        // 通过UIManager绑定护盾区域

        // 其它调整

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        // 通过判断ShieldArea的子物体数量与curShield来调整UI
        if (ShieldArea.childCount != curShield)
        {

        }
    }

    public override void OnDestory()
    {
        base.OnDestory();
        // 清空ShieldArea的子物体

        
    }

    public override void Effect()
    {
        base.Effect();
        curShield--;
        if (curShield <= 0 )
        {
            isUsed = true;
        }
    }
}
