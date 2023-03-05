using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.UI.CanvasScaler;

public class GridManager : MonoBehaviour
{
    private GameObject map;
    private List<List<TileObject>> objectArray;
    private List<Unit> unitList;

    private UIManager uiManager;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        // get map
        map = GameObject.FindWithTag("Map");

        objectArray = new List<List<TileObject>>();
        unitList = new List<Unit>();

        // fill object list with null values (100 x 100 for now), maybe fix later
        for(int i = 0; i < 100; i++)
        {
            List<TileObject> yList = new List<TileObject>();
            for (int j = 0; j < 100; j++)
            {
                yList.Add(null);
            }
            objectArray.Add(yList);
        }

        foreach(Transform child in map.transform)
        {
            // adds TileObjects to lists
            if (child.tag == "TileObject" || child.tag == "Unit")
            {
                Vector2 pos = child.position;
                int x = (int)pos.x;
                int y = (int)pos.y;
                objectArray[x][y] = child.GetComponent<TileObject>();

                // adds units to unit list
                if (child.tag == "Unit")
                {
                    unitList.Add(child.GetComponent<Unit>());
                }
            }
        }

        // get UIManager
        uiManager = gameObject.GetComponent<UIManager>();
    }


    private bool isOccupied(Vector2 dest)
    {
        int x = (int)dest.x;
        int y = (int)dest.y;
        if (x < 0 || y < 0)
        {
            return false;
        }

        if (objectArray[x][y] != null)
        {
            return true;
        }
        return false;
    }

    public void UnitClimb(Unit unit, Vector2 dir)
    {
        int cost = 1;
        int height = 0;

        // sets carried units if they exist
        SetCarriedUnits(unit);

        Vector2 pos = unit.GetPos();
        Vector2 dest = pos + dir;

        bool climbing = true;

        while (climbing)
        {
            if (isOccupied(dest))
            {
                height++;
                dest = dest + Vector2.up;
            } else
            {
                climbing = false;
            }
        }

        Vector2 above = unit.GetPos() + Vector2.up;
        for (int i = 0; i < height; i++)
        {
            if (isOccupied(above))
            {
                return;
            } 
            above = above + Vector2.up;
        }

        // returns if carrying and needs to climb
        if (unit.IsCarrying() && cost > 1) 
        {
            uiManager.DisplayError("Can't climb while carrying");
            return;
        }

        if (TryUnitAct(unit, cost + height))
        {
            MoveObj(unit, dest);
            ObjectFall(unit);

            // moves carried units
            MountedMove(unit.GetCarriedUnit(), dir);
        }

    }

    private void SetCarriedUnits(Unit unit)
    {
        Vector2 dest = unit.GetPos() + Vector2.up;
        if (isOccupied(dest))
        {
            TileObject obj = GetObj(dest);
            if (obj.GetType() == typeof(Unit))
            {
                Unit upUnit = (Unit)obj;
                unit.SetCarriedUnit(upUnit);

                SetCarriedUnits(upUnit);
                return;
            }
        }
        unit.SetCarriedUnit(null);
    }
    
    private void MountedMove(Unit unit, Vector2 dir)
    {
        if (unit == null)
        {
            return;
        }

        Vector2 pos = unit.GetPos();
        Vector2 dest = pos + dir;

        if (!isOccupied(dest))
        {
            MoveObj(unit, dest);
        }

        ObjectFall(unit);

        MountedMove(unit.GetCarriedUnit(), dir);
    }


    public void MoveObj(TileObject obj, Vector2 dest)
    {
        Vector2 pos = obj.GetPos();

        obj.UpdatePos(dest);

        SetCoord(pos, null);
        SetCoord(dest, obj);
    }

    private void SetCoord(Vector2 pos, TileObject tileObject)
    {
        int x = (int)pos.x;
        int y = (int)pos.y;
        objectArray[x][y] = tileObject;
    }

    private TileObject GetObj(Vector2 pos)
    {
        int x = (int)pos.x;
        int y = (int)pos.y;
        return objectArray[x][y];
    }

    private void ObjectFall(TileObject obj)
    {
        if (!obj.Falls())
        {
            return;
        }

        int height = 0;
        bool hitFloor = false;
        Vector2 dest = obj.GetPos();
        while (!hitFloor)
        {
            Vector2 down = dest + Vector2.down;
            if (isOccupied(down))
            {
                TileObject downObj = GetObj(down);
                if (obj.GetType() == typeof(BlockTile) && downObj.GetType() == typeof(Unit))
                {
                    DestroyObj(downObj);
                }
                else
                {
                    FallDamage(obj, height);
                    FallDamage(downObj, height);
                    hitFloor = true;
                    continue;
                }
            } 
            
            height++;
            dest = down;
        }
        MoveObj(obj, dest);
    }

    private void FallDamage(TileObject unit, int height)
    {
        if ( unit.GetType() != typeof(Unit))
        {
            return;
        }

        int offset = 2;

        int amt = (height - offset);
        if (amt <= 0)
        {
            return;
        }

        unit.AdjustHP(amt);

        CheckDead(unit);
    }

    private void ColumnFall(int x)
    {
        List<TileObject> yList = objectArray[x];
        for (int y = 0; y < yList.Count; y++)
        {
            Vector2 pos = new Vector2(x, y);
            TileObject obj = GetObj(pos);
            if (obj != null)
            {
                ObjectFall(obj);
            }
        }
    }

    public void AttackObj(Unit unit, Vector2 dest)
    {
        int cost = 1;

        SetCarriedUnits(unit);

        /*
        if (unit.IsCarrying())
        {
            Debug.Log("can't attack while carrying");
            return;
        }
        */

        TileObject obj = GetObj(dest);
        if (obj == null)
        {
            Debug.Log("no object");
            return;
        }

        if (!obj.IsAttackable())
        {
            Debug.Log("not attackable");
            return;
        }

        if (TryUnitAct(unit, cost))
        {
            obj.AdjustHP(cost);
        }

        if (obj.GetType() == typeof(BlockTile))
        {
            BlockTile blockTile = (BlockTile)obj;
            blockTile.CheckHP();
        }

        CheckDead(obj);
    }

    private void CheckDead(TileObject obj)
    {
        // check if dead
        if (obj.IsDead())
        {
            DestroyObjAndFall(obj);
        }
    }

    private void DestroyObjAndFall(TileObject obj)
    {
        Vector2 pos = obj.GetPos();
        DestroyObj(obj);

        // falls column 
        ColumnFall((int)pos.x);
    }

    private void DestroyObj(TileObject obj)
    {
        Vector2 pos = obj.GetPos();
        SetCoord(pos, null);
        
        // scuffed, goes through list and makes dead unit null
        for (int i = 0; i < unitList.Count; i++)
        {
            if (unitList[i] == obj)
            {
                unitList[i] = null;
            }
        }

        Destroy(obj.gameObject);

        CheckWin();

    }

    private void CheckWin()
    {
        int blueCount = 0;
        int redCount = 0;
        foreach (Unit unit in unitList) {
            if (unit == null)
            {
                Debug.Log("dead unit is null");
                continue;
            }
            if (unit.GetFaction() == Faction.BLUE)
            {
                blueCount++;
            } 
            else if (unit.GetFaction() == Faction.RED)
            {
                redCount++;
            }
        }

        Debug.Log("red:" + redCount + " blue:" + blueCount);
        if (blueCount > 0 && redCount == 0)
        {
            // blue wins
            uiManager.DisplayWinCanvas("blue");
            Debug.Log("blue wins");
        } 
        else if (redCount > 0 && blueCount == 0)
        {
            // red wins
            uiManager.DisplayWinCanvas("red");
            Debug.Log("red wins");
        } 
        else if (blueCount == 0 && redCount == 0)
        {
            // tie
            uiManager.DisplayWinCanvas("tie");
            Debug.Log("tie");
        }
    }

    // return true if unit has enough points to do action, spends the points
    private bool TryUnitAct(Unit unit, int cost)
    {
        if (unit.GetAP() - cost < 0)
        {
            //
            Debug.Log("Not enough AP (" + cost + " needed)");
            uiManager.DisplayError("Not enough AP (" + cost + " needed)");
            return false;
        }
        else
        {
            unit.SpendActPts(cost);
            return true;
        }
        
    }

}
