using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Pathfinding;

//public class aiTest : MonoBehaviour
//{
//    public Transform Target;// 目标位置
//    public float speed = 200f;// 设置AI移动速度
//    public float nextNodeDistance = 3f;// 设置判断是否到达下个节点的临界值

//    Path path;// 存储要遵循的路径
//    int currentNode = 0;// 记录当前移动到了哪个节点
//    bool isReached = false;// 记录是否到达正在执行的路径的终点
//    Seeker seeker;// 用于计算路径
//    Rigidbody2D rb;// 实现移动逻辑用的组件(可用不用是Rigidbody)

//    private void Start()
//    {
//        seeker = GetComponent<Seeker>();
//        rb = GetComponent<Rigidbody2D>();
//        // 计算路径 StartPath(AI当前位置，目标位置，当路径计算完时调用的函数)
//        InvokeRepeating(nameof(OnPathCounting), 0, 0.5f);
//    }

//    void OnPathCounting()
//    {
//        if (seeker.IsDone())
//        {
//            seeker.StartPath(rb.position, Target.position, OnCountCompelete);
//        }
//    }

//    void OnCountCompelete(Path p)
//    {
//        // 检测路径是否有错误
//        if (!p.error)
//        {
//            // 如果没有则将计算好的路径赋值给存储路径的地方
//            path = p;
//            currentNode = 0;// 重置当前节点
//        }
//    }

//    private void Update()
//    {
//        if (path == null)
//        {
//            return;
//        }

//        if (currentNode >= path.vectorPath.Count)
//        {
//            isReached = true;
//            return;
//        }
//        else
//        {
//            isReached = false;
//        }

//        if (!isReached)
//        {
//            Move();
//        }

//    }

//    private void Move()
//    {
//        // 计算移动方向(方向=目标节点位置 - 自身位置)
//        Vector2 Dir = ((Vector2)path.vectorPath[currentNode] - rb.position).normalized;
//        Vector2 Force = Dir * speed * Time.deltaTime;// 计算移动的加速度

//        // 判断AI朝向
//        if (Dir.x > 0.01f)
//        {
//            transform.localScale = new Vector3(-1, 1, 1);
//        }
//        else if (Dir.x < -0.01f)
//        {
//            transform.localScale = new Vector3(1, 1, 1);
//        }

//        rb.AddForce(Force);
//        // 记得要给AI加上一个阻力 

//        float distance = Vector2.Distance(rb.position, path.vectorPath[currentNode]);// 计算距离下个节点的距离
//        if (distance < nextNodeDistance)
//        {
//            currentNode++;
//        }

//    }

//}
