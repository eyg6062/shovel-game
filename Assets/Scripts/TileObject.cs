using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    [SerializeField]protected string objName;
    [SerializeField] protected int totalHP;

    private int HP;

    protected Vector2 pos;
    protected bool falls;

    private void Start()
    {
        Initialize();
    }

    virtual public void Initialize()
    {
        HP = totalHP;
    }

    public Vector2 GetPos()
    {
        return transform.position; 
    }

    public bool Falls() 
    {
        return falls;
    }

    public void SetPos()
    {

    }
}
