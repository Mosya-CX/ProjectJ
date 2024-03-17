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
    PathFinding,// Ѱ·״̬
    Fight,// ս��״̬
    Dead,// ����״̬
    StoryReading,// ��������
}

public class PlayerController : MonoBehaviour
{
    // ��ұ���
    public float maxHp;
    public float curHp;
    //public float maxEndurance;
    //public float curEndurance;

    public PlayerState playerState;// ���״̬

    public float skipSpeed;// ����˲ʱ�ٶ�
    public float attackSpeed;// ����λ��˲ʱ�ٶ�
    public float slowRate = 0.5f;// ������

    // ��ʱ����ر���(������նɱ������)
    public float curTime;
    public float totalTime = 0.2f;
    public float unattachableTime = 0.5f;
    
    public bool isReadyToSkip;// �Ƿ���������
    public bool isSkip;// �ж��Ƿ�������״̬
    public bool isAttack;// �ж��Ƿ��ڹ���״̬
    public bool isDead;// �ж��Ƿ�����
    public bool isUnattachable;// �ж��Ƿ����޵�֡״̬

    // �洢�ɹ������˵�����
    public List<Enemy> attackableEnemies;
    // �����������µİ�������Ϣ
    KeyCode lastKeyCode;
    // �����Ѫ��ui
    public Slider HpBar;
    // ����ҹ�����Χ�жϴ���������
    public GameObject AttackArea;
    // �����Ҹ������
    public Rigidbody2D rb;
    // �жϳ���
    public SpriteRenderer srFace;

    public List<Enemy> tar;// �洢Ҫնɱ�ĵ���

    private void Start()
    {
        // ��ʼ��
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
        // Ѫ������
        //HpGradualVary();

        // ���������
        foreach (KeyCode Key in System.Enum.GetValues(typeof(KeyCode)))
        {
            
            // �����ʲô����
            if (Input.GetKeyDown(Key))
            {
                Debug.Log(Key.ToString());
                lastKeyCode = Key;// ��������µļ�λ

                // �����ж����ⰴ��
                if (Key == KeyCode.Escape)
                {
                    UIManager.Instance.OpenPanel(UIConst.SettingUI);// �����ý���
                }
                else
                {
                    // ����Ƿ��ڹ���������״̬��������״̬,�����򲻽�����ĸ�����ܼ��
                    if (isAttack || isSkip || playerState == PlayerState.Dead)
                    {
                        break;
                    }
                    // Ȼ���ж�����
                    if (Key == KeyCode.LeftShift)
                    {

                        // �ж��Ƿ�Ϊս��״̬
                        if (playerState != PlayerState.Fight)
                        {
                            // �����黯����

                            break;
                        }

                        // �洢��ǰ���Ͼ����������ĵ��˵���Ϣ
                        Enemy TheClosest = FindCloseEnemy();

                        if (TheClosest != null)
                        {
                            isSkip = true;
                            // ���㷽��
                            Vector2 skipDir = (gameObject.transform.position - TheClosest.transform.position).normalized;
                            // �л�����������

                            // �ж�Ҫ����
                            if (skipDir.x > 0)
                            {
                                srFace.flipX = false;
                            }
                            else
                            {
                                srFace.flipX = true;
                            }

                            // �ж�����ĵ����ڹ�����Χ�ڻ��Ƿ�Χ��
                            if (attackableEnemies.Count > 0)
                            {
                                // Զ������ĵ���
                                rb.velocity = skipDir * skipSpeed;

                            }
                            else
                            {
                                // ��������ĵ���
                                rb.velocity = -skipDir * skipSpeed;
                            }
                        }
                        else
                        {
                            // �����黯����

                        }

                        break;
                    }
                    // ����ж���ĸ������Ƿ���ս��״̬
                    else if (Key.ToString().Length == 1 && playerState == PlayerState.Fight)
                    {
                        Debug.Log("ͨ����ĸ����������");

                        bool isPushMistake = true;// ����Ƿ񰴴��

                        char key = Key.ToString()[0];
                        // ��һ������Ƿ�����ĸ����
                        if ((int)key < 65 || (int)key > 90)
                        {
                            break;
                        }
                        // ����Ƿ��пɹ�������
                        if (attackableEnemies.Count <= 0)
                        {
                            // ���ж��Ƿ�ʹ���˵�����ʯ
                            // if

                            // ��û�о͵���Comboϵͳ�������������

                            // ����Ϳ�Ѫ
                            OnHit(1);

                            isPushMistake = false;

                            break;
                        }

                        Debug.Log("����ɹ���������������׶�");

                        // �ȸ��ɹ���������������(�и����������ǰ��Ȼ��̬�����ٵ���ǰ����������ĸ�ٵ���ǰ������ڸ���Acill��������)
                        AttackableSort();
                        
                        foreach (Enemy enemy in attackableEnemies)
                        {
                            Debug.Log("��������жϽ׶�");
                            // �����ͬ���͵ĵ���
                            if (enemy.currentHealthLetters[0] == key)
                            {
                                if (enemy.enemyType == 1)
                                {
                                    Debug.Log("�������1");
                                    // ���ж�նɱ�������Ƿ�����������ĵ���
                                    // ���������նɱ����
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
                                    Debug.Log("�������2");
                                    // ���ж��Ƿ�ɱ�նɱ
                                    // ����������նɱ����
                                    if (enemy.CanExeCute)
                                    {
                                        tar.Add(enemy);
                                    }
                                    // �������նɱ������û��2�����������ˣ�û������õ��˵ĸ�������
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
                                    Debug.Log("�������3");
                                    // ���ж��Ƿ�ֻʣһ����̬��ĸ
                                    // ����������նɱ����
                                    if (enemy.CanExeCute)
                                    {
                                        tar.Add(enemy);
                                    }
                                    // �������նɱ������û��2�����������ˣ�û������õ��˵ĸ�������
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
                        // ������ҹ����߼�
                        if (tar.Count > 0)
                        {
                            Debug.Log("����նɱ�׶�1");
                            // ����
                            KillableSort();

                            isAttack = true;// ����

                            Attack();// ���ù�������
                        }

                        if (isPushMistake)
                        {
                            // ���ж��Ƿ�ʹ���˵�����ʯ
                            // if

                            // ��û�о͵���Comboϵͳ�������������
                            
                            // ����Ϳ�Ѫ
                            OnHit(1);


                            isPushMistake = false;
                        }
                    }
                }
                
                break; 
            }
        }

        // ����Ƿ�������״̬
        if (isSkip)
        {

            // ����
            if (rb.velocity.magnitude >= slowRate)
            {
                rb.velocity *= (1f - slowRate * Time.deltaTime);
            }
            else
            {
                rb.velocity = Vector2.zero;
                isSkip = false;
                // ������������

            }
        }
        // �޵�֡״̬���
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

    // ������Χ���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            // ��ӽ��ɹ�������
            attackableEnemies.Add(collision.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            // ���õ�����ĸ
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ResetImage();
            }
            // �Ƴ��ɹ�������
            attackableEnemies.Remove(collision.GetComponent<Enemy>());

        }
    }

    // ��������
    public void Attack()
    {
        Debug.Log("����նɱ�׶�2");
        Enemy tarEnemy = tar[0];
        // �жϳ���
        if (tarEnemy.transform.position.x - gameObject.transform.position.x >= 0)
        {
            srFace.flipX = true;
        }
        else
        {
            srFace.flipX = false;
        }
        // ����λ��
        Vector2 tarPos = tarEnemy.transform.position;
        StartCoroutine(Rush(tarPos, gameObject.transform.position));
        // �ӳٵ��ù���Ч��
        StartCoroutine(AttackEffect(totalTime * 0.75f, tarEnemy, lastKeyCode.ToString()[0]));
        // ����Comboϵͳ��������������


        tar.RemoveAt(0);// ��նɱ�������Ƴ�
    }

    // ��ҹ���Ч��
    IEnumerator AttackEffect(float DelayTime, Enemy enemy, char key)
    {
        // �ӳ�
        yield return new WaitForSeconds(DelayTime);
        Debug.Log("����նɱ�׶�3");
        // ��֡Ч��
        AttackMoment.Instance.HitPause();

        // ��Ļ����Ч��
        AttackMoment.Instance.CamShake();

        // �����˴ӿɹ����������Ƴ�
        attackableEnemies.Remove(enemy);
        Debug.Log("����նɱ�׶�5");
        // ���õ��˵��ܻ�����
        enemy.OnHit(key);
        enemy.OnDeath();// ʹ��������
    }

    // ��ҹ���λ��
    IEnumerator Rush(Vector2 tarPos, Vector2 startPos)
    {
        Debug.Log("����նɱ�׶�4");

        curTime = 0;// ���õ�ǰ��ʱ

        // �л��������

        while (curTime < totalTime)
        {
            curTime += Time.deltaTime;// ���µ�ǰʱ��  
            float t = curTime / totalTime;// ����ʱ���
            float v = EaseInOutCubic(t);// ͨ��ʱ������ٶȱ���Ϊ��ֵ

            Vector3 curPos = Vector2.Lerp(startPos, tarPos, v);// ���㵱ǰλ��
            gameObject.transform.position = curPos;// λ��

            yield return null;
        }

        // ��������޵�֡
        isUnattachable = true;

        // �л��������

        // λ����ɼ���Ƿ���նɱ������Ŀ��
        if (tar.Count == 0)
        {
            isAttack = false;
        }
        else
        {
            // ûնɱ��ͼ�������
            Attack();
        }
    }

    // ���뻺�����ߺ���(�ﵽ�ȿ����Ч��)  
    private float EaseInOutCubic(float t)
    {
        t *= 2;
        if (t < 1) return 0.5f * t * t * t;
        t -= 2;
        return 0.5f * (t * t * t + 2);
    }

    // ����ܻ��ж�
    public void OnHit(int Demage)
    {
        if (playerState != PlayerState.Dead)
        {
            // ���ж��Ƿ����޵�֡��������ִ������Ĵ���
            if (!isUnattachable)
            {

                // �����ܻ�����

                // �����ܻ���Ч

                // ��״̬
                if (curHp > Demage)
                {
                    curHp -= Demage;
                }
                else
                {
                    curHp = 0;
                    playerState = PlayerState.Dead;
                    // ������������

                    // �ӳ�ִ��������ĳЩ�����߼�


                }
            }
            
        }
    }

    // ��ѯ������Χ��������������ĵ���
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
                    // �ж��ĸ����˾���������
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
                    // �ж��ĸ����˾���������
                    if (Vector2.Distance(gameObject.transform.position, enemy.gameObject.transform.position) < Vector2.Distance(gameObject.transform.position, tmp.transform.position))
                    {
                        tmp = enemy;
                    }
                }
            }
        }

        return tmp;
    }

    // ���ɹ�����������(�и����������ǰ����������ĸ�ٵ���ǰ������ڸ���Acill��������)
    public void AttackableSort()
    {
        List<Enemy> lightTone = new List<Enemy>();// �洢������ĸ����
        List<Enemy> normalTone = new List<Enemy>();// �洢��̬��ĸ����
        List<Enemy> canExeCute = new List<Enemy>();// �洢�ɱ�նɱ����
        // �ȷ���
        for (int i = 0;i < attackableEnemies.Count;i++)
        {
            // �ж��Ƿ��Ǹ�����ĸ����
            // ��
            if (attackableEnemies[i].isHighLight)
            {
                // �ж��Ƿ�ɱ�նɱ
                if (attackableEnemies[i].CanExeCute)
                {
                    canExeCute.Add(attackableEnemies[i]);
                }
                else
                {
                    lightTone.Add(attackableEnemies[i]);
                }
            }
            // ��
            else
            {
                normalTone.Add(attackableEnemies[i]);
            }
        }
        // ����ɱ�նɱ��
        canExeCute = canExeCute.OrderByDescending(c => c.originalHealthLetters.Length)// ��string�����Ž���
            .ThenBy(c => c.currentHealthLetters)// ����ĸ˳��������
            .ToList();
        
        // ���������ĸ��
        lightTone = lightTone.OrderByDescending(c => c.originalHealthLetters.Length)// ��string�����Ž���
            .ThenBy(c => c.currentHealthLetters)// ����ĸ˳��������
            .ToList();

        // ����̬��ĸ��
        normalTone = normalTone.OrderBy(b => b.currentHealthLetters.Length) // ��string������������  
                              .ThenBy(b => b.currentHealthLetters) // ������ͬʱ������ĸ˳���������򣨴�A��Z��  
                              .ToList();

        // �������õ�����ƴ������
        attackableEnemies = new List<Enemy>(canExeCute);
        attackableEnemies.AddRange(lightTone);
        attackableEnemies.AddRange(normalTone);

    }
    
    // ��նɱ��������(��ĸ�����ǰ��Ȼ���ٰ�Acill��������)
    public void KillableSort()
    {
        tar = tar.OrderByDescending(t => t.currentHealthLetters.Length)// ��string�����Ž���
            .ThenBy(t => t.currentHealthLetters)// ����ĸ˳��������
            .ToList();
    }

    // Ѫ�����䷽��
    public void HpGradualVary()
    {
        if (curHp != HpBar.value)
        {
            HpBar.value = Mathf.Lerp(HpBar.value, curHp, Time.deltaTime);
        }
    }

}
