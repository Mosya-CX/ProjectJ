using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Pathfinding;

//public class aiTest : MonoBehaviour
//{
//    public Transform Target;// Ŀ��λ��
//    public float speed = 200f;// ����AI�ƶ��ٶ�
//    public float nextNodeDistance = 3f;// �����ж��Ƿ񵽴��¸��ڵ���ٽ�ֵ

//    Path path;// �洢Ҫ��ѭ��·��
//    int currentNode = 0;// ��¼��ǰ�ƶ������ĸ��ڵ�
//    bool isReached = false;// ��¼�Ƿ񵽴�����ִ�е�·�����յ�
//    Seeker seeker;// ���ڼ���·��
//    Rigidbody2D rb;// ʵ���ƶ��߼��õ����(���ò�����Rigidbody)

//    private void Start()
//    {
//        seeker = GetComponent<Seeker>();
//        rb = GetComponent<Rigidbody2D>();
//        // ����·�� StartPath(AI��ǰλ�ã�Ŀ��λ�ã���·��������ʱ���õĺ���)
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
//        // ���·���Ƿ��д���
//        if (!p.error)
//        {
//            // ���û���򽫼���õ�·����ֵ���洢·���ĵط�
//            path = p;
//            currentNode = 0;// ���õ�ǰ�ڵ�
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
//        // �����ƶ�����(����=Ŀ��ڵ�λ�� - ����λ��)
//        Vector2 Dir = ((Vector2)path.vectorPath[currentNode] - rb.position).normalized;
//        Vector2 Force = Dir * speed * Time.deltaTime;// �����ƶ��ļ��ٶ�

//        // �ж�AI����
//        if (Dir.x > 0.01f)
//        {
//            transform.localScale = new Vector3(-1, 1, 1);
//        }
//        else if (Dir.x < -0.01f)
//        {
//            transform.localScale = new Vector3(1, 1, 1);
//        }

//        rb.AddForce(Force);
//        // �ǵ�Ҫ��AI����һ������ 

//        float distance = Vector2.Distance(rb.position, path.vectorPath[currentNode]);// ��������¸��ڵ�ľ���
//        if (distance < nextNodeDistance)
//        {
//            currentNode++;
//        }

//    }

//}
