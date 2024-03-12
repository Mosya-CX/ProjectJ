using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO/LevelInfo",menuName = "LevelInfo")]
public class LevelSO : ScriptableObject
{
    public int id;// �ؿ�id
    public GameObject BattleScene;// ս������
    public GameObject PathFindingPreBg;// Ѱ·ǰ��
    public GameObject PathFindingBg;// Ѱ·����
    public List<TurnData> turnDataList = new List<TurnData>();// �洢�ؿ�ս��������Ϣ
}

[Serializable]
public class TurnData
{
    public bool isBossTurn;
    public int Enemy1Num;
    public int Enemy2Num;
    public int Enemy3Num;
    public int BossId;// û�о���0
}