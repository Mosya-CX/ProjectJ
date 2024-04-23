using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LoadStoryTextToSO : EditorWindow
{
    public static string LineInfoName = "";
    public static string StoryTurnName = "";
    [MenuItem("����/���ع����ı���ָ����StoryTurn")]
    public static void OpenWindow()
    {
        var window = GetWindow<LoadStoryTextToSO>("���ع����ı���ָ����StoryTurn");
        window.Show();
    }

    public void OnGUI()
    {
        GUILayout.Label("�������ı�����");
        LineInfoName = GUILayout.TextField(LineInfoName);
        GUILayout.Label("������SO����");
        StoryTurnName = GUILayout.TextField(StoryTurnName);
        if (LineInfoName.Length >  0 || StoryTurnName.Length > 0)
        {
            if (GUILayout.Button("����" + LineInfoName + "��" + StoryTurnName))
            {
                LoadDialogues(LineInfoName, StoryTurnName);
            }
        }
        
    }

    public static void LoadDialogues(string lineInfoName, string storyTurnName)
    {
        StoryTurn turn = Resources.Load<StoryTurn>("Data/Level/" + storyTurnName);
        List<Dialogue> Lines = turn.Lines;
        List<string> actorsName = turn.actorsName;
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
    }
}
