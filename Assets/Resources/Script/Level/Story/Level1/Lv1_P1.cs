using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Lv1_P1 : PerformConfig
{
    

    public override void Init()
    {
        base.Init();
        storyTellingUI.Bg.GetComponent<Image>().color = new Color(0, 0, 0, 255);
        
    }
    public override void Play()
    {

        base.Play();
        
        //Init();
        Debug.Log("׼������Act");
        StartCoroutine(Act());
        //return false;
        Debug.Log("�˳�Act");
    }
 
    IEnumerator Act()
    {
        Debug.Log("����Act");
        while (true)
        {
            if (!(dialogueList.Count > 0))
            {
                break;
            }
            if (curDialogueNode != null)
            {
                curDialogueNode.text = "";// ���֮ǰ���ı�����
            }

            Dialogue tmp = dialogueList.Dequeue();
            float duration = duationTime_PerTenWords * tmp.words.Length / 10;
            speaker = actors[tmp.speaker];
            Debug.Log(tmp.words);
            Debug.Log("����ʱ��:" + duration);
            // ����˵��������
            GameObject dialogueObj = null;
            if (tmp.speaker == "������Ļ" || tmp.speaker == "��")
            {
                curDialogueNode = speaker.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                // ��ʼ�������
                PrepareDialogueObj(dialogueObj);
            }
            
            Debug.Log("׼����һ��Act");
            // ������Ч

            curDialogueNode.DOText(tmp.words, duration);
            yield return new WaitForSecondsRealtime(duration);
            if (dialogueObj != null)
            {
                Destroy(dialogueObj);
            }
        }
        Debug.Log("׼���˳�Act");
        isOver = true;
        //yield return null;
    }
}
