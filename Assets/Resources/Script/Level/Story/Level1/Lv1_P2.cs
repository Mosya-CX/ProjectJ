using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Lv1_P2 : PerformConfig
{
    public float gradualTurnSpeed = 255f / 4;
    public Vector3 bornPos = new Vector3 (-1, 0, 0);
    public Transform guider;
    public float scaleVarySpeedRate = 0.05f;
    public float targetScale = 1.2f;
    public Sprite MapImg;
    public Sprite KeyImg;

    public override void Init()
    {
        base.Init();
        
        curDialogueNode = storyTellingUI.BlackBoardText.GetComponent<TextMeshProUGUI>();
        storyTellingUI.PlayerAsideText.text = "";
        storyTellingUI.Bg.GetComponent<Image>().color = new Color(0, 0, 0, 255);
        GameManager.Instance.Player.transform.position = bornPos;
        GameObject obj = Instantiate(Resources.Load("Prefab/Character/Guider") as GameObject);
        guider = obj.transform;
        guider.transform.position = new Vector3(GameManager.Instance.viewWidth / 2, 0, 0);// 设置引导者初始位置
        // 加载地图图片
        //Texture2D texture = Resources.Load<Texture2D>("");
        //MapImg = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        // 加载钥匙图片
        //texture = Resources.Load<Texture2D>("");
        //KeyImg = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    public override void Play()
    {
        base.Play();

        StartCoroutine(Act());
    }

    IEnumerator Act()
    {
        index++;
        yield return StartCoroutine(GradualTurnLight());

        // 播放待机动画

        while (true)
        {
            if (!(dialogueList.Count > 0))
            {
                break;
            }
            index++;
            // 第一个关键点
            if (index == 5)
            {
                //香草从右侧屏幕外走到乌酱的对称位置停下并播放待机动画
                while (true)
                {
                    Vector3 tarPos = new Vector3(-bornPos.x, bornPos.y, bornPos.z);                  
                    guider.position = new Vector3(Mathf.Lerp(guider.position.x, tarPos.x, Time.deltaTime), tarPos.y, tarPos.z);
                    if (Mathf.Abs(Vector3.Distance(tarPos, guider.position)) <= 0.1f)
                    {
                        guider.position = tarPos;
                        break;
                    }
                    yield return null;
                }
                index++;
            }
            // 第二个关键点
            if (index == 16)
            {
                //图鉴系统展示，除左下角圈住怪物图鉴的圆形区域以外，屏幕其他部分微微变暗

                // 获得点击按钮
                Button click = null;
                bool isClicked = false;
                // 加上点击事件
                UnityAction action = () =>
                {
                    OnClickButton(ref isClicked);
                };
                click.onClick.AddListener(action);
                while (isClicked)
                {
                    // 等待响应
                    yield return null;
                }
                click.onClick.RemoveListener(action);// 移除点击事件
                index++;

                //玩家点击后可进入手绘风格的怪物图鉴界面自由查看，并等待玩家点击右上角 x 退出
                // 找到图鉴UI后绑定关闭按钮
                click = null;
                // 添加点击事件
                isClicked = false;
                click.onClick.AddListener(action);
                while (isClicked)
                {
                    // 等待响应
                    yield return null;
                }
                click.onClick.RemoveListener(action);// 移除点击事件
                index++;
            }
            // 第三个关键点
            if (index == 33)
            {
                // 显示地图图片并逐渐放大
                Transform itemDisplay = storyTellingUI.DisplayArea;
                itemDisplay.GetComponent<Image>().sprite = MapImg;
                RectTransform displayTransform = itemDisplay.GetComponent<RectTransform>();
                displayTransform.sizeDelta = new Vector2(MapImg.rect.width, MapImg.rect.height);
                displayTransform.localScale = Vector3.zero;
                itemDisplay.gameObject.SetActive(true);
                while (true)
                {
                    displayTransform.localScale += (Time.deltaTime * new Vector3(scaleVarySpeedRate, scaleVarySpeedRate, scaleVarySpeedRate)); 
                    if (displayTransform.localScale.x >= targetScale)
                    {
                        break;
                    }
                    yield return null;
                }
                index++;
            }
            // 第四个关键点
            if (index == 42)
            {
                // 逐渐缩小地图图片并销毁
                Transform itemDisplay = storyTellingUI.DisplayArea;
                RectTransform displayTransform = itemDisplay.GetComponent<RectTransform>();
                while (true)
                {
                    displayTransform.localScale -= (Time.deltaTime * new Vector3(scaleVarySpeedRate, scaleVarySpeedRate, scaleVarySpeedRate));
                    if (displayTransform.localScale.x <= scaleVarySpeedRate)
                    {
                        itemDisplay.GetComponent<Image>().sprite = null;
                        displayTransform.sizeDelta = new Vector2 (100, 100);
                        itemDisplay.gameObject.SetActive(false);
                        break;
                    }
                    yield return null;
                }
                index++;
            }
            // 第五个关键点
            if (index == 51)
            {
                // 显示钥匙图片并逐渐放大
                Transform itemDisplay = storyTellingUI.DisplayArea;
                itemDisplay.GetComponent<Image>().sprite = KeyImg;
                RectTransform displayTransform = itemDisplay.GetComponent<RectTransform>();
                displayTransform.sizeDelta = new Vector2(KeyImg.rect.width, KeyImg.rect.height);
                displayTransform.localScale = Vector3.zero;
                itemDisplay.gameObject.SetActive(true);
                while (true)
                {
                    displayTransform.localScale += (Time.deltaTime * new Vector3(scaleVarySpeedRate, scaleVarySpeedRate, scaleVarySpeedRate));
                    if (displayTransform.localScale.x >= targetScale)
                    {
                        break;
                    }
                    yield return null;
                }
                index++;
            }
            // 第六个关键点
            if (index == 54)
            {
                // 逐渐缩小钥匙图片并销毁
                Transform itemDisplay = storyTellingUI.DisplayArea;
                RectTransform displayTransform = itemDisplay.GetComponent<RectTransform>();
                while (true)
                {
                    displayTransform.localScale -= (Time.deltaTime * new Vector3(scaleVarySpeedRate, scaleVarySpeedRate, scaleVarySpeedRate));
                    if (displayTransform.localScale.x <= scaleVarySpeedRate)
                    {
                        itemDisplay.GetComponent<Image>().sprite = null;
                        displayTransform.sizeDelta = new Vector2(100, 100);
                        itemDisplay.gameObject.SetActive(false);
                        break;
                    }
                    yield return null;
                }

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
                PrepareDialogueObj(dialogueObj);
            }

            Debug.Log("准备下一轮Act");

            curDialogueNode.DOText(tmp.words, duration);
            yield return new WaitForSecondsRealtime(duration);
            if (dialogueObj != null)
            {
                Destroy(dialogueObj);
            }
        }

        storyTellingUI.Bg.GetComponent<Image>().color = new Color(0, 0, 0, 255);
        Destroy(guider);
        yield return new WaitForSecondsRealtime(2);

        isOver = true;
        yield break;
    }

    IEnumerator GradualTurnLight()
    {
        Color orginalColor = curDialogueNode.color;
        while (true)
        {
            Color color = curDialogueNode.color;
            if (color.a - gradualTurnSpeed <= 0 )
            {
                color.a = 0;
                curDialogueNode.color = color;
                break;
            }
            color.a -= gradualTurnSpeed;
            curDialogueNode.color = color;
            yield return null;
        }

        Color tmp = storyTellingUI.Bg.GetComponent<Image>().color;
        tmp.a = 0;
        storyTellingUI.Bg.GetComponent<Image>().color = tmp;
        curDialogueNode.text = "";
        curDialogueNode.color = orginalColor;

        yield break;
    }

    public void OnClickButton(ref bool isClicked)
    {
        isClicked = true;
    }
    
}
