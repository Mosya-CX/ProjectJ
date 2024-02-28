using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class FactorySO : ScriptableObject,IFactory
{
    public abstract GameObject Create();
    public abstract GameObject GetPrefab();
}
