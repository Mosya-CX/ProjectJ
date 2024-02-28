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

    public float skipSpeed;// 闪避瞬时速度
    public float attackSpeed;// 攻击位移瞬时速度
    public float slowRate = 0.5f;// 减速率

    // 计时器相关变量
    public float curTime;
    public float totalTime = 0.3f;

    public bool isReadyToSkip;// 是否允许闪避
    public bool isPushMistake;// 是否按错键
    public bool isMove;// 判断是否在进行位移
    
    // 存储可攻击敌人的数据
    public List<Enemy> attackableEnemies;
    // 储存玩家按下的按键的信息
    KeyCode lastKeyCode;
    // 绑定玩家血条ui
    public Slider HpBar;
    // 获得玩家刚体组件
    public Rigidbody2D rb;

    private void Start()
    {
        // 初始化
        curHp = maxHp;
        //curEndurance = maxEndurance;
        attackableEnemies = new List<Enemy>();
        isReadyToSkip = false;
        isPushMistake = false;
        curTime = 0;

        rb = GetComponent<Rigidbody2D>();

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
            if (Input.GetKeyDown(Key) && isMove)
            {
                lastKeyCode = Key;// 储存最后按下的键位
                // 优先判断闪避
                if (Key == KeyCode.LeftShift)
                {
                    // 存储当前场上距离玩家最近的敌人的信息
                    Enemy TheClosest = FindCloseEnemy();
                    
                    if (TheClosest != null)
                    {
                        Vector2 skipDir = (gameObject.transform.position - TheClosest.transform.position).normalized;
                        // 远离最近的敌人
                        rb.velocity = skipDir * skipSpeed;
                        isMove = true;
                    }
                    else
                    {
                        // 随机向一个方向闪避

                    }
                }
                // 其次判断字母输入
                else if (Key.ToString().Length == 1)
                {
                    isPushMistake = true;
                    char key = Key.ToString()[0];
                    foreach (Enemy enemy in attackableEnemies)
                    {
                        if (enemy.currentHealthLetters.Contains(key))
                        {
                            // 处理玩家攻击逻辑
                            // 进行位移
                            StartCoroutine(Rush(enemy.transform.position, gameObject.transform.position));
                            // 延迟调用攻击效果
                            StartCoroutine(Attack(totalTime, enemy, key));
                            // 调用Combo系统并增加连击次数
                 
     
                            isPushMistake = false;
                            break;
                        }
                    }
                    if (isPushMistake)
                    {
                        // 按错就扣血
                        OnHit(1);
                        // 清空连击次数


                        isPushMistake = false;
                    }
                }
                
                break; 
            }
        }

        // 检测是否在位移并减速
        if (isMove)
        {
            if (rb.velocity.magnitude >= slowRate)
            {
                rb.velocity *= (1f - slowRate * Time.deltaTime);
            }
            else
            {
                rb.velocity = Vector2.zero;
                isMove = false;
            }
        }

    }

    // 玩家攻击效果
    IEnumerator Attack(float DelayTime, Enemy enemy, char key)
    {
        // 延迟
        yield return new WaitForSeconds(DelayTime);

        // 顿帧效果

        // 屏幕抖动效果

        // 调用敌人的受击函数
        enemy.OnHit(key);
    }

    // 玩家攻击位移
    IEnumerator Rush(Vector2 tarPos, Vector2 startPos)
    {
        curTime = 0;// 重置当前计时

        // 切换玩家动画状态

        while (curTime < totalTime)
        {
            curTime += Time.deltaTime;// 更新当前时间  
            float t = curTime / totalTime;// 计算时间比
            float v = EaseInOutCubic(t);// 通过时间比算速度比作为插值

            Vector3 curPos = Vector2.Lerp(startPos, tarPos, v);// 计算当前位置
            gameObject.transform.position = curPos;// 位移

            yield return null;
        }

        
    }

    // 缓入缓出曲线函数(达到先快后慢效果)  
    private float EaseInOutCubic(float t)
    {
        t *= 2;
        if (t < 1) return 0.5f * t * t * t;
        t -= 2;
        return 0.5f * (t * t * t + 2);
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
                if (Vector2.Distance(gameObject.transform.position, enemy.gameObject.transform.position) < Vector2.Distance(gameObject.transform.position, tmp.transform.position))
                {
                    tmp = enemy;
                }
            }
        }

        return tmp;
    }
}
