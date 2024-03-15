using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo",menuName = "LevelSO/LevelInfo")]
public class LevelSO : ScriptableObject
{
    public int id;// 关卡id

    public List<TurnData> turnDataList = new List<TurnData>();// 存储关卡波次信息
}








