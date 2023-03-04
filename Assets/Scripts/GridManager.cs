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

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        // get map
        map = GameObject.FindWithTag("Map");

        objectArray = new List<List<TileObject>>();

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
            }
        }
    }


    private bool isOccupied(Vector2 dest)
    {
        int x = (int)dest.x;
        int y = (int)dest.y;
        if (x < 0 || y < 0)
        {
            return true;
        }

        if (objectArray[x][y] != null)
        {
            return true;
        }
        return false;
    }

    public void UnitWalk(Unit unit, Vector2 dir)
    {
        int cost = 1;

        Vector2 pos = unit.GetPos();
        Vector2 dest = pos + dir;

        if (isOccupied(dest))
        {
            return;
        }

        if (TryUnitAct(unit, cost))
        {
            MoveObj(unit, dest);
            ObjectFall(unit);
        }
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
                if (downObj.GetType() == typeof(Unit))
                {
                    DestroyObj(downObj);
                }
                else
                {
                    hitFloor = true;
                    continue;
                }
            } 
            
            height++;
            dest = down;
        }
        MoveObj(obj, dest);
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

        TileObject obj = GetObj(dest);
        if (obj == null)
        {
            return;
        }

        if (!obj.IsAttackable())
        {
            return;
        }

        if (TryUnitAct(unit, cost))
        {
            obj.AdjustHP(cost);
        }

        Debug.Log("attacked at " + dest + ". Remaining HP is " + obj.GetHP());

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
        Destroy(obj.gameObject);
    }

    // return true if unit has enough points to do action, spends the points
    private bool TryUnitAct(Unit unit, int cost)
    {
        if (unit.GetActPts() - cost < 0)
        {
            //
            Debug.Log("not enough points");
            return false;
        }
        else
        {
            unit.SpendActPts(cost);
            return true;
        }
        
    }

}
