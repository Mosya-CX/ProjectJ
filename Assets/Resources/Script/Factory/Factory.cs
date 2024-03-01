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
        instantiatedObject.SetActive(false); // ʵ�������������ã���ֹOnEnable������
        return instantiatedObject;
    }

    public override GameObject GetPrefab()
    {
        return prefab;
    }


}
