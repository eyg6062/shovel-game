using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEventHandler : MonoBehaviour
{
    private Controller controller;
    private Unit unit;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        unit = gameObject.GetComponent<Unit>();

        GameObject gameManager = GameObject.FindWithTag("GameManager");
        controller = gameManager.GetComponent<Controller>();

        controller.OnEndTurn += RefreshUnit;
    }

    private void RefreshUnit(object sender, Controller.OnEndTurnArgs e)
    {
        if (e.faction == unit.GetFaction())
        {
            unit.RefreshActPts();
        }
    }
}
