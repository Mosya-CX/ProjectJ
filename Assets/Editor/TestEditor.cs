using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestEditor
{
    [MenuItem("����/��ȡ�ؿ����ݲ�����")]
    public static void ReadAndLoadLevelData()
    {
        LevelManager.Instance.LoadLevel(LevelPathConst.Level01Path);
    }
}
