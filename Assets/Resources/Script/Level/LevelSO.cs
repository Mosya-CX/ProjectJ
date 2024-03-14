using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO/LevelInfo",menuName = "LevelInfo")]
public class LevelSO : ScriptableObject
{
    public int id;// �ؿ�id
    
    public GameObject PathFindingPreBg;// Ѱ·ǰ��
    public GameObject PathFindingBg;// Ѱ·����
    public List<TurnData> turnDataList = new List<TurnData>();// �洢�ؿ�������Ϣ
}

[Serializable]
public class TurnData : ScriptableObject
{
    public GameObject TurnScene;// ս������
    public Vector3 BornPos;// ��ʼλ��
    public PlayerController playerData;// ��¼�����Ϣ
    public Vector2 ScenePos;
    public virtual void OnCreate()
    {
        // �������λ��
        playerData.transform.position = BornPos;
        // ���س���
        if (LevelManager.Instance.curScene != TurnScene)
        {
            Destroy(LevelManager.Instance.curScene);
            Instantiate(TurnScene, ScenePos, Quaternion.identity);
        }
        else
        {
            LevelManager.Instance.curScene.SetActive(true);
        }
    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnDestory()
    {
        LevelManager.Instance.curTurn++;
    }

}

[CreateAssetMenu(fileName = "LevelSO/TurnInfo/StoryTurn", menuName = "StoryTurn")]
public class StoryTurn : TurnData
{
    public override void OnCreate()
    {
        base.OnCreate();
        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.StoryReading;
    }
}
[CreateAssetMenu(fileName = "LevelSO/TurnInfo/FightTurn", menuName = "FightTurn")]
public class FightTurn : TurnData
{
    public override void OnCreate()
    {
        base.OnCreate();
        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.Fight;
    }

    public override void OnDestory()
    {
        LevelManager.Instance.LoadPathFinding();
        base.OnDestory();
    }
}
[CreateAssetMenu(fileName = "LevelSO/TurnInfo/BossTurn", menuName = "BossTurn")]
public class BossTurn : TurnData
{
    public override void OnCreate()
    {
        base.OnCreate();
        GameManager.Instance.Player.GetComponent<PlayerController>().playerState = PlayerState.Fight;
    }

}


