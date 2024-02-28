using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public List<Enemy> attackableEnemyList;
    private void Start()
    {
        attackableEnemyList = gameObject.GetComponentInParent<PlayerController>().attackableEnemies;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            attackableEnemyList.Add(collision.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            // ÷ÿ÷√µ–»À◊÷ƒ∏
            Enemy enemy = collision.GetComponent<Enemy1>();
            if (enemy == null)
            {
                enemy = collision.GetComponent<Enemy2>();
            }
            if (enemy == null)
            {
                enemy = collision.GetComponent<Enemy3>();
            }
            if (enemy != null)
            {
                enemy.ResetHealthLetters();
            }

            attackableEnemyList.Remove(collision.GetComponent<Enemy>());
            
        }
    }

}
