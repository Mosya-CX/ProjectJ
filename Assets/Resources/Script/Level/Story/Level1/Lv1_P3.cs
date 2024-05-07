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
        player = actors["乌酱"].transform;
        player.position = mapPos.position + new Vector3(0, -1, 0);
        player.transform.Find("Image").localScale = new Vector3(-0.5f, 0.5f, 0.5f);// 面向右侧
        guider = actors["香草"].transform;
        guider.position = player.position + new Vector3(0, 2, 0);
        guider.transform.Find("Image").localScale = new Vector3(-0.5f, 0.5f, 0.5f);// 面向右侧


        // 加载并生成两只团子到指定位置上并失活
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
        actors["团子"] = enemyObj2;

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
        // 播放玩家和引导者的走路动画
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

            // 第一个关键点
            if (index == 5)
            {
                /*两只团子原地出现屏幕最右侧，两人变为待机动画，背景停止移动（会的话可以做个摄
                像机视角放大，然后拉至两个团子处再拉回来，视角缩小的效果再进入念台词环节）*/
                //播放玩家和引导者的待机动画
                playerAni.SetBool("Idel", true);
                playerAni.SetBool("Walk", false);
                playerAni.SetBool("Attack", false);
                guiderAni.SetBool("Idle", true);
                guiderAni.SetBool("Walk", false);
                guiderAni.SetBool("Run", false);
                // 激活敌人
                enemys[0].gameObject.SetActive(true);
                enemys[1].gameObject.SetActive(true);
                // 拉近摄像机
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
                // 拉回原位置
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
            // 第二个关键点
            if (index == 7)
            {
                //两只团子原地逐渐消失
                enemys[0].gameObject.SetActive(false);
                enemys[1].gameObject.SetActive(false);
                index++;
                isWalking = true;
                // 播放玩家和引导者的走路动画
                playerAni.SetBool("Idel", false);
                playerAni.SetBool("Walk", true);
                playerAni.SetBool("Attack", false);
                guiderAni.SetBool("Idle", false);
                guiderAni.SetBool("Walk", true);
                guiderAni.SetBool("Run", false);
            }
            // 第三个关键点
            if (index == 11)
            {
                //背景不再移动，香草向右走出画面

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
                //播放怪物声效(目前没有)
                //AudioClip clip = Effects.Dequeue();
                //soundManager.Instance.PlayEffect(clip);
                //yield return new WaitForSecondsRealtime(clip.length);
                index++;
                //乌酱面向左侧
                player.Find("Image").localScale = new Vector3(0.5f, 0.5f, 0.5f);
                Destroy(guider.gameObject);
                index++;
            }
            // 第四个关键点
            if (index == 17)
            {
                //一个怪物（头顶有字母）从左侧走入画面一小段距离后停下，
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
                curDialogueNode.text = "";// 清除之前的文本内容
            }

            Dialogue tmp = dialogueList.Dequeue();
            float duration = duationTime_PerTenWords * tmp.words.Length / 10;
            speaker = actors[tmp.speaker];
            if (speaker == null)
            {
                Debug.Log("找不到该Speaker对象:" + tmp.speaker);
            }
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
            // 计时
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
                // 移动地图
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

        //乌酱转向右侧走出画面
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
