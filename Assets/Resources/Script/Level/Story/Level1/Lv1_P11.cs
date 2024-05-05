using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lv1_P11 : PerformConfig
{
    public override void Init()
    {
        base.Init();
        storyTellingUI.Bg.GetComponent<Image>().color = new Color(0, 0, 0, 255);
    }

    public override void Play()
    {
        base.Play();

        Debug.Log("准备进入Act");
        StartCoroutine(Act());

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
            index++;

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
                dialogueObj = PrepareDialogueObj();
            }

            Debug.Log("准备下一轮Act");

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

