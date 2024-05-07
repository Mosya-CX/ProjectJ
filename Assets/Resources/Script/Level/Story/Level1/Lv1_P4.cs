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

        // ���ؿ��ȱ��ͼƬ

        // ����Կ��ͼƬ
        //Texture2D texture = Resources.Load<Texture2D>("Img/Skill/����");
        //skillImg = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        skillImg = Resources.Load<Sprite>("Img/Skill/Skill1");

        

        if (skillImg == null )
        {
            Debug.LogError("����ͼƬ��ȡʧ��");
        }

        playerAni = actors["�ڽ�"].GetComponentInChildren<Animator>();
        playerAni.SetBool("Idle", true);
        playerAni.SetBool("Walk", false);
        playerAni.SetBool("Attack", false);

        actors["�ڽ�"].transform.position = LevelManager.Instance.curScene.transform.position - new Vector3(0, 2, 0);

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
            // ��һ���ؼ���
            if (index == 3)
            {

                // ��ʾ���ȱ��ͼƬ���𽥷Ŵ�
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
            // �ڶ����ؼ���
            if (index == 8)
            {
                //���ܡ����ȱ�ǡ�ͼƬ��ʧ
                Transform itemDisplay = storyTellingUI.DisplayArea;
                itemDisplay.GetComponentInChildren<Image>().sprite = null;
                itemDisplay.gameObject.SetActive(false);
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
                dialogueObj = PrepareDialogueObj();
            }

            Debug.Log("׼����һ�ֶԻ�");

            curDialogueNode.DOText(tmp.words, duration);
            yield return new WaitForSecondsRealtime(duration);
            if (dialogueObj != null)
            {
                Destroy(dialogueObj);
            }
        }
        // ��Ļ
        storyTellingUI.Bg.GetComponent<Image>().color = new Color(0, 0, 0, 255);

        SkillManager.Instance.AddSkill(SkillType.���ȱ��);// ��Ӽ���

        playerAni.SetBool("Idle", false);
        playerAni.SetBool("Walk", false);
        playerAni.SetBool("Attack", false);
        yield return new WaitForSecondsRealtime(1);

        yield break;
    }
}
