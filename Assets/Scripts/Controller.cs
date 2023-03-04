using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Unit activeUnit;

    [SerializeField] protected GameObject unitControllerPf;
    private GameObject unitController;

    [SerializeField] private GameObject interactTilesPf;
    private GameObject interactTiles;

    public void ClickedUnit(Unit unit)
    {
        // deactivate unit if clicked on same unit
        if (ReferenceEquals(activeUnit, unit))
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
        
    }

}
