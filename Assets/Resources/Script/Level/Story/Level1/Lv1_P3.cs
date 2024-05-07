using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lv1_P3 : PerformConfig
{
    public float speed = 2f;
    public Transform mapPos;
    public float curTime;
    public Transform guider;
    public Transform[] enemys;
    public float enemyOffset = 6;
    public float viewScaleRate = 0.5f;
    public float viewMoveSpeed = 1f;
    public bool isWalking = false;
    public Transform player;

    public override void Init()
    {
        base.Init();
        storyTellingUI.Bg.GetComponent<Image>().color = new Color(0, 0, 0, 0);

        mapPos = LevelManager.Instance.curScene.transform;
        Camera.main.transform.position = mapPos.position + new Vector3(0, 0, -10);
        curTime = 0;
        player = actors["�ڽ�"].transform;
        player.position = mapPos.position + new Vector3(0, -1, 0);
        player.transform.Find("Image").localScale = new Vector3(-0.5f, 0.5f, 0.5f);// �����Ҳ�
        guider = actors["���"].transform;
        guider.position = player.position + new Vector3(0, 2, 0);
        guider.transform.Find("Image").localScale = new Vector3(-0.5f, 0.5f, 0.5f);// �����Ҳ�


        // ���ز�������ֻ���ӵ�ָ��λ���ϲ�ʧ��
        GameObject enemyObj1 = Instantiate(Resources.Load("Prefab/Character/Enemy/Enemy01") as GameObject);
        GameObject enemyObj2 = Instantiate(Resources.Load("Prefab/Character/Enemy/Enemy01") as GameObject);
        GameObject enemyObj3 = Instantiate(Resources.Load("Prefab/Character/Enemy/Enemy01") as GameObject);
        enemyObj1.name = "Enemy01_StoryMode";
        enemyObj2.name = "Enemy01_StoryMode";
        enemyObj3.name = "Enemy01_StoryMode";
        enemys = new Transform[] { enemyObj1.transform, enemyObj2.transform, enemyObj3.transform };
        enemys[0].position = player.position + new Vector3(enemyOffset, 0, 0);
        enemys[1].position = guider.position + new Vector3(enemyOffset, 0, 0);
        enemys[2].position = player.position - new Vector3((Camera.main.orthographicSize * Camera.main.aspect) + 2, 0, 0);
        enemyObj1.SetActive(false);
        enemyObj2.SetActive(false);
        enemyObj3.SetActive(false);
        foreach (Transform e in enemys)
        {
            e.GetComponent<EnemyController>().enabled = false;
        }
        actors["����"] = enemyObj2;

    }

    public override void Play()
    {
        

        base.Play();
        isWalking = true;
        

        StartCoroutine(Act());
    }

    IEnumerator Act()
    {
        index++;
        Animator playerAni = player.GetComponentInChildren<Animator>();
        Animator guiderAni = guider.GetComponentInChildren<Animator>();
        // ������Һ������ߵ���·����
        playerAni.SetBool("Idel", false);
        playerAni.SetBool("Walk", true);
        playerAni.SetBool("Attack", false);
        guiderAni.SetBool("Idle", false);
        guiderAni.SetBool("Walk", true);
        guiderAni.SetBool("Run", false);

        while (true)
        {
            if (!(dialogueList.Count > 0))
            {
                break;
            }

            index++;
            curTime = 0;

            // ��һ���ؼ���
            if (index == 5)
            {
                /*��ֻ����ԭ�س�����Ļ���Ҳ࣬���˱�Ϊ��������������ֹͣ�ƶ�����Ļ�����������
                ����ӽǷŴ�Ȼ�������������Ӵ������������ӽ���С��Ч���ٽ�����̨�ʻ��ڣ�*/
                //������Һ������ߵĴ�������
                playerAni.SetBool("Idel", true);
                playerAni.SetBool("Walk", false);
                playerAni.SetBool("Attack", false);
                guiderAni.SetBool("Idle", true);
                guiderAni.SetBool("Walk", false);
                guiderAni.SetBool("Run", false);
                // �������
                enemys[0].gameObject.SetActive(true);
                enemys[1].gameObject.SetActive(true);
                // ���������
                Vector3 tarPos = (enemys[0].position + enemys[1].position) / 2 + new Vector3(0, 0, -10);
                Camera camera = Camera.main;
                Vector3 originalPos = camera.transform.position;
                while(true)
                {
                    camera.transform.position = Vector3.Lerp(camera.transform.position, tarPos, Time.deltaTime);
                    camera.orthographicSize -= viewScaleRate * Time.deltaTime;
                    if (Mathf.Abs(Vector3.Distance(camera.transform.position, tarPos)) <= 0.5f)
                    {
                        camera.transform.position = tarPos;
                        break;
                    }
                    yield return null;
                }
                // ����ԭλ��
                while (true)
                {
                    camera.transform.position = Vector3.Lerp(camera.transform.position, originalPos, Time.deltaTime);
                    camera.orthographicSize += viewScaleRate * Time.deltaTime;
                    if (Mathf.Abs(Vector3.Distance(camera.transform.position, originalPos)) <= 0.5f)
                    {
                        camera.transform.position = originalPos;
                        break;
                    }
                    yield return null;
                }
                index++;
                isWalking = false;
            }
            // �ڶ����ؼ���
            if (index == 7)
            {
                //��ֻ����ԭ������ʧ
                enemys[0].gameObject.SetActive(false);
                enemys[1].gameObject.SetActive(false);
                index++;
                isWalking = true;
                // ������Һ������ߵ���·����
                playerAni.SetBool("Idel", false);
                playerAni.SetBool("Walk", true);
                playerAni.SetBool("Attack", false);
                guiderAni.SetBool("Idle", false);
                guiderAni.SetBool("Walk", true);
                guiderAni.SetBool("Run", false);
            }
            // �������ؼ���
            if (index == 11)
            {
                //���������ƶ�����������߳�����

                playerAni.SetBool("Idel", true);
                playerAni.SetBool("Walk", false);
                playerAni.SetBool("Attack", false);

                isWalking = false;
                while (true)
                {
                    Vector3 pos = guider.transform.position;
                    pos.x += speed * Time.deltaTime;
                    guider.position = pos;
                    if (guider.position.x > ((Camera.main.orthographicSize * Camera.main.aspect) + 2))
                    {
                        break;
                    }
                    yield return null;
                }
                index++;
                //���Ź�����Ч(Ŀǰû��)
                //AudioClip clip = Effects.Dequeue();
                //soundManager.Instance.PlayEffect(clip);
                //yield return new WaitForSecondsRealtime(clip.length);
                index++;
                //�ڽ��������
                player.Find("Image").localScale = new Vector3(0.5f, 0.5f, 0.5f);
                Destroy(guider.gameObject);
                index++;
            }
            // ���ĸ��ؼ���
            if (index == 17)
            {
                //һ�����ͷ������ĸ����������뻭��һС�ξ����ͣ�£�
                Transform enemy = enemys[2];
                enemy.gameObject.SetActive(true);
                while (true)
                {
                    Vector3 pos = enemy.position;
                    pos.x += speed * Time.deltaTime;
                    enemy.position = pos;
                    if (enemy.position.x >= (player.position.x - 3))
                    {
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
            if (speaker == null)
            {
                Debug.Log("�Ҳ�����Speaker����:" + tmp.speaker);
            }
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

            Debug.Log("׼����һ��Act");

            curDialogueNode.DOText(tmp.words, duration);
            // ��ʱ
            while (true)
            {
                if (curTime >= duration)
                {
                    break;
                }
                else
                {
                    curTime += Time.deltaTime;
                }
                // �ƶ���ͼ
                if (isWalking)
                {
                    Vector3 pos = mapPos.position;
                    pos.x -= speed * Time.deltaTime;
                    mapPos.position = pos;
                }
                
                yield return null;
            }
            //yield return new WaitForSecondsRealtime(duration);
            if (dialogueObj != null)
            {
                Destroy(dialogueObj);
            }
        }

        //�ڽ�ת���Ҳ��߳�����
        player.transform.Find("Image").localScale = new Vector3(-0.5f,0.5f,0.5f);
        while (true)
        {
            Vector3 pos = player.transform.position;
            pos.x += speed * Time.deltaTime;
            player.position = pos;
            if (player.position.x > (GameManager.Instance.viewWidth / 2 + 1))
            {
                break;
            }
            yield return null;
        }
        player.transform.Find("Image").localScale = new Vector3(0.5f, 0.5f, 0.5f);
        isOver = true;

        for (int i = 0; i < enemys.Length; i++)
        {
            Destroy(enemys[i].gameObject);
        }

        yield break;
    }
}
