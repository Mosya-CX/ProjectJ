using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 玩家变量
    public float maxHp;
    public float curHp;
    public float maxEndurance;
    public float curEndurance;
    public bool isReadyToSkip;
    public bool isPushMistake;
    // 存储可攻击敌人的数据
    public List<Enemy> attackableEnemies;
    // 储存玩家按下的按键的信息
    KeyCode lastKeyCode;

    private void Start()
    {
        // 初始化
        curHp = maxHp;
        curEndurance = maxEndurance;
        attackableEnemies = new List<Enemy>();
        isReadyToSkip = false;
        isPushMistake = false;
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
                    Enemy Temp = null;
                    foreach (Enemy enemy in EnemyManager.Instance.enemyList)
                    {
                        if (Temp == null)
                        {
                            Temp = enemy;
                        }
                        else
                        {
                            if (Vector2.Distance(gameObject.transform.position, enemy.gameObject.transform.position) < Vector2.Distance(gameObject.transform.position, Temp.transform.position))
                            {
                                Temp = enemy;
                            }
                        }
                    }
                    if (Temp != null)
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
                            // 调用敌人的受击函数

                            isPushMistake = false;
                            break;
                        }
                    }
                    if (isPushMistake)
                    {
                        OnHit();
                        isPushMistake = false;
                    }
                }
                
                break; 
            }
        }


    }

    

    // 玩家受击判定
    public void OnHit()
    {
        // 播放受击动画

        // 播放受击音效

        // 扣状态
        curHp--;
    }

}
