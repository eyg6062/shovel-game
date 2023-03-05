using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoBox : MonoBehaviour
{
    private Text hp;
    private Text ap;

    public void Initialize()
    {
        GameObject hpText = transform.Find("HP").gameObject;
        hp = hpText.GetComponent<Text>();

        GameObject apText = transform.Find("AP").gameObject;
        ap = apText.GetComponent<Text>();
    }

    public void DisplayInfo(Unit unit)
    {
        hp.text = "HP: " + unit.GetHP();
        ap.text = "AP: " + unit.GetAP();

        gameObject.SetActive(true);

    }

    public void HideInfo()
    {
        gameObject.SetActive(false);
    }
}
