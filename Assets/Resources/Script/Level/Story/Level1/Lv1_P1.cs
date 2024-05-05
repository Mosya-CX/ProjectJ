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
        Debug.Log("准备进入Act");
        StartCoroutine(Act());
        //return false;
        Debug.Log("退出Act");
    }
 
    IEnumerator Act()
    {
        Debug.Log("进入Act");
        while (true)
        {
            if (!(dialogueList.Count > 0))
            {
                break;
            }
            if (curDialogueNode != null)
            {
                curDialogueNode.text = "";// 清除之前的文本内容
            }

            Dialogue tmp = dialogueList.Dequeue();
            float duration = duationTime_PerTenWords * tmp.words.Length / 10;
            speaker = actors[tmp.speaker];
            Debug.Log(tmp.words);
            Debug.Log("持续时间:" + duration);
            // 区别说话人类型
            GameObject dialogueObj = null;
            if (tmp.speaker == "黑屏字幕" || tmp.speaker == "你")
            {
                curDialogueNode = speaker.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                // 初始化聊天框
                PrepareDialogueObj(dialogueObj);
            }
            
            Debug.Log("准备下一轮Act");
            // 播放音效

            curDialogueNode.DOText(tmp.words, duration);
            yield return new WaitForSecondsRealtime(duration);
            if (dialogueObj != null)
            {
                Destroy(dialogueObj);
            }
        }
        Debug.Log("准备退出Act");
        isOver = true;
        //yield return null;
    }
}
