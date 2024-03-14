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

    public int curTurn;
    public int maxTurn;
    public int enemyNum;

    public GameObject PathFindingPreBg;// Ѱ·ǰ��
    public GameObject PathFindingBg;// Ѱ·����
    public GameObject curScene;// ��¼��ǰ����

    public PlayerController playerData;// ��¼�����Ϣ

    public float pathFindingDuration;// ����Ѱ·��Ϣʱ��

    private void Start()
    {
        curTurn = 0;
        maxTurn = 0;
        enemyNum = 0;

        playerData = GameManager.Instance.Player.GetComponent<PlayerController>();
    }



    // ���عؿ���Ϣ
    public void LoadLevel()
    {
        // �������λ��

        // ��ʼ���ؿ�����
        curTurn = 0;
        maxTurn = curLevelData.turnDataList.Count;
        enemyNum = 0;

        // ���ó���
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
        // ���ص�ǰTurn
        curTurnData = curLevelData.turnDataList[curTurn];
        curTurnData.playerData = playerData;
        curTurnData.ScenePos = ScenePos;
        curTurnData.OnCreate();

    }

    public void LoadPathFinding()
    {
        // ���ر���
        curScene.SetActive(false);
        PathFindingBg.SetActive(true);

        // �������λ��

        // ������Ҷ���

        // �������״̬
        playerData.playerState = PlayerState.PathFinding;
        // �ӳټ�����һ��Turn
        StartCoroutine(AfterPathFinding());
    }

    // ��ȡ��ǰ�ؿ���Ϣ
    public void GetCurLevelData(int curProgress)
    {
        switch (curProgress)
        {
            case 1:
                curLevelData = Resources.Load(LevelPathConst.Level01Path) as LevelSO;
                break;
            default:
                Debug.LogWarning("�޵�ǰ��Ϸ���̵Ĺؿ���Ϣ");
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
                // �������Ƿ��Ķ�����

                break;
            case PlayerState.Fight:
                curTurnData.OnUpdate();
                // �жϳ������Ƿ�ֻʣ�����һ��С�����Ѿ�����

                break;
            case PlayerState.PathFinding:
                // ������������ƶ��߼�

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
    // �˴�д�ؿ�·��
    const string LevelDataPath = "Data/Level/";
    public const string Level01Path = LevelDataPath + "Level01";
}
