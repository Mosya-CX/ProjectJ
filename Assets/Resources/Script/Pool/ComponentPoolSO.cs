using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]
public abstract class ComponentPoolSO : PoolSO
{
    //池子的使用者
    private Transform parent;
    public string poolName;
    //池子本体
    private Transform poolRoot;
    private Transform PoolRoot
    {
        get
        {
            if (poolRoot == null)
            {
                //这里创建了一个空物体，并且获得了它的transform，并把它的父对象设置成了parent，顺利挂在了使用者下面
                poolRoot = new GameObject(poolName).transform;
                poolRoot.SetParent(parent);
            }
            return poolRoot;
        }
    }
    //给使用者留的函数
    public void SetParent(Transform t)
    {
        Debug.Log(poolName);
        parent = t;
        PoolRoot.SetParent(parent);    
    }
    //建立出物体后，设置父对象并且关闭
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

