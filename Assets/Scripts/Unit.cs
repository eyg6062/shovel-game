using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : TileObject
{
    [SerializeField]private int totalActPoints;

    private int actPoints;

    private void Start()
    {
        Initialize();
    }

    override public void Initialize()
    {
        base.Initialize();
        actPoints = totalActPoints;
    }

}
