using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo",menuName = "LevelSO/LevelInfo")]
public class LevelSO : ScriptableObject
{
    public int id;// �ؿ�id

    public List<TurnData> turnDataList = new List<TurnData>();// �洢�ؿ�������Ϣ
}








