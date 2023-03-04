﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInteract : Tile
{
    private Controller controller;

    private void Start()
    {
        Initialize();
    }

    override public void Initialize()
    {
        base.Initialize();
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        controller = gameManager.GetComponent<Controller>();

    }

    private void OnMouseDown()
    {
        controller.ClickedAtkTile(GetPos());
    }
}
