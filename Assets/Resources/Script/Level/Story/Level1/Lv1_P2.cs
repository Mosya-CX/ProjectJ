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
        guider.transform.position = new Vector3(GameManager.Instance.viewWidth / 2, 0, 0);// ���������߳�ʼλ��
        // ���ص�ͼͼƬ
        //Texture2D texture = Resources.Load<Texture2D>("");
        //MapImg = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        // ����Կ��ͼƬ
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

        // ���Ŵ�������

        while (true)
        {
            if (!(dialogueList.Count > 0))
            {
                break;
            }
            index++;
            // ��һ���ؼ���
            if (index == 5)
            {
                //��ݴ��Ҳ���Ļ���ߵ��ڽ��ĶԳ�λ��ͣ�²����Ŵ�������
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
            // �ڶ����ؼ���
            if (index == 16)
            {
                //ͼ��ϵͳչʾ�������½�Ȧס����ͼ����Բ���������⣬��Ļ��������΢΢�䰵

                // ��õ����ť
                Button click = null;
                bool isClicked = false;
                // ���ϵ���¼�
                UnityAction action = () =>
                {
                    OnClickButton(ref isClicked);
                };
                click.onClick.AddListener(action);
                while (isClicked)
                {
                    // �ȴ���Ӧ
                    yield return null;
                }
                click.onClick.RemoveListener(action);// �Ƴ�����¼�
                index++;

                //��ҵ����ɽ����ֻ���Ĺ���ͼ���������ɲ鿴�����ȴ���ҵ�����Ͻ� x �˳�
                // �ҵ�ͼ��UI��󶨹رհ�ť
                click = null;
                // ��ӵ���¼�
                isClicked = false;
                click.onClick.AddListener(action);
                while (isClicked)
                {
                    // �ȴ���Ӧ
                    yield return null;
                }
                click.onClick.RemoveListener(action);// �Ƴ�����¼�
                index++;
            }
            // �������ؼ���
            if (index == 33)
            {
                // ��ʾ��ͼͼƬ���𽥷Ŵ�
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
            // ���ĸ��ؼ���
            if (index == 42)
            {
                // ����С��ͼͼƬ������
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
            // ������ؼ���
            if (index == 51)
            {
                // ��ʾԿ��ͼƬ���𽥷Ŵ�
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
            // �������ؼ���
            if (index == 54)
            {
                // ����СԿ��ͼƬ������
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
