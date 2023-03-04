using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private GridManager gridManager;
    private Unit activeUnit;

    [SerializeField]private GameObject interactTilesPf;
    private GameObject interactTiles;

    private void Start()
    {
        gridManager = GetComponent<GridManager>();
        // sets active unit to that one existing unit, get rid of later
        activeUnit = GameObject.FindWithTag("Unit").GetComponent<Unit>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            gridManager.UnitWalk(activeUnit, Vector2.left);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            gridManager.UnitWalk(activeUnit, Vector2.right);
        }
    }

    public void ClickedAtkTile(Vector2 pos)
    {
        gridManager.AttackObj(activeUnit, pos);
    }

    public void ClickedUnit(Unit unit)
    {
        activeUnit = unit;
        interactTiles = Instantiate(interactTilesPf, unit.GetPos(), Quaternion.identity);
        interactTiles.transform.parent = unit.gameObject.transform;
       
    }
}
