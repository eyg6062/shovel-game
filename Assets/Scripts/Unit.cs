using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : TileObject
{
    [SerializeField] protected int totalActPoints;
    [SerializeField] protected Faction faction;

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

    public int GetAP()
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

    public Faction GetFaction()
    {
        return faction;
    }

    public void RefreshActPts()
    {
        actPoints = totalActPoints;
    }

}
