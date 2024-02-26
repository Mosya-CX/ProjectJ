using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ��ұ���
    public float maxHp;
    public float curHp;
    public float maxEndurance;
    public float curEndurance;
    public bool isReadyToSkip;
    public bool isPushMistake;
    // �洢�ɹ������˵�����
    public List<Enemy> attackableEnemies;
    // ������Ұ��µİ�������Ϣ
    KeyCode lastKeyCode;

    private void Start()
    {
        // ��ʼ��
        curHp = maxHp;
        curEndurance = maxEndurance;
        attackableEnemies = new List<Enemy>();
        isReadyToSkip = false;
        isPushMistake = false;
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
                            // ���õ��˵��ܻ�����

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

    

    // ����ܻ��ж�
    public void OnHit()
    {
        // �����ܻ�����

        // �����ܻ���Ч

        // ��״̬
        curHp--;
    }

}
