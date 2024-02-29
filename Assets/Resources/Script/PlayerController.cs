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
    public bool isSkip;// 判断是否在闪避状态
    public bool isAttack;// 判断是否在攻击状态
    
    // 存储可攻击敌人的数据
    public List<Enemy> attackableEnemies;
    // 储存玩家最后按下的按键的信息
    KeyCode lastKeyCode;
    // 绑定玩家血条ui
    public Slider HpBar;
    // 获得玩家刚体组件
    public Rigidbody2D rb;

    public List<Enemy> tar;// 存储要斩杀的敌人

    private void Start()
    {
        // 初始化
        curHp = maxHp;
        //curEndurance = maxEndurance;
        attackableEnemies = new List<Enemy>();
        tar = new List<Enemy>();
        isReadyToSkip = false;
        isPushMistake = false;
        isSkip = false;
        isAttack = false;
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
        
        // 玩家输入检测
        foreach (KeyCode Key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (isAttack || isSkip)
            {
                break;// 检测是否在攻击或闪避状态若是则不进行检测
            }
            if (Input.GetKeyDown(Key))
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
                        isSkip = true;
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
                            if (enemy.enemyType == 1)
                            {
                                tar.Add(enemy);// 加入斩杀名单
                            }
                            else if (enemy.enemyType == 2)
                            {
                                // 先判断是否有高亮字母
                                // 如果有则加入斩杀名单

                                // 如果没有则调用敌人的高亮函数

                            }
                            else if (enemy.enemyType == 3)
                            {
                                // 先判断是否只剩一个常态字母
                                // 如果是则加入斩杀名单
                                
                                // 如果否则调用敌人的高亮函数
                                
                            }
                            // 处理玩家攻击逻辑
                            if (tar.Count > 0)
                            {
                                isAttack = true;

                                Attack();// 调用攻击函数
                            }
 
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

        // 检测是否在闪避状态并减速
        if (isSkip)
        {
            if (rb.velocity.magnitude >= slowRate)
            {
                rb.velocity *= (1f - slowRate * Time.deltaTime);
            }
            else
            {
                rb.velocity = Vector2.zero;
                isSkip = false;
            }
        }

    }
    // 攻击函数
    public void Attack()
    {
        Enemy tarEnemy = tar[0];
        // 进行位移
        StartCoroutine(Rush(tarEnemy.transform.position, gameObject.transform.position));
        // 延迟调用攻击效果
        StartCoroutine(AttackEffect(totalTime, tarEnemy, lastKeyCode.ToString()[0]));
        // 调用Combo系统并增加连击次数

        tar.RemoveAt(0);// 头删

    }

    // 玩家攻击效果
    IEnumerator AttackEffect(float DelayTime, Enemy enemy, char key)
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
        // 检测是否已斩杀完所有目标
        if (tar.Count == 0)
        {
            isAttack = false;
        }
        else
        {
            // 没斩杀完就继续调用
            Attack();
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
