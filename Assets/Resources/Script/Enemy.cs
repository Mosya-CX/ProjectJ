using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 敌人变量
    public List<char> key;// 存储当前敌人身上的字母
    public int enemyType;// 判断敌人种类

    private void Awake()
    {
        key = new List<char>();
    }

    private void Start()
    {
        // 初始化

        // 根据敌人种类生成敌人身上字母

    }

    // 随机生成敌人身上字母
    public void CreateKey()
    {
        key.Add((char)Random.Range(65, 91));
    }

    // 受击判定
    public void OnHit()
    {
        
    }

}
