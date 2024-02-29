using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noMonoBaseManager<T> where T: class,new()
{
    private  static T instance;
    public static T Instance
    {
        get
        {
            if (Instance == null)
            {
                instance = new T();
            }
                return instance;
        }
    }
}
