using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonWithMono<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        
        CreatePlayer();
    }

    // 游戏进程记录
    public int curProgress;
    // 储存玩家信息
    public GameObject Player;
    // 存储摄像机相关信息
    public float viewHeight;
    public float viewWidth;

    private void Start()
    {
        // 初始化变量
        curProgress = 0;

        viewHeight = Camera.main.orthographicSize * 2;
        viewWidth = viewHeight * Camera.main.aspect;

        // 初始界面和场景
        

        

        // 测试


    }


    private void Update()
    {
        
    }

    public void CreatePlayer()
    {
        //绑定玩家信息
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }
        
        if (Player == null)
        {
            // 生成玩家预制体
            Player = GameObject.Instantiate(Resources.Load("Prefab/Character/Player")) as GameObject;
            
        }
        Player.name = "Player";
        Player.SetActive(false);
    }
}
