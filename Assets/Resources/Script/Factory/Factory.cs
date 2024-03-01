using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Factory", menuName = "Factory")]
public class Factory : FactorySO
{
    public GameObject prefab = default;
    public override GameObject Create()
    {
        GameObject instantiatedObject = Instantiate(prefab);
        instantiatedObject.SetActive(false); // 实例化后立即禁用，防止OnEnable被调用
        return instantiatedObject;
    }

    public override GameObject GetPrefab()
    {
        return prefab;
    }


}
