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
    public Transform SceneParentNode;// ���ó���λ��

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
    public bool isPause;



    private void Start()
    {
        

        
        
    }



    // ���عؿ���Ϣ
    public void LoadLevel(string levelPath)
    {
        
        if (!playerData.gameObject.activeSelf)
        {
            playerData.gameObject.SetActive(true);
        }

        GetCurLevelData(levelPath);
        Debug.Log("����Level���ݳɹ�");
        // �������λ��

        // ��ʼ���ؿ�����
        curTurn = 0;
        maxTurn = curLevelData.turnDataList.Count;

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
        Debug.Log("����Turn���ݳɹ�");
    }

    public void LoadTurn()
    {
        // ���ص�ǰTurn
        if (curTurn >= maxTurn)
        {
            // ������һ�ػ���ʲôʲô

            Debug.Log("��ǰ�ؿ����Խ���");
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
        if (playerData == null)
        {
            return;
        }
        if (isPause)
        {
            Debug.Log("LevelManager��ͣ��");
            return;
        }
        switch(playerData.playerState)
        {
            case PlayerState.None:
                break;
            case PlayerState.StoryReading:
                if (curTurnData == null)
                {
                    Debug.LogWarning("��ǰ��������Ϊ��");
                }
                curTurnData.OnUpdate();
                // �������Ƿ��Ķ�����

                break;
            case PlayerState.Fight:
                if (curTurnData ==  null)
                {
                    Debug.LogWarning("��ǰ��������Ϊ��");
                }
                curTurnData.OnUpdate();
                // �жϳ������Ƿ�ֻʣ�����һ��С���Ҽ�����������Boss��������

                break;
            case PlayerState.PathFinding:
                if (curTurnData == null)
                {
                    Debug.LogWarning("��ǰ��������Ϊ��");
                }
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
