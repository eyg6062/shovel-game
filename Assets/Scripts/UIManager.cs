using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject canvasPf;
    private GameObject canvasObject;

    private UnitInfoBox unitInfoBox;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        canvasObject = Instantiate(canvasPf);

        GameObject unitInfoObject = GameObject.FindWithTag("UnitInfoBox");
        unitInfoBox = unitInfoObject.GetComponent<UnitInfoBox>();

        unitInfoBox.Initialize();
        unitInfoBox.HideInfo();
    }

    public void DisplayUnitInfo(Unit unit)
    {
        unitInfoBox.DisplayInfo(unit);
    }

    public void HideUnitInfo()
    {
        unitInfoBox.HideInfo();
    }

}
