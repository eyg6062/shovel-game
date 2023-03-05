using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private GridManager gridManager;
    private Unit unit;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        gridManager = gameManager.GetComponent<GridManager>();
    }

    public void SetUnit(Unit unit)
    {
        this.unit = unit;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            gridManager.UnitClimb(unit, Vector2.left);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            gridManager.UnitClimb(unit, Vector2.right);
        }
    }

    public void ClickedAtkTile(Vector2 pos)
    {
        Vector2 unitPos = unit.GetPos();
        if (pos == unitPos + Vector2.up ||
            pos == unitPos + Vector2.down ||
            pos == unitPos + Vector2.left ||
            pos == unitPos + Vector2.right)
        {
            gridManager.AttackObj(unit, pos);
        }
    }

    public Unit GetUnit()
    {
        return unit;
    }
}
