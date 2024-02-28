using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public bool isPushMistake;// �Ƿ񰴴��
    public bool isMove;// �ж��Ƿ��ڽ���λ��
    
    // �洢�ɹ������˵�����
    public List<Enemy> attackableEnemies;
    // ������Ұ��µİ�������Ϣ
    KeyCode lastKeyCode;
    // �����Ѫ��ui
    public Slider HpBar;
    // �����Ҹ������
    public Rigidbody2D rb;

    private void Start()
    {
        // ��ʼ��
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
        
        // ����ж����
        foreach (KeyCode Key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(Key) && isMove)
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
                        isMove = true;
                    }
                    else
                    {
                        // �����һ����������

                    }
                }
                // ����ж���ĸ����
                else if (Key.ToString().Length == 1)
                {
                    isPushMistake = true;
                    char key = Key.ToString()[0];
                    foreach (Enemy enemy in attackableEnemies)
                    {
                        if (enemy.currentHealthLetters.Contains(key))
                        {
                            // ������ҹ����߼�
                            // ����λ��
                            StartCoroutine(Rush(enemy.transform.position, gameObject.transform.position));
                            // �ӳٵ��ù���Ч��
                            StartCoroutine(Attack(totalTime, enemy, key));
                            // ����Comboϵͳ��������������
                 
     
                            isPushMistake = false;
                            break;
                        }
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

        // ����Ƿ���λ�Ʋ�����
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

    // ��ҹ���Ч��
    IEnumerator Attack(float DelayTime, Enemy enemy, char key)
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
}
