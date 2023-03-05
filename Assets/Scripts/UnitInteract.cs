using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInteract : Tile
{
    private Unit unit;

    private Controller controller;
    private UnitInfoBox unitInfoBox;
    private UIManager uiManager;

    private void Start()
    {
        Initialize();
    }

    override public void Initialize()
    {
        base.Initialize();
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        controller = gameManager.GetComponent<Controller>();

        uiManager = gameManager.GetComponent<UIManager>();

        unit = gameObject.GetComponent<Unit>();

    }

    private void OnMouseDown()
    {
        controller.ClickedUnit(unit);
    }

    private void OnMouseEnter()
    {
        uiManager.DisplayUnitInfo(unit);
    }

    private void OnMouseExit()
    {
        uiManager.HideUnitInfo();
    }
}
