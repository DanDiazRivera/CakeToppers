using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FruitArrangment : MonoBehaviour
{
    public Vector3[] positions1 => items1.Select(x => x.position).ToArray();
    public Vector3[] positions2 => items2.Select(x => x.position).ToArray();

    public bool hasTwo => positions2.Length > 0;

    public int positionCount1 => items1.Length;
    public int positionCount2 => items2.Length;

    [SerializeField] Transform[] items1;
    [SerializeField] Transform[] items2;

}
