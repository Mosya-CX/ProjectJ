using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class LoadStoryTextToSO : EditorWindow
{

    public static string FileName = "/Resources/Data/StoryText";
    [MenuItem("����/���ع����ı���ָ����StoryTurn")]
    public static void OpenWindow()
    {
        var window = GetWindow<LoadStoryTextToSO>("���ع����ı���ָ����StoryTurn");
        window.Show();
    }

    public void OnGUI()
    {
        GUILayout.Label("��ȷ��Ҫ���صľ籾�ļ�������");
        FileName = GUILayout.TextField(FileName);
        
        if (FileName.Length > 0)
        {
            if (GUILayout.Button("����"))
            {
                LoadAllLines(FileName);
            }
        }
        
    }

    public static void LoadAllLines(string fileName)
    {
        string filePath = Application.dataPath + fileName;
        string[] storyTexts = Directory.GetFiles(filePath, "*.txt");
        List<string> storyTextNames = new List<string>();
        for (int i = 0; i < storyTexts.Length; i++)
        {
            storyTextNames.Add(Path.GetFileNameWithoutExtension(storyTexts[i]));
        }
        for (int i = 0;i < storyTextNames.Count; i++)
        {
            Debug.Log("����"+storyTextNames[i]+"��");
            LoadDialogues(storyTextNames[i], storyTextNames[i]);
        }
    }

    public static void LoadDialogues(string lineInfoName, string storyTurnName)
    {
        StoryTurn turn = Resources.Load<StoryTurn>("Data/Level/" + storyTurnName);
        List<Dialogue> Lines = turn.Lines;
        Lines.Clear();
        List<string> actorsName = turn.actorsName;
        actorsName.Clear();
        string dialogues = Resources.Load<TextAsset>("Data/StoryText/" + lineInfoName).text;
        string[] lines = dialogues.Split("/");
        string[] names = lines[0].Split("��");
        for (int i = 0; i < names.Length; i++)
        {
            actorsName.Add(names[i]);
        }
        for (int i = 1; i < lines.Length; i++)
        {
            string[] tmp = lines[i].Split("��");
            Dialogue newDialogue = new Dialogue(tmp[0], tmp[1]);
            Lines.Add(newDialogue);
        }
        Debug.Log("���سɹ�:" + lineInfoName);
    }
}
