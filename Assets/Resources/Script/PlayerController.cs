using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 玩家变量
    public float maxHp;
    public float curHp;
    public float maxEndurance;
    public float curEndurance;

    private void Start()
    {
        // 初始化
        curHp = maxHp;
        curEndurance = maxEndurance;
    }

    private void Update()
    {
        // 玩家行动检测


    }

    // 玩家攻击范围检测方法

    // 玩家受击判定
    public void OnHit()
    {
        
    }

}
