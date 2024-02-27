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
    public bool isReadyToSkip;
    public bool isPushMistake;
    // �洢�ɹ������˵�����
    public List<Enemy> attackableEnemies;
    // ������Ұ��µİ�������Ϣ
    KeyCode lastKeyCode;
    // �����Ѫ��ui
    public Slider HpBar;
    private void Start()
    {
        // ��ʼ��
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
        
        // ����ж����
        foreach (KeyCode Key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(Key))
            {
                lastKeyCode = Key;
                if (Key == KeyCode.Space)
                {
                    // �洢��ǰ���Ͼ����������ĵ��˵���Ϣ
                    Enemy TheClosest = FindCloseEnemy();
                    
                    if (TheClosest != null)
                    {
                        // Զ������ĵ���

                    }
                    else
                    {
                        // �����һ����������

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
                            // ������ҹ����߼�
                            // ����λ��

                            // �ӳٵ��ù���Ч��
                            StartCoroutine(Attack(0.3f));
                            // ���õ��˵��ܻ�����
                            enemy.OnHit();

                            isPushMistake = false;
                            break;
                        }
                    }
                    if (isPushMistake)
                    {
                        // ����Ϳ�Ѫ
                        OnHit(1);

                        isPushMistake = false;
                    }
                }
                
                break; 
            }
        }


    }

    // ��ҹ���Ч��
    IEnumerator Attack(float DelayTime)
    {
        // �ӳ�
        yield return new WaitForSeconds(DelayTime);

        // ������Ч����

        // ��֡Ч��

        // ��Ļ����Ч��

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
                if (Vector2.Distance(gameObject.transform.position, enemy.gameObject.transform.position) < Vector2.Distance(gameObject.transform.position, Temp.transform.position))
                {
                    tmp = enemy;
                }
            }
        }

        return tmp;
    }
}
