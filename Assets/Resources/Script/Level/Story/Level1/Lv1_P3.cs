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
        guider.transform.Find("CharacterImage").GetComponent<SpriteRenderer>().flipX = true;// 面向右侧
        player = GameManager.Instance.Player.transform;
        player.transform.Find("PlayerImage").GetComponent<SpriteRenderer>().flipX = true;// 面向右侧
        // 加载并生成两只团子到指定位置上并失活
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
        // 播放玩家和引导者的走路动画

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

            // 第一个关键点
            if (index == 5)
            {
                /*两只团子原地出现屏幕最右侧，两人变为待机动画，背景停止移动（会的话可以做个摄
                像机视角放大，然后拉至两个团子处再拉回来，视角缩小的效果再进入念台词环节）*/
                //播放玩家和引导者的待机动画

                // 激活敌人
                enemys[0].gameObject.SetActive(true);
                enemys[1].gameObject.SetActive(true);
                // 拉近摄像机
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
                // 拉回原位置
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
            // 第二个关键点
            if (index == 7)
            {
                //两只团子原地逐渐消失
                enemys[0].gameObject.SetActive(false);
                enemys[1].gameObject.SetActive(false);
                index++;
                isWalking = true;
                // 播放玩家和引导者的走路动画

            }
            // 第三个关键点
            if (index == 11)
            {
                //背景不再移动，香草向右走出画面
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
                //播放怪物声效
                AudioClip clip = Effects.Dequeue();

                yield return new WaitForSecondsRealtime(clip.length);
                index++;
                //乌酱面向左侧
                player.transform.Find("PlayerImage").GetComponent<SpriteRenderer>().flipX = false;
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
