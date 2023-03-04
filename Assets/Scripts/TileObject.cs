using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    [SerializeField] protected string objName;
    [SerializeField] protected int totalHP;
    [SerializeField] protected bool falls;

    private int HP;

    protected Vector2 pos;

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

    public void UpdatePos(Vector2 dest)
    {
        transform.position = dest;
    }

    public int GetHP()
    {
        return HP;
    }

    public void AdjustHP(int amt)
    {
        HP = HP - amt;
        if (HP < 0)
        {
            HP = 0;
        }
    }

    public bool IsDead()
    {
        return (HP <= 0);
    }
}
