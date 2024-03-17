using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D.IK;
using UnityEngine.UI;

public enum PlayerState
{
    None,
    PathFinding,// 寻路状态
    Fight,// 战斗状态
    Dead,// 死亡状态
    StoryReading,// 读剧情中
}

public class PlayerController : MonoBehaviour
{
    // 玩家变量
    public float maxHp;
    public float curHp;
    //public float maxEndurance;
    //public float curEndurance;

    public PlayerState playerState;// 玩家状态

    public float skipSpeed;// 闪避瞬时速度
    public float attackSpeed;// 攻击位移瞬时速度
    public float slowRate = 0.5f;// 减速率

    // 计时器相关变量(现用于斩杀间隔相关)
    public float curTime;
    public float totalTime = 0.2f;
    public float unattachableTime = 0.5f;
    
    public bool isReadyToSkip;// 是否允许闪避
    public bool isSkip;// 判断是否在闪避状态
    public bool isAttack;// 判断是否在攻击状态
    public bool isDead;// 判断是否死亡
    public bool isUnattachable;// 判断是否处于无敌帧状态

    // 存储可攻击敌人的数据
    public List<Enemy> attackableEnemies;
    // 储存玩家最后按下的按键的信息
    KeyCode lastKeyCode;
    // 绑定玩家血条ui
    public Slider HpBar;
    // 绑定玩家攻击范围判断触发器物体
    public GameObject AttackArea;
    // 获得玩家刚体组件
    public Rigidbody2D rb;
    // 判断朝向
    public SpriteRenderer srFace;

    public List<Enemy> tar;// 存储要斩杀的敌人

    private void Start()
    {
        // 初始化
        curHp = maxHp;
        //curEndurance = maxEndurance;

        attackableEnemies = new List<Enemy>();
        tar = new List<Enemy>();

        isReadyToSkip = false;
        isSkip = false;
        isAttack = false;
        isDead = false;
        isUnattachable = false;

        curTime = 0;

        // playerState = PlayerState.None;

        rb = GetComponent<Rigidbody2D>();
        srFace = GetComponent<SpriteRenderer>();

        AttackArea = transform.Find("AttackArea").gameObject;
        //if (HpBar == null)
        //{
        //    HpBar = GameObject.Find("UI/FightPanel/Top/StatusBar/HpBar").GetComponent<Slider>();
        //}
        
        //HpBar.maxValue = maxHp;
        //HpBar.value = curHp;
        //HpBar.minValue = 0;
    }

    private void Update()
    {
        // 血条渐变
        //HpGradualVary();

        // 玩家输入检测
        foreach (KeyCode Key in System.Enum.GetValues(typeof(KeyCode)))
        {
            
            // 检测是什么按键
            if (Input.GetKeyDown(Key))
            {
                Debug.Log(Key.ToString());
                lastKeyCode = Key;// 储存最后按下的键位

                // 优先判断特殊按键
                if (Key == KeyCode.Escape)
                {
                    UIManager.Instance.OpenPanel(UIConst.SettingUI);// 打开设置界面
                }
                else
                {
                    // 检测是否在攻击或闪避状态或者死亡状态,若是则不进行字母和闪避检测
                    if (isAttack || isSkip || playerState == PlayerState.Dead)
                    {
                        break;
                    }
                    // 然后判断闪避
                    if (Key == KeyCode.LeftShift)
                    {

                        // 判断是否为战斗状态
                        if (playerState != PlayerState.Fight)
                        {
                            // 播放虚化动画

                            break;
                        }

                        // 存储当前场上距离玩家最近的敌人的信息
                        Enemy TheClosest = FindCloseEnemy();

                        if (TheClosest != null)
                        {
                            isSkip = true;
                            // 计算方向
                            Vector2 skipDir = (gameObject.transform.position - TheClosest.transform.position).normalized;
                            // 切换成闪避姿势

                            // 判断要朝向
                            if (skipDir.x > 0)
                            {
                                srFace.flipX = false;
                            }
                            else
                            {
                                srFace.flipX = true;
                            }

                            // 判断最近的敌人在攻击范围内还是范围外
                            if (attackableEnemies.Count > 0)
                            {
                                // 远离最近的敌人
                                rb.velocity = skipDir * skipSpeed;

                            }
                            else
                            {
                                // 靠近最近的敌人
                                rb.velocity = -skipDir * skipSpeed;
                            }
                        }
                        else
                        {
                            // 播放虚化动画

                        }

                        break;
                    }
                    // 其次判断字母输入和是否在战斗状态
                    else if (Key.ToString().Length == 1 && playerState == PlayerState.Fight)
                    {
                        Debug.Log("通过字母输入初步检测");

                        bool isPushMistake = true;// 检测是否按错键

                        char key = Key.ToString()[0];
                        // 进一步检测是否是字母输入
                        if ((int)key < 65 || (int)key > 90)
                        {
                            break;
                        }
                        // 检测是否有可攻击敌人
                        if (attackableEnemies.Count <= 0)
                        {
                            // 先判断是否使用了道具磐石
                            // if

                            // 若没有就调用Combo系统，清空连击次数

                            // 按错就扣血
                            OnHit(1);

                            isPushMistake = false;

                            break;
                        }

                        Debug.Log("进入可攻击对象名单排序阶段");

                        // 先给可攻击对象名单排序(有高亮字体的在前，然后常态字体少的在前，接着是字母少的在前，最后在根据Acill码排升序)
                        AttackableSort();
                        
                        foreach (Enemy enemy in attackableEnemies)
                        {
                            Debug.Log("进入敌人判断阶段");
                            // 差别处理不同类型的敌人
                            if (enemy.currentHealthLetters[0] == key)
                            {
                                if (enemy.enemyType == 1)
                                {
                                    Debug.Log("进入敌人1");
                                    // 先判断斩杀名单中是否有其它级别的敌人
                                    // 若无则加入斩杀名单
                                    bool hasOhterType = false;
                                    foreach (Enemy killTar in tar)
                                    {
                                        if(killTar.enemyType != 1)
                                        {
                                            hasOhterType = true;
                                            break;
                                        }
                                    }
                                    if (!hasOhterType)
                                    {
                                        tar.Add(enemy);
                                    }
                                }
                                else if (enemy.enemyType == 2)
                                {
                                    Debug.Log("进入敌人2");
                                    // 先判断是否可被斩杀
                                    // 如果是则加入斩杀名单
                                    if (enemy.CanExeCute)
                                    {
                                        tar.Add(enemy);
                                    }
                                    // 如果否且斩杀名单内没有2级或三级敌人，没有则调用敌人的高亮函数
                                    else
                                    {
                                        bool canHighLight = true;
                                        foreach (Enemy killTar in tar)
                                        {
                                            if (killTar.enemyType != 1)
                                            {
                                                canHighLight = false;
                                                break;
                                            }
                                        }
                                        if (canHighLight)
                                        {
                                            enemy.OnHit(key);
                                        }
                                    }
                                }
                                else if (enemy.enemyType == 3)
                                {
                                    Debug.Log("进入敌人3");
                                    // 先判断是否只剩一个常态字母
                                    // 如果是则加入斩杀名单
                                    if (enemy.CanExeCute)
                                    {
                                        tar.Add(enemy);
                                    }
                                    // 如果否且斩杀名单内没有2级或三级敌人，没有则调用敌人的高亮函数
                                    else
                                    {
                                        bool canHighLight = true;
                                        foreach (Enemy killTar in tar)
                                        {
                                            if (killTar.enemyType != 1)
                                            {
                                                canHighLight = false;
                                                break;
                                            }
                                        }
                                        if (canHighLight)
                                        {
                                            enemy.OnHit(key);
                                        }
                                    }
                                }
                                isPushMistake = false;
                            }
                        }
                        // 处理玩家攻击逻辑
                        if (tar.Count > 0)
                        {
                            Debug.Log("进入斩杀阶段1");
                            // 排序
                            KillableSort();

                            isAttack = true;// 调整

                            Attack();// 调用攻击函数
                        }

                        if (isPushMistake)
                        {
                            // 先判断是否使用了道具磐石
                            // if

                            // 若没有就调用Combo系统，清空连击次数
                            
                            // 按错就扣血
                            OnHit(1);


                            isPushMistake = false;
                        }
                    }
                }
                
                break; 
            }
        }

        // 检测是否在闪避状态
        if (isSkip)
        {

            // 减速
            if (rb.velocity.magnitude >= slowRate)
            {
                rb.velocity *= (1f - slowRate * Time.deltaTime);
            }
            else
            {
                rb.velocity = Vector2.zero;
                isSkip = false;
                // 结束闪避姿势

            }
        }
        // 无敌帧状态检测
        if (isUnattachable)
        {
            if (curTime >= unattachableTime)
            {
                isUnattachable = false;
            }
            else
            {
                if (!isAttack)
                {
                    curTime += Time.deltaTime;
                }
            }
        }

    }

    // 攻击范围检测
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            // 添加进可攻击名单
            attackableEnemies.Add(collision.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            // 重置敌人字母
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ResetImage();
            }
            // 移除可攻击名单
            attackableEnemies.Remove(collision.GetComponent<Enemy>());

        }
    }

    // 攻击函数
    public void Attack()
    {
        Debug.Log("进入斩杀阶段2");
        Enemy tarEnemy = tar[0];
        // 判断朝向
        if (tarEnemy.transform.position.x - gameObject.transform.position.x >= 0)
        {
            srFace.flipX = true;
        }
        else
        {
            srFace.flipX = false;
        }
        // 进行位移
        Vector2 tarPos = tarEnemy.transform.position;
        StartCoroutine(Rush(tarPos, gameObject.transform.position));
        // 延迟调用攻击效果
        StartCoroutine(AttackEffect(totalTime * 0.75f, tarEnemy, lastKeyCode.ToString()[0]));
        // 调用Combo系统并增加连击次数


        tar.RemoveAt(0);// 从斩杀名单中移除
    }

    // 玩家攻击效果
    IEnumerator AttackEffect(float DelayTime, Enemy enemy, char key)
    {
        // 延迟
        yield return new WaitForSeconds(DelayTime);
        Debug.Log("进入斩杀阶段3");
        // 顿帧效果
        AttackMoment.Instance.HitPause();

        // 屏幕抖动效果
        AttackMoment.Instance.CamShake();

        // 将敌人从可攻击名单中移除
        attackableEnemies.Remove(enemy);
        Debug.Log("进入斩杀阶段5");
        // 调用敌人的受击函数
        enemy.OnHit(key);
        enemy.OnDeath();// 使敌人死亡
    }

    // 玩家攻击位移
    IEnumerator Rush(Vector2 tarPos, Vector2 startPos)
    {
        Debug.Log("进入斩杀阶段4");

        curTime = 0;// 重置当前计时

        // 切换玩家姿势

        while (curTime < totalTime)
        {
            curTime += Time.deltaTime;// 更新当前时间  
            float t = curTime / totalTime;// 计算时间比
            float v = EaseInOutCubic(t);// 通过时间比算速度比作为插值

            Vector3 curPos = Vector2.Lerp(startPos, tarPos, v);// 计算当前位置
            gameObject.transform.position = curPos;// 位移

            yield return null;
        }

        // 启用落地无敌帧
        isUnattachable = true;

        // 切换玩家姿势

        // 位移完成检测是否已斩杀完所有目标
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
        if (playerState != PlayerState.Dead)
        {
            // 先判断是否处于无敌帧，若否则执行下面的代码
            if (!isUnattachable)
            {

                // 播放受击动画

                // 播放受击音效

                // 扣状态
                if (curHp > Demage)
                {
                    curHp -= Demage;
                }
                else
                {
                    curHp = 0;
                    playerState = PlayerState.Dead;
                    // 播放死亡动画

                    // 延迟执行死亡后某些代码逻辑


                }
            }
            
        }
    }

    // 查询攻击范围内外距离玩家最近的敌人
    public Enemy FindCloseEnemy()
    {
        Enemy tmp = null;
        if (attackableEnemies.Count > 0)
        {
            foreach (Enemy enemy in attackableEnemies)
            {
                if (tmp == null)
                {
                    tmp = enemy;
                }
                else
                {
                    // 判断哪个敌人距离玩家最近
                    if (Vector2.Distance(gameObject.transform.position, enemy.gameObject.transform.position) < Vector2.Distance(gameObject.transform.position, tmp.transform.position))
                    {
                        tmp = enemy;
                    }
                }
            }
        }
        else
        {
            foreach (Enemy enemy in EnemyManager.Instance.enemyList)
            {
                if (tmp == null)
                {
                    tmp = enemy;
                }
                else
                {
                    // 判断哪个敌人距离玩家最近
                    if (Vector2.Distance(gameObject.transform.position, enemy.gameObject.transform.position) < Vector2.Distance(gameObject.transform.position, tmp.transform.position))
                    {
                        tmp = enemy;
                    }
                }
            }
        }

        return tmp;
    }

    // 给可攻击名单排序(有高亮字体的在前，接着是字母少的在前，最后在根据Acill码排升序)
    public void AttackableSort()
    {
        List<Enemy> lightTone = new List<Enemy>();// 存储高亮字母敌人
        List<Enemy> normalTone = new List<Enemy>();// 存储常态字母敌人
        List<Enemy> canExeCute = new List<Enemy>();// 存储可被斩杀敌人
        // 先分组
        for (int i = 0;i < attackableEnemies.Count;i++)
        {
            // 判断是否是高亮字母敌人
            // 是
            if (attackableEnemies[i].isHighLight)
            {
                // 判断是否可被斩杀
                if (attackableEnemies[i].CanExeCute)
                {
                    canExeCute.Add(attackableEnemies[i]);
                }
                else
                {
                    lightTone.Add(attackableEnemies[i]);
                }
            }
            // 否
            else
            {
                normalTone.Add(attackableEnemies[i]);
            }
        }
        // 排序可被斩杀组
        canExeCute = canExeCute.OrderByDescending(c => c.originalHealthLetters.Length)// 按string长度排降序
            .ThenBy(c => c.currentHealthLetters)// 按字母顺序排升序
            .ToList();
        
        // 排序高亮字母组
        lightTone = lightTone.OrderByDescending(c => c.originalHealthLetters.Length)// 按string长度排降序
            .ThenBy(c => c.currentHealthLetters)// 按字母顺序排升序
            .ToList();

        // 排序常态字母组
        normalTone = normalTone.OrderBy(b => b.currentHealthLetters.Length) // 按string长度升序排序  
                              .ThenBy(b => b.currentHealthLetters) // 长度相同时，按字母顺序升序排序（从A到Z）  
                              .ToList();

        // 最后将排序好的数据拼接起来
        attackableEnemies = new List<Enemy>(canExeCute);
        attackableEnemies.AddRange(lightTone);
        attackableEnemies.AddRange(normalTone);

    }
    
    // 给斩杀名单排序(字母多的在前，然后再按Acill码排升序)
    public void KillableSort()
    {
        tar = tar.OrderByDescending(t => t.currentHealthLetters.Length)// 按string长度排降序
            .ThenBy(t => t.currentHealthLetters)// 按字母顺序排升序
            .ToList();
    }

    // 血条渐变方法
    public void HpGradualVary()
    {
        if (curHp != HpBar.value)
        {
            HpBar.value = Mathf.Lerp(HpBar.value, curHp, Time.deltaTime);
        }
    }

}
