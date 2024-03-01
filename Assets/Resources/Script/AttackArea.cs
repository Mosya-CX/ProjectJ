using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public PlayerController playerData;// �������
    private void Start()
    {
        if (playerData == null)
        {
            playerData = GetComponentInParent<PlayerController>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !playerData.isAttack && !playerData.isSkip)
        {
            // ��ӽ��ɹ�������
            playerData.attackableEnemies.Add(collision.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !playerData.isAttack && !playerData.isSkip)
        {
            // ���õ�����ĸ
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ResetHealthLetters();
            }
            // �Ƴ��ɹ�������
            playerData.attackableEnemies.Remove(collision.GetComponent<Enemy>());
            
        }
    }

}
