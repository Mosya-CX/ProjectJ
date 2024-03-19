using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // 游戏进程记录
    public int curProgress;
    // 储存玩家信息
    public GameObject Player;

    private void Start()
    {
        // 初始化变量
        curProgress = 0;

        // 初始界面和场景
        //UIManager.Instance.OpenPanel(UIConst.LoginUI);

        // 绑定玩家信息
        //Player = GameObject.FindWithTag("Player");
        //if (Player == null)
        //{
        //    // 生成玩家预制体
        //    Player = GameObject.Instantiate(Resources.Load("Prefab/Character/Player")) as GameObject;
        //}

        // 测试
        
    }


    private void Update()
    {
        
    }
}
