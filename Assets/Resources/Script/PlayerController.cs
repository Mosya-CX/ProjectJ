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
    // 存储可攻击敌人的数据
    public List<Enemy> attackableEnemies;

    private void Start()
    {
        // 初始化
        curHp = maxHp;
        curEndurance = maxEndurance;
        attackableEnemies = new List<Enemy>();
    }

    private void Update()
    {
        // 玩家行动检测


    }

    

    // 玩家受击判定
    public void OnHit()
    {
        
    }

}
