using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public PlayerController playerData;// 玩家数据
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
            // 添加进可攻击名单
            playerData.attackableEnemies.Add(collision.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !playerData.isAttack && !playerData.isSkip)
        {
            // 重置敌人字母
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ResetHealthLetters();
            }
            // 移除可攻击名单
            playerData.attackableEnemies.Remove(collision.GetComponent<Enemy>());
            
        }
    }

}
