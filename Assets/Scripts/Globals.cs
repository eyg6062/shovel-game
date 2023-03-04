using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals
{
    // todo: change slightly
    static public Dictionary<Vector2, T> GetChildren<T>(string groupName, GameObject gameObj)
    {
        Dictionary<Vector2, T> tiles = new Dictionary<Vector2, T>();

        // gets grid child from map gameobject
        GameObject parent = gameObj.transform.Find(groupName).gameObject;

        // puts all the tiles from the map into the tiles dictionary
        foreach (Transform child in parent.transform)
        {
            Vector2 posV2 = child.gameObject.transform.position;
            T tile = child.gameObject.GetComponent<T>();
            tiles[posV2] = tile;
        }
        return tiles;
    }
}
