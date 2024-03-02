using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 回春：
//回复角色 1 点血量，（例如没断连击杀第 10 个怪物时掉落了回春，那么角色将会回复 2
//点血，回春+1，combo=10，+1）

public class Item02 : BaseItem
{
    public float healHp = 1;
    public GameObject playerInfo;
    public Animator playerAni;
    public PlayerController playerData;
    public override void OnCreate()
    {
        base.OnCreate();
        // 初始化成员变量
        healHp = 1;
        // 获得玩家的信息
        playerInfo = GameObject.FindWithTag("Player");
        playerAni = playerInfo.GetComponent<Animator>();
        playerData = playerInfo.GetComponent<PlayerController>();
        // 播放回血动画

        // 其它调整

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        // 检测回血动画播放进度
        // 到达合适的进度时调用Effect()
        // 当播放完毕时将isUsed改为true
    }

    public override void OnDestory()
    {
        base.OnDestory();
    }

    public override void Effect()
    {
        base.Effect();
        // 检测Combo次数
        // 若大于等于10则healHp+1
        if (playerData.curHp + healHp >= playerData.maxHp)
        {
            playerData.curHp = playerData.maxHp;
        }
        else
        {
            playerData.curHp += healHp;
        }
    }
}
