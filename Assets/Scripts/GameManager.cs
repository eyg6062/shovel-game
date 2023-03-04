using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject mapPf;

    void Awake()
    {
        // instantiates whole map
        Instantiate(mapPf);

    }
}
