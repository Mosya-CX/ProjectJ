using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lv1_P4 : PerformConfig
{
    public Sprite skillImg;
    public float scaleVarySpeedRate = 0.5f;
    public Animator playerAni;
    public override void Init()
    {

        storyTellingUI.Bg.GetComponent<Image>().color = new Color(0, 0, 0, 0);

        // 加载狂热标记图片

        // 加载钥匙图片
        //Texture2D texture = Resources.Load<Texture2D>("Img/Skill/狂热");
        //skillImg = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        skillImg = Resources.Load<Sprite>("Img/Skill/Skill1");

        

        if (skillImg == null )
        {
            Debug.LogError("技能图片读取失败");
        }

        playerAni = actors["乌酱"].GetComponentInChildren<Animator>();
        playerAni.SetBool("Idle", true);
        playerAni.SetBool("Walk", false);
        playerAni.SetBool("Attack", false);

        actors["乌酱"].transform.position = LevelManager.Instance.curScene.transform.position - new Vector3(0, 2, 0);

        base.Init();
    }

    public override void Play()
    {
        base.Play();
        StartCoroutine(Act());
    }

    IEnumerator Act()
    {

        while (true)
        {
            if (!(dialogueList.Count > 0))
            {
                break;
            }
            index++;
            // 第一个关键点
            if (index == 3)
            {

                // 显示狂热标记图片并逐渐放大
                Transform itemDisplay = storyTellingUI.DisplayArea;
                itemDisplay.GetComponentInChildren<Image>().sprite = skillImg;
                RectTransform displayTransform = itemDisplay.GetComponent<RectTransform>();
                displayTransform.sizeDelta = new Vector2(skillImg.rect.width, skillImg.rect.height);
                displayTransform.localScale = Vector3.zero;
                itemDisplay.gameObject.SetActive(true);
                while (true)
                {
                    displayTransform.localScale += (Time.deltaTime * new Vector3(scaleVarySpeedRate, scaleVarySpeedRate, scaleVarySpeedRate));
                    if (displayTransform.localScale.x >= 0.8f)
                    {
                        break;
                    }
                    yield return null;
                }
                index++;
            }
            // 第二个关键点
            if (index == 8)
            {
                //技能【狂热标记】图片消失
                Transform itemDisplay = storyTellingUI.DisplayArea;
                itemDisplay.GetComponentInChildren<Image>().sprite = null;
                itemDisplay.gameObject.SetActive(false);
                index++;
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
                dialogueObj = PrepareDialogueObj();
            }

            Debug.Log("准备下一轮对话");

            curDialogueNode.DOText(tmp.words, duration);
            yield return new WaitForSecondsRealtime(duration);
            if (dialogueObj != null)
            {
                Destroy(dialogueObj);
            }
        }
        // 黑幕
        storyTellingUI.Bg.GetComponent<Image>().color = new Color(0, 0, 0, 255);

        SkillManager.Instance.AddSkill(SkillType.狂热标记);// 添加技能

        playerAni.SetBool("Idle", false);
        playerAni.SetBool("Walk", false);
        playerAni.SetBool("Attack", false);
        yield return new WaitForSecondsRealtime(1);

        yield break;
    }
}
