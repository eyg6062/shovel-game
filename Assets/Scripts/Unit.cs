using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : TileObject
{
    [SerializeField]protected int totalActPoints;

    private int actPoints;
    private Unit carriedUnit;

    private void Start()
    {
        Initialize();
    }

    override public void Initialize()
    {
        base.Initialize();
        actPoints = totalActPoints;
        falls = true;
        carriedUnit = null;
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

        Debug.Log(GetPos() + " has " + actPoints + " action points left");
    }

    public void SetCarriedUnit(Unit carriedUnit)
    {
        this.carriedUnit = carriedUnit;
    }

    public Unit GetCarriedUnit()
    {
        return carriedUnit;
    }

    public bool IsCarrying()
    {
        return !(carriedUnit == null);
    }

}
