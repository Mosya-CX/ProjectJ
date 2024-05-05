using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lv1_P3 : PerformConfig
{
    public float speed = 1f;
    public Transform mapPos;
    public float curTime;
    public Transform guider;
    public Transform[] enemys;
    public float enemyOffset = 2;
    public float viewScaleRate = 0.05f;
    public float viewMoveSpeed = 1f;
    public bool isWalking = false;
    public Transform player;
    public override void Init()
    {
        base.Init();
        storyTellingUI.Bg.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        mapPos = LevelManager.Instance.curScene.transform;
        curTime = 0;
        GameObject obj = Instantiate(Resources.Load("Prefab/Character/Guider") as GameObject);
        guider = obj.transform;
        guider.position = GameManager.Instance.Player.transform.position;
        guider.transform.Find("CharacterImage").GetComponent<SpriteRenderer>().flipX = true;// �����Ҳ�
        player = GameManager.Instance.Player.transform;
        player.transform.Find("PlayerImage").GetComponent<SpriteRenderer>().flipX = true;// �����Ҳ�
        // ���ز�������ֻ���ӵ�ָ��λ���ϲ�ʧ��
        GameObject enemyObj1 = Instantiate(Resources.Load("Prefab/Character/Enemy/Enemy01") as GameObject) ;
        GameObject enemyObj2 = Instantiate(Resources.Load("Prefab/Character/Enemy/Enemy01") as GameObject) ;
        GameObject enemyObj3 = Instantiate(Resources.Load("Prefab/Character/Enemy/Enemy01") as GameObject);
        enemyObj1.name = "Enemy01_StoryMode";
        enemyObj2.name = "Enemy01_StoryMode";
        enemys = new Transform[] { enemyObj1.transform, enemyObj2.transform, enemyObj3.transform };
        enemys[0].position = player.position + new Vector3(enemyOffset, 0, 0);
        enemys[1].position = guider.position + new Vector3(enemyOffset, 0, 0);
        enemys[2].position = player.position - new Vector3(GameManager.Instance.viewWidth/2+1, 0, 0);
        enemyObj1.SetActive(false);
        enemyObj2.SetActive(false);
        enemyObj3.SetActive(false);
    }

    public override void Play()
    {
        base.Play();
        isWalking = true;
        // ������Һ������ߵ���·����

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
            curTime = 0;

            // ��һ���ؼ���
            if (index == 5)
            {
                /*��ֻ����ԭ�س�����Ļ���Ҳ࣬���˱�Ϊ��������������ֹͣ�ƶ�����Ļ�����������
                ����ӽǷŴ�Ȼ�������������Ӵ������������ӽ���С��Ч���ٽ�����̨�ʻ��ڣ�*/
                //������Һ������ߵĴ�������

                // �������
                enemys[0].gameObject.SetActive(true);
                enemys[1].gameObject.SetActive(true);
                // ���������
                Vector3 tarPos = (enemys[0].position + enemys[1].position) / 2;
                Camera camera = Camera.main;
                while(true)
                {
                    camera.transform.position = Vector3.Lerp(camera.transform.position, tarPos, Time.deltaTime);
                    camera.orthographicSize -= viewScaleRate * Time.deltaTime;
                    if (Mathf.Abs(Vector3.Distance(camera.transform.position, tarPos)) <= 0.1)
                    {
                        camera.transform.position = tarPos;
                        break;
                    }
                    yield return null;
                }
                // ����ԭλ��
                while (true)
                {
                    camera.transform.position = Vector3.Lerp(camera.transform.position, Vector3.zero, Time.deltaTime);
                    camera.orthographicSize += viewScaleRate * Time.deltaTime;
                    if (Mathf.Abs(Vector3.Distance(camera.transform.position, Vector3.zero)) <= 0.1)
                    {
                        camera.transform.position = Vector3.zero;
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

            }
            // �������ؼ���
            if (index == 11)
            {
                //���������ƶ�����������߳�����
                isWalking = false;
                while (true)
                {
                    Vector3 pos = guider.transform.position;
                    pos.x += speed * Time.deltaTime;
                    guider.position = pos;
                    if (guider.position.x > (GameManager.Instance.viewWidth / 2 + 1))
                    {
                        break;
                    }
                    yield return null;
                }
                index++;
                //���Ź�����Ч
                AudioClip clip = Effects.Dequeue();

                yield return new WaitForSecondsRealtime(clip.length);
                index++;
                //�ڽ��������
                player.transform.Find("PlayerImage").GetComponent<SpriteRenderer>().flipX = false;
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
        player.transform.Find("PlayerImage").GetComponent<SpriteRenderer>().flipX = true;
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

        player.transform.Find("PlayerImage").GetComponent<SpriteRenderer>().flipX = false;
        isOver = true;
        
        yield break;
    }
}
