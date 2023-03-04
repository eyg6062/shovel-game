using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    protected Vector2 pos;

    private void Start()
    {
        Initialize();
    }

    virtual public void Initialize()
    {
        pos = GetPos();
    }

    public Vector2 GetPos()
    {
        return transform.position;
    }
}
