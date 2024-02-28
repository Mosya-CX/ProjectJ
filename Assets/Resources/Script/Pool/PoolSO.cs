using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class PoolSO : ScriptableObject,IPool
{
    //池子本体
    protected Stack<GameObject> Available = new();
    //这里使用工厂的接口作父类,到时池子的使用者设置这个字段的get和set
    public abstract IFactory Factory { get; set; }
    protected bool HasBeenPrewarmed { get; set; }
    public int initSize;
    protected virtual GameObject Create()
    {
        GameObject temp= Factory.Create();
        Available.Push(temp);
        return temp;
    }

    public virtual void Prewarm()
    {
        if (HasBeenPrewarmed)
        {
            Debug.LogWarning($"Pool {name} has already been prewarmed.");
            return;
        }
        for (int i = 0; i < initSize; i++)
        {
            Create();
        }
        HasBeenPrewarmed = true;
    }

    public virtual GameObject Request()
    {
        return Available.Count > 0 ? Available.Pop() : Create();
    }


    public virtual void Return(GameObject member)
    {
        Available.Push(member);
    }

    public virtual void OnDisable()
    {
        Available.Clear();
        HasBeenPrewarmed = false;
    }
}
