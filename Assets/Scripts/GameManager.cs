using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected GameObject mapPf;

    void Awake()
    {
        // instantiates whole map
        Instantiate(mapPf);

    }
}
