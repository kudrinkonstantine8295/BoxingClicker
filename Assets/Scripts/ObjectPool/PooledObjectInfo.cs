using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObjectInfo
{
    public string LookingString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}
