using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : TileObject
{
    [SerializeField]protected int totalActPoints;

    private int actPoints;

    private void Start()
    {
        Initialize();
    }

    override public void Initialize()
    {
        base.Initialize();
        actPoints = totalActPoints;
        falls = true;
    }

    public int GetActPts()
    {
        return actPoints;
    }

    public void SpendActPts(int amt)
    {
        actPoints -= amt;
        if (actPoints < 0)
        {
            actPoints = 0;
        }
    }

}
