using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform target;// 目标位置
    public float speed = 5f;// 设置AI移动速度
    public float nextNodeDistance = 3f;// 设置判断是否到达下个节点的临界值

    Path path;// 存储要遵循的路径
    int currentNode = 0;// 记录当前移动到了哪个节点
    public bool isReached = false;// 记录是否到达正在执行的路径的终点
    Seeker seeker;// 用于计算路径
    Rigidbody2D rb;// 实现移动逻辑用的组件(可用不用是Rigidbody)
    Transform model;// 存储敌人模型数据

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        model = transform.Find("Model");
    }

    public void StartUpdatePath()
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
    public void StopUpdatePath()
    {
        CancelInvoke(nameof(UpdatePath));
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
            model.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if (rb.velocity.x<-0.01)
        {
            model.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
        float distance = Vector2.Distance(rb.position , (Vector2)path.vectorPath[currentNode]);
        if(distance < nextNodeDistance )
        {
            currentNode++;
        }
    }
}
