using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (child.tag == "TileObject")
            {
                Vector2 pos = child.position;
                int x = (int)pos.x;
                int y = (int)pos.y;
                objectArray[x][y] = child.GetComponent<TileObject>();
            }
        }

    }

    private void MoveUnit(Unit unit)
    {
        UnitFall(unit);
    }

    private void UnitFall(Unit unit)
    {
        if (!unit.Falls())
        {
            return;
        }

    }
}
