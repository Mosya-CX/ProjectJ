using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public Vector2 ScenePos;// 设置场景位置

    // 储存关卡信息
    public LevelSO curLevelData;
    public TurnData curTurnData;

    public int curTurn;// 记录当前Turn
    public int maxTurn;

    public GameObject curScene;// 记录当前场景
    public Dictionary<string, GameObject> loadedScene;// 记录已经加载过的场景
    //public GameObject enemySpawnPoints;// 记录战斗场景的所有刷怪点的父节点

    public PlayerController playerData;// 记录玩家信息

    //public bool isLastTurn;

    private void Start()
    {
        curTurn = 0;
        maxTurn = 0;



        playerData = GameManager.Instance.Player.GetComponent<PlayerController>();
    }



    // 加载关卡信息
    public void LoadLevel()
    {
        // 重置玩家位置

        // 初始化关卡数据
        curTurn = 0;
        maxTurn = curLevelData.turnDataList.Count - 1;

        // 清空已经加载过的场景
        if (curLevelData != null )
        {
            foreach (GameObject scene in loadedScene.Values )
            {
                Destroy( scene );
            }
            loadedScene.Clear();
        }
        // 加载第一个场景
        LoadTurn();
    }

    public void LoadTurn()
    {
        // 加载当前Turn
        curTurnData = curLevelData.turnDataList[curTurn];
        curTurnData.playerData = playerData;
        curTurnData.ScenePos = ScenePos;
        if (curTurnData is FightTurn)
        {

        }
        else if (curTurnData is StoryTurn)
        {

        }
        else if (curTurnData is BossTurn)
        {

        }
        else if (curTurnData is PathFindingTurn)
        {

        }
        curTurnData.OnCreate();
        loadedScene.Add(curScene.name, curScene);
    }

    // 获取当前关卡信息
    public void GetCurLevelData(int curProgress)
    {
        switch (curProgress)
        {
            case 1:
                curLevelData = Resources.Load(LevelPathConst.Level01Path) as LevelSO;
                break;
            default:
                Debug.LogWarning("无当前游戏进程的关卡信息");
                break;
        }

    }

    private void Update()
    {
        switch(playerData.playerState)
        {
            case PlayerState.None:
                break;
            case PlayerState.StoryReading:
                curTurnData.OnUpdate();
                // 检测剧情是否阅读结束

                break;
            case PlayerState.Fight:
                curTurnData.OnUpdate();
                // 判断场景里是否只剩下最后一个小怪且已经死亡

                break;
            case PlayerState.PathFinding:
                curTurnData.OnUpdate();
                break;
        }


    }

 

}

public class LevelPathConst
{
    // 此处写关卡路径
    const string LevelDataPath = "Data/Level/";
    public const string Level01Path = LevelDataPath + "Level01";
}
