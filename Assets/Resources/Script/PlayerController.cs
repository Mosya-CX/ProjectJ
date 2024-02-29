using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D.IK;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // ��ұ���
    public float maxHp;
    public float curHp;
    //public float maxEndurance;
    //public float curEndurance;

    public float skipSpeed;// ����˲ʱ�ٶ�
    public float attackSpeed;// ����λ��˲ʱ�ٶ�
    public float slowRate = 0.5f;// ������

    // ��ʱ����ر���
    public float curTime;
    public float totalTime = 0.3f;

    public bool isReadyToSkip;// �Ƿ���������
    public bool isSkip;// �ж��Ƿ�������״̬
    public bool isAttack;// �ж��Ƿ��ڹ���״̬
    
    // �洢�ɹ������˵�����
    public List<Enemy> attackableEnemies;
    // �����������µİ�������Ϣ
    KeyCode lastKeyCode;
    // �����Ѫ��ui
    public Slider HpBar;
    // �����Ҹ������
    public Rigidbody2D rb;

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
        
        // ���������
        foreach (KeyCode Key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (isAttack || isSkip)
            {
                break;// ����Ƿ��ڹ���������״̬�����򲻽��м��
            }
            if (Input.GetKeyDown(Key))
            {
                lastKeyCode = Key;// ��������µļ�λ
                // �����ж�����
                if (Key == KeyCode.LeftShift)
                {
                    // �洢��ǰ���Ͼ����������ĵ��˵���Ϣ
                    Enemy TheClosest = FindCloseEnemy();
                    
                    if (TheClosest != null)
                    {
                        Vector2 skipDir = (gameObject.transform.position - TheClosest.transform.position).normalized;
                        // Զ������ĵ���
                        rb.velocity = skipDir * skipSpeed;
                        isSkip = true;
                    }
                    else
                    {
                        // �����һ����������

                    }
                }
                // ����ж���ĸ����
                else if (Key.ToString().Length == 1)
                {
                    // �ȸ��ɹ���������������(�и����������ǰ��Ȼ��̬�����ٵ���ǰ����������ĸ�ٵ���ǰ������ڸ���Acill��������)
                    Sort();

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
                                
                                // ���������õ��˵ĸ�������
                                
                            }
                            isPushMistake = false;
                        }
                    }
                    // ������ҹ����߼�
                    if (tar.Count > 0)
                    {
                        // ����


                        isAttack = true;

                        Attack();// ���ù�������
                    }

                    if (isPushMistake)
                    {
                        // ����Ϳ�Ѫ
                        OnHit(1);
                        // �����������


                        isPushMistake = false;
                    }
                }
                
                break; 
            }
        }

        // ����Ƿ�������״̬������
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
    // ��������
    public void Attack()
    {
        Enemy tarEnemy = tar[0];
        // ����λ��
        StartCoroutine(Rush(tarEnemy.transform.position, gameObject.transform.position));
        // �ӳٵ��ù���Ч��
        StartCoroutine(AttackEffect(totalTime, tarEnemy, lastKeyCode.ToString()[0]));
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
        // �����ܻ�����

        // �����ܻ���Ч

        // ��״̬
        curHp -= Demage;
    }

    // ��ѯ�����ھ����������ĵ���
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

    // ���ɹ�����������(�и����������ǰ��Ȼ��̬�����ٵ���ǰ����������ĸ�ٵ���ǰ������ڸ���Acill��������)
    public void Sort()
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
        // ���������ĸ
        for (int i = 0; i < lightToneEnemies.Count; i++)
        {
            // �ȷ���
            // ��Ϊ�ɱ�նɱ��Ͳ��ɱ�նɱ��


            // Ȼ������������������ƴ������

        }
        
        // ����̬��ĸ
        var sortedList = normalToneEnemies.OrderBy(b => b.currentHealthLetters.Length) // ��string������������  
                              .ThenBy(b => b.currentHealthLetters) // ������ͬʱ������ĸ˳���������򣨴�A��Z��  
                              .ToList();

        // �������õ�����ƴ������
        attackableEnemies = new List<Enemy>(lightToneEnemies);
        attackableEnemies.AddRange(normalToneEnemies);

    }
    
}
