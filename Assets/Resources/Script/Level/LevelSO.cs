using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO/LevelInfo",menuName = "LevelInfo")]
public class LevelSO : ScriptableObject
{
    public int id;// 关卡id
    public GameObject BattleScene;// 战斗场景
    public GameObject PathFindingPreBg;// 寻路前景
    public GameObject PathFindingBg;// 寻路背景
    public List<TurnData> turnDataList = new List<TurnData>();// 存储关卡战斗波次信息
}

[Serializable]
public class TurnData
{
    public bool isBossTurn;
    public int Enemy1Num;
    public int Enemy2Num;
    public int Enemy3Num;
    public int BossId;// 没有就填0
}