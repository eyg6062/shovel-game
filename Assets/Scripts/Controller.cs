using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Unit activeUnit;
    private Faction activeFaction;

    [SerializeField] protected GameObject unitControllerPf;
    private GameObject unitController;

    [SerializeField] private GameObject interactTilesPf;
    private GameObject interactTiles;

    public event EventHandler<OnEndTurnArgs> OnEndTurn;

    public class OnEndTurnArgs : EventArgs { public Faction faction; }

    public void ClickedUnit(Unit unit)
    {
        if (unit.GetFaction() != activeFaction && activeUnit != null)
        {
            ThrowToUnitController(unit);
        }
        else if (unit.GetFaction() != activeFaction )
        {
            return;
        }
        // deactivate unit if clicked on same unit
        else if (ReferenceEquals(activeUnit, unit))
        {
            activeUnit = null;

            Destroy(unitController);
            Destroy(interactTiles); 
        } 
        // activate unit if no other are active
        else if (activeUnit == null)
        {
            activeUnit = unit;

            unitController = Instantiate(unitControllerPf);
            UnitController controllerScript = unitController.GetComponent<UnitController>();
            controllerScript.SetUnit(activeUnit);

            interactTiles = Instantiate(interactTilesPf, activeUnit.GetPos(), Quaternion.identity);

            // sticks objects onto unit
            interactTiles.transform.parent = activeUnit.gameObject.transform;
            unitController.transform.parent = activeUnit.gameObject.transform;
        }
        // redirect to unit controller
        else
        {
            ThrowToUnitController(unit);
        }
        
    }

    // temporary, to avoid overlapping click
    private void ThrowToUnitController(Unit unit)
    {
        UnitController controllerScript = unitController.GetComponent<UnitController>();
        controllerScript.ClickedAtkTile(unit.GetPos());
    }

    public void BlueEndTurn()
    {
        if (activeUnit != null)
        {
            return;
        }

        if (activeFaction == Faction.RED)
        {
            return;
        }

        activeFaction = Faction.RED;

        OnEndTurn?.Invoke(this, new OnEndTurnArgs {faction = Faction.BLUE});
    }

    public void RedEndTurn()
    {
        if (activeUnit != null)
        {
            return;
        }

        if (activeFaction == Faction.BLUE)
        {
            return;
        }

        activeFaction = Faction.BLUE;

        OnEndTurn?.Invoke(this, new OnEndTurnArgs { faction = Faction.RED });

        
    }
}
