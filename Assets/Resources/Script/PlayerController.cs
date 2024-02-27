using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // 玩家变量
    public float maxHp;
    public float curHp;
    //public float maxEndurance;
    //public float curEndurance;
    public bool isReadyToSkip;
    public bool isPushMistake;
    // 存储可攻击敌人的数据
    public List<Enemy> attackableEnemies;
    // 储存玩家按下的按键的信息
    KeyCode lastKeyCode;
    // 绑定玩家血条ui
    public Slider HpBar;
    private void Start()
    {
        // 初始化
        curHp = maxHp;
        //curEndurance = maxEndurance;
        attackableEnemies = new List<Enemy>();
        isReadyToSkip = false;
        isPushMistake = false;
        if (HpBar == null)
        {
            HpBar = GameObject.Find("UI/FightPanel/Top/StatusBar/HpBar").GetComponent<Slider>();
        }
        HpBar.maxValue = maxHp;
        HpBar.value = curHp;
        HpBar.minValue = 0;
    }

    private void Update()
    {
        
        // 玩家行动检测
        foreach (KeyCode Key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(Key))
            {
                lastKeyCode = Key;
                if (Key == KeyCode.Space)
                {
                    // 存储当前场上距离玩家最近的敌人的信息
                    Enemy TheClosest = FindCloseEnemy();
                    
                    if (TheClosest != null)
                    {
                        // 远离最近的敌人

                    }
                    else
                    {
                        // 随机向一个方向闪避

                    }
                }
                else if (Key.ToString().Length == 1)
                {
                    isPushMistake = true;
                    char key = Key.ToString()[0];
                    foreach (Enemy enemy in attackableEnemies)
                    {
                        if (enemy.key[0] == key)
                        {
                            // 处理玩家攻击逻辑
                            // 进行位移

                            // 延迟调用攻击效果
                            StartCoroutine(Attack(0.3f));
                            // 调用敌人的受击函数
                            enemy.OnHit();

                            isPushMistake = false;
                            break;
                        }
                    }
                    if (isPushMistake)
                    {
                        // 按错就扣血
                        OnHit(1);

                        isPushMistake = false;
                    }
                }
                
                break; 
            }
        }


    }

    // 玩家攻击效果
    IEnumerator Attack(float DelayTime)
    {
        // 延迟
        yield return new WaitForSeconds(DelayTime);

        // 攻击特效动画

        // 顿帧效果

        // 屏幕抖动效果

    }

    // 玩家受击判定
    public void OnHit(int Demage)
    {
        // 播放受击动画

        // 播放受击音效

        // 扣状态
        curHp -= Demage;
    }

    // 查询场景内距离玩家最近的敌人
    public Enemy FindCloseEnemy()
    {
        Enemy tmp = null;
        foreach (Enemy enemy in EnemyManager.Instance.enemyList)
        {
            if (tmp == null)
            {
                tmp = enemy;
            }
            else
            {
                if (Vector2.Distance(gameObject.transform.position, enemy.gameObject.transform.position) < Vector2.Distance(gameObject.transform.position, Temp.transform.position))
                {
                    tmp = enemy;
                }
            }
        }

        return tmp;
    }
}
