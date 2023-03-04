using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInteract : Tile
{
    private UnitController unitController;

    private void Start()
    {
        Initialize();
    }

    override public void Initialize()
    {
        base.Initialize();
        GameObject unitControllerObj = GameObject.FindWithTag("UnitController");
        unitController = unitControllerObj.GetComponent<UnitController>();

    }

    private void OnMouseDown()
    {
        unitController.ClickedAtkTile(GetPos());
    }
}
