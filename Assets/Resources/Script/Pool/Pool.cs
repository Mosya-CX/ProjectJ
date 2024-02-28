using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Pool", menuName = "Pool")]
public class Pool : ComponentPoolSO
{
    public Factory factory;
    public override IFactory Factory
    {
        get
        {
            return factory;
        }
        set
        {
            factory = value as Factory;
        }
    }

}
