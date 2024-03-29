using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestEditor
{
    [MenuItem("测试/读取关卡数据并加载")]
    public static void ReadAndLoadLevelData()
    {
        LevelManager.Instance.LoadLevel(LevelPathConst.Level01Path);
    }
    [MenuItem("测试/提供敌人")]
    public static void Enemy()
    {
        EnemyManager.Instance.CreateEnemy01(new Vector3(0,0, 0));
        EnemyManager.Instance.CreateEnemy02(new Vector3(0, 2, 2));
        EnemyManager.Instance.CreateEnemy03(new Vector3(0, 4, 4));

    }


}
