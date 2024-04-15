using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : SingletonWithMono<LevelManager>
{
    protected override void Awake()
    {
        base.Awake();
        curTurn = 0;
        maxTurn = 0;

        isPause = false;

        if (SceneParentNode ==  null)
        {
            GameObject obj = new GameObject();
            obj.transform.position = Vector3.zero;
            obj.name = "SceneParentNode";
            SceneParentNode = obj.transform;
        }

        loadedScene = new Dictionary<string, GameObject>();
        playerData = GameManager.Instance.Player.GetComponent<PlayerController>();
    }
    public Transform SceneParentNode;// 设置场景位置

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
    public bool isPause;



    private void Start()
    {
        

        
        
    }



    // 加载关卡信息
    public void LoadLevel(string levelPath)
    {
        
        if (!playerData.gameObject.activeSelf)
        {
            playerData.gameObject.SetActive(true);
        }

        GetCurLevelData(levelPath);
        Debug.Log("加载Level数据成功");
        // 重置玩家位置

        // 初始化关卡数据
        curTurn = 0;
        maxTurn = curLevelData.turnDataList.Count;

        // 清空已经加载过的场景
        if (loadedScene != null )
        {
            foreach (GameObject scene in loadedScene.Values )
            {
                Destroy( scene );
            }
            loadedScene.Clear();
        }
        // 加载第一个场景
        LoadTurn();
        Debug.Log("加载Turn数据成功");
    }

    public void LoadTurn()
    {
        // 加载当前Turn
        if (curTurn >= maxTurn)
        {
            // 加载下一关或者什么什么

            Debug.Log("当前关卡测试结束");
            return;
        }
        else
        {
            curTurnData = curLevelData.turnDataList[curTurn];
        }
        curTurnData.playerData = playerData;
        curTurnData.parent = SceneParentNode;
        playerData.transform.position = curTurnData.BornPos;
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
        curScene = curTurnData.OnCreate();
        if (!loadedScene.ContainsKey(curScene.name))
        {
            loadedScene.Add(curScene.name, curScene);
        }
    }

    // 获取当前关卡信息
    public void GetCurLevelData(string levelPath)
    {
        if (levelPath == null)
        {
            Debug.LogWarning("路径为空:"+levelPath);
        }
        curLevelData = Resources.Load(levelPath) as LevelSO;
        if (curLevelData == null )
        {
            Debug.LogWarning("此路径下未找到Level相关数据:"+levelPath);
        }
    }

    private void Update()
    {
        if (playerData == null)
        {
            return;
        }
        if (isPause)
        {
            Debug.Log("LevelManager暂停中");
            return;
        }
        switch(playerData.playerState)
        {
            case PlayerState.None:
                break;
            case PlayerState.StoryReading:
                if (curTurnData == null)
                {
                    Debug.LogWarning("当前场景数据为空");
                }
                curTurnData.OnUpdate();
                // 检测剧情是否阅读结束

                break;
            case PlayerState.Fight:
                if (curTurnData ==  null)
                {
                    Debug.LogWarning("当前场景数据为空");
                }
                curTurnData.OnUpdate();
                // 判断场景里是否只剩下最后一个小怪且即将死亡或者Boss即将死亡

                break;
            case PlayerState.PathFinding:
                if (curTurnData == null)
                {
                    Debug.LogWarning("当前场景数据为空");
                }
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
