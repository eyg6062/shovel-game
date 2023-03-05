using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject canvasPf;
    private GameObject canvasObject;

    private GameObject errorTextObj;

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

        errorTextObj = canvasObject.transform.Find("ErrorText").gameObject;

        errorTextObj.SetActive(false);
    }

    public void DisplayUnitInfo(Unit unit)
    {
        unitInfoBox.DisplayInfo(unit);
    }

    public void HideUnitInfo()
    {
        unitInfoBox.HideInfo();
    }

    public void DisplayError(string msg)
    {
        StartCoroutine(DisplayErrorWait(msg));
    }

    IEnumerator DisplayErrorWait(string msg)
    {
        errorTextObj.SetActive(true);
        Text text = errorTextObj.GetComponent<Text>();
        text.text = msg;

        yield return new WaitForSeconds(1);

        errorTextObj.SetActive(false);
    }

}
