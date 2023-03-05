using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTile : TileObject
{
    private GameObject crack;

    private void Start()
    {
        Initialize();
    }

    override public void Initialize()
    {
        base.Initialize();
        crack = transform.Find("CrackedSquare").gameObject;
        crack.SetActive(false);
    }

    public void CheckHP()
    {
        if (GetHP() <= 1)
        {
            crack.SetActive(true);
        }
    }



}
