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

    public int curTurn;
    public int maxTurn;
    public int enemyNum;

    public GameObject PathFindingPreBg;// 寻路前景
    public GameObject PathFindingBg;// 寻路背景
    public GameObject curScene;// 记录当前场景

    public PlayerController playerData;// 记录玩家信息

    public float pathFindingDuration;// 设置寻路休息时间

    private void Start()
    {
        curTurn = 0;
        maxTurn = 0;
        enemyNum = 0;

        playerData = GameManager.Instance.Player.GetComponent<PlayerController>();
    }



    // 加载关卡信息
    public void LoadLevel()
    {
        // 重置玩家位置

        // 初始化关卡数据
        curTurn = 0;
        maxTurn = curLevelData.turnDataList.Count;
        enemyNum = 0;

        // 重置场景
        if (PathFindingBg != null)
        {
            Destroy(PathFindingPreBg);
        }
        if (PathFindingBg != null)
        {
            Destroy(PathFindingBg);
        }
        if (curScene != null)
        {
            Destroy(curScene);
        }
        GameObject temp = Instantiate(curLevelData.PathFindingBg, ScenePos, Quaternion.identity);
        temp.SetActive(false);
        temp = Instantiate(PathFindingPreBg, ScenePos, Quaternion.identity);
        temp.SetActive(false);
    }

    public void LoadTurn()
    {
        // 加载当前Turn
        curTurnData = curLevelData.turnDataList[curTurn];
        curTurnData.playerData = playerData;
        curTurnData.ScenePos = ScenePos;
        curTurnData.OnCreate();

    }

    public void LoadPathFinding()
    {
        // 加载背景
        curScene.SetActive(false);
        PathFindingBg.SetActive(true);

        // 设置玩家位置

        // 加载玩家动画

        // 设置玩家状态
        playerData.playerState = PlayerState.PathFinding;
        // 延迟加载下一个Turn
        StartCoroutine(AfterPathFinding());
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
                // 处理玩家物体移动逻辑

                break;
        }


    }

    IEnumerator AfterPathFinding()
    {
        yield return new WaitForSecondsRealtime(pathFindingDuration);

        LoadTurn();
    }

}

public class LevelPathConst
{
    // 此处写关卡路径
    const string LevelDataPath = "Data/Level/";
    public const string Level01Path = LevelDataPath + "Level01";
}
