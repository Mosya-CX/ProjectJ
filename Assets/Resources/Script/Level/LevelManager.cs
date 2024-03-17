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
    public Vector2 ScenePos;// ���ó���λ��

    // ����ؿ���Ϣ
    public LevelSO curLevelData;
    public TurnData curTurnData;

    public int curTurn;// ��¼��ǰTurn
    public int maxTurn;

    public GameObject curScene;// ��¼��ǰ����
    public Dictionary<string, GameObject> loadedScene;// ��¼�Ѿ����ع��ĳ���
    //public GameObject enemySpawnPoints;// ��¼ս������������ˢ�ֵ�ĸ��ڵ�

    public PlayerController playerData;// ��¼�����Ϣ

    //public bool isLastTurn;

    private void Start()
    {
        curTurn = 0;
        maxTurn = 0;

        loadedScene =new Dictionary<string, GameObject>();

        playerData = GameManager.Instance.Player.GetComponent<PlayerController>();
    }



    // ���عؿ���Ϣ
    public void LoadLevel(string levelPath)
    {
        GetCurLevelData(levelPath);

        // �������λ��

        // ��ʼ���ؿ�����
        curTurn = 0;
        maxTurn = curLevelData.turnDataList.Count - 1;

        // ����Ѿ����ع��ĳ���
        if (loadedScene != null )
        {
            foreach (GameObject scene in loadedScene.Values )
            {
                Destroy( scene );
            }
            loadedScene.Clear();
        }
        // ���ص�һ������
        LoadTurn();
    }

    public void LoadTurn()
    {
        // ���ص�ǰTurn
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

    // ��ȡ��ǰ�ؿ���Ϣ
    public void GetCurLevelData(string levelPath)
    {
        if (levelPath == null)
        {
            Debug.LogWarning("·��Ϊ��:"+levelPath);
        }
        curLevelData = Resources.Load(levelPath) as LevelSO;
        if (curLevelData == null )
        {
            Debug.LogWarning("��·����δ�ҵ�Level�������:"+levelPath);
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
                // �������Ƿ��Ķ�����

                break;
            case PlayerState.Fight:
                curTurnData.OnUpdate();
                // �жϳ������Ƿ�ֻʣ�����һ��С���Ҽ�����������Boss��������

                break;
            case PlayerState.PathFinding:
                curTurnData.OnUpdate();
                break;
        }


    }

 

}

public class LevelPathConst
{
    // �˴�д�ؿ�·��
    const string LevelDataPath = "Data/Level/";
    public const string Level01Path = LevelDataPath + "Level01";
}
