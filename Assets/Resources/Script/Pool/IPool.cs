using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPool
{
    void Prewarm();
    GameObject Request();
    void Return(GameObject member);
}
