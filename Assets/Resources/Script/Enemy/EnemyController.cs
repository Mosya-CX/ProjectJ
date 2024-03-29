using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform target;// Ŀ��λ��
    public float speed = 5f;// ����AI�ƶ��ٶ�
    public float nextNodeDistance = 3f;// �����ж��Ƿ񵽴��¸��ڵ���ٽ�ֵ

    Path path;// �洢Ҫ��ѭ��·��
    int currentNode = 0;// ��¼��ǰ�ƶ������ĸ��ڵ�
    public bool isReached = false;// ��¼�Ƿ񵽴�����ִ�е�·�����յ�
    Seeker seeker;// ���ڼ���·��
    Rigidbody2D rb;// ʵ���ƶ��߼��õ����(���ò�����Rigidbody)

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        InvokeRepeating(nameof(UpdatePath), 0, 1f);
    }
    private void UpdatePath()
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
    private void OnPathComplete(Path p)
    {
        path = p;
        currentNode = 0;
    }
    private void FixedUpdate()
    {
        Move();
        float distance2 = Vector2.Distance(rb.position, target.position);
        if(distance2<0.5)
        {
            rb.velocity = Vector2.MoveTowards(rb.velocity,Vector2.zero,10);
        }
    }
    public void Move()
    {
        if(path==null) return;
        if (currentNode>=path.vectorPath.Count)
        {
            isReached = true;
            return;
        }
        else
        {
            isReached= false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentNode]-rb.position).normalized;
        rb.velocity = direction * speed;
        if(rb.velocity.x>0.01)
        {
            transform.localScale = Vector3.one;
        }
        else if (rb.velocity.x<-0.01)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        float distance = Vector2.Distance(rb.position , (Vector2)path.vectorPath[currentNode]);
        if(distance < nextNodeDistance )
        {
            currentNode++;
        }
    }
}
