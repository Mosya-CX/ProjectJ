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
}
