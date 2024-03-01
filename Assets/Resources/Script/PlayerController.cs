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
    Search,// Ѱ·״̬
    Fight,// ս��״̬
    Dead,// ����״̬
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

    // ��ʱ����ر���
    public float curTime;
    public float totalTime = 0.3f;

    public bool isReadyToSkip;// �Ƿ���������
    public bool isSkip;// �ж��Ƿ�������״̬
    public bool isAttack;// �ж��Ƿ��ڹ���״̬
    public bool isDead;// �ж��Ƿ�����

    // �洢�ɹ������˵�����
    public List<Enemy> attackableEnemies;
    // �����������µİ�������Ϣ
    KeyCode lastKeyCode;
    // �����Ѫ��ui
    public Slider HpBar;
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

        curTime = 0;

        playerState = PlayerState.None;

        rb = GetComponent<Rigidbody2D>();
        srFace = GetComponent<SpriteRenderer>();

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
        // Ѫ������
        HpGradualVary();

        // ���������
        foreach (KeyCode Key in System.Enum.GetValues(typeof(KeyCode)))
        {
            
            // �����ʲô����
            if (Input.GetKeyDown(Key))
            {
                
                lastKeyCode = Key;// ��������µļ�λ

                // �����ж����ⰴ��
                if (Key == KeyCode.Escape)
                {
                    UIManager.Instance.OpenPanel<SettingPanel>("SettingPanel");// �����ý���
                }
                else
                {
                    if (isAttack || isSkip || playerState == PlayerState.Dead)
                    {
                        break;// ����Ƿ��ڹ���������״̬��������״̬,�����򲻽�����ĸ�����ܼ��
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
                                srFace.flipY = false;
                            }
                            else
                            {
                                srFace.flipY = true;
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
                        // �ȸ��ɹ���������������(�и����������ǰ��Ȼ��̬�����ٵ���ǰ����������ĸ�ٵ���ǰ������ڸ���Acill��������)
                        AttackableSort();

                        bool isPushMistake = true;// ����Ƿ񰴴��
                        char key = Key.ToString()[0];

                        foreach (Enemy enemy in attackableEnemies)
                        {
                            // �����ͬ���͵ĵ���
                            if (enemy.currentHealthLetters[0] == key)
                            {
                                if (enemy.enemyType == 1)
                                {
                                    // ���ж�նɱ�������Ƿ��и�����ĸ�ĵ���
                                    // ���������նɱ����
                                    bool hasLightTone = false;
                                    foreach (Enemy killTar in tar)
                                    {
                                        //if

                                    }
                                    if (!hasLightTone)
                                    {
                                        tar.Add(enemy);
                                    }
                                }
                                else if (enemy.enemyType == 2)
                                {
                                    // ���ж��Ƿ��и�����ĸ
                                    // ����������նɱ����

                                    // ���û����նɱ������û���и�����ĸ�ĵ��ˣ�����õ��˵ĸ�������

                                }
                                else if (enemy.enemyType == 3)
                                {
                                    // ���ж��Ƿ�ֻʣһ����̬��ĸ
                                    // ����������նɱ����

                                    // �������նɱ������û���и�����ĸ�ĵ��ˣ�����õ��˵ĸ�������

                                }
                                isPushMistake = false;
                            }
                        }
                        // ������ҹ����߼�
                        if (tar.Count > 0)
                        {
                            // ����
                            KillableSort();

                            isAttack = true;// ����

                            Attack();// ���ù�������
                        }

                        if (isPushMistake)
                        {
                            // ���ж��Ƿ�ʹ���˵�����ʯ
                            // if

                            // ��û�о�ִ������Ĵ���
                            // ����Ϳ�Ѫ
                            OnHit(1);
                            // ����Comboϵͳ�������������


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

    }
    // ��������
    public void Attack()
    {
        Enemy tarEnemy = tar[0];
        // �жϳ���
        if (tarEnemy.transform.position.x - gameObject.transform.position.x >= 0)
        {
            srFace.flipY = true;
        }
        else
        {
            srFace.flipY = false;
        }
        // ����λ��
        StartCoroutine(Rush(tarEnemy.transform.position, gameObject.transform.position));
        // �ӳٵ��ù���Ч��
        StartCoroutine(AttackEffect(totalTime - 0.05f, tarEnemy, lastKeyCode.ToString()[0]));
        // ����Comboϵͳ��������������


        tar.RemoveAt(0);// ͷɾ
    }

    // ��ҹ���Ч��
    IEnumerator AttackEffect(float DelayTime, Enemy enemy, char key)
    {
        // �ӳ�
        yield return new WaitForSeconds(DelayTime);

        // ��֡Ч��

        // ��Ļ����Ч��

        // �����˴ӿɹ����������Ƴ�
        attackableEnemies.Remove(enemy);

        // ���õ��˵��ܻ�����
        enemy.OnHit(key);
    }

    // ��ҹ���λ��
    IEnumerator Rush(Vector2 tarPos, Vector2 startPos)
    {
        curTime = 0;// ���õ�ǰ��ʱ

        // �л���Ҷ���״̬

        while (curTime < totalTime)
        {
            curTime += Time.deltaTime;// ���µ�ǰʱ��  
            float t = curTime / totalTime;// ����ʱ���
            float v = EaseInOutCubic(t);// ͨ��ʱ������ٶȱ���Ϊ��ֵ

            Vector3 curPos = Vector2.Lerp(startPos, tarPos, v);// ���㵱ǰλ��
            gameObject.transform.position = curPos;// λ��

            yield return null;
        }

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
            // �����ܻ�����

            // �����ܻ���Ч

            // ��״̬()
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

    // ���ɹ�����������(�и����������ǰ��Ȼ��̬�����ٵ���ǰ����������ĸ�ٵ���ǰ������ڸ���Acill��������)
    public void AttackableSort()
    {
        List<Enemy> lightToneEnemies = new List<Enemy>();// �洢������ĸ����
        List<Enemy> normalToneEnemies = new List<Enemy>();// �洢��̬��ĸ����
        // �ȷ���
        for (int i = 0;i < attackableEnemies.Count;i++)
        {
            // �ж��Ƿ��Ǹ�����ĸ����
            // ��
            lightToneEnemies.Add(attackableEnemies[i]);
            // ��
            normalToneEnemies.Add(attackableEnemies[i]);
        }
        // ���������ĸ��
        for (int i = 0; i < lightToneEnemies.Count; i++)
        {
            // �ȷ���
            // ��Ϊ�ɱ�նɱ��Ͳ��ɱ�նɱ��


            // Ȼ������������������ƴ������

        }
        
        // ����̬��ĸ��
        normalToneEnemies = normalToneEnemies.OrderBy(b => b.currentHealthLetters.Length) // ��string������������  
                              .ThenBy(b => b.currentHealthLetters) // ������ͬʱ������ĸ˳���������򣨴�A��Z��  
                              .ToList();

        

        // �������õ�����ƴ������
        attackableEnemies = new List<Enemy>(lightToneEnemies);
        attackableEnemies.AddRange(normalToneEnemies);

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
