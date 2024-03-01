using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]
public abstract class ComponentPoolSO : PoolSO
{
    //���ӵ�ʹ����
    private Transform parent;
    public string poolName;
    //���ӱ���
    private Transform poolRoot;
    private Transform PoolRoot
    {
        get
        {
            if (poolRoot == null)
            {
                //���ﴴ����һ�������壬���һ��������transform���������ĸ��������ó���parent��˳��������ʹ��������
                poolRoot = new GameObject(poolName).transform;
                poolRoot.SetParent(parent);
            }
            return poolRoot;
        }
    }
    //��ʹ�������ĺ���
    public void SetParent(Transform t)
    {
        Debug.Log(poolName);
        parent = t;
        PoolRoot.SetParent(parent);    
    }
    //��������������ø������ҹر�
    protected override GameObject Create()
    {
        GameObject newMember = base.Create();
        newMember.transform.SetParent(PoolRoot);
        newMember.SetActive(false);
        return newMember;
    }
    public override GameObject Request()
    {
        GameObject member = base.Request();
        member.SetActive(true);
        return member;
    }
    public GameObject Request(Transform transform)
    {
        GameObject member = base.Request();
        member.transform.position= transform.position;
        member.SetActive(true);
        return member;
    }
    public override void Return(GameObject member)
    {
        //member.transform.SetParent(PoolRoot);
        base.Return(member);
        member.SetActive(false);
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (poolRoot != null)
        {
#if UNITY_EDITOR
            DestroyImmediate(poolRoot.gameObject);
#else
				Destroy(poolRoot.gameObject);
#endif
        }
    }
}

