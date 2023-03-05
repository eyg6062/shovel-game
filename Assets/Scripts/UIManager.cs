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

    private GameObject redButton;
    private GameObject blueButton;

    [SerializeField] private GameObject winCanvasPf;
    private GameObject winCanvasObj;

    [SerializeField] private GameObject tutorialCanvasPf;
    private GameObject tutorialCanvasObj;

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

        redButton = canvasObject.transform.Find("EndRedTurnButton").gameObject;
        blueButton = canvasObject.transform.Find("EndBlueTurnButton").gameObject;
        redButton.SetActive(false);
    }

    public void ShowBlueButton()
    {
        redButton.SetActive(false);
        blueButton.SetActive(true);
    }

    public void ShowRedButton()
    {
        blueButton.SetActive(false);
        redButton.SetActive(true);
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

    public void DisplayWinCanvas(string factionStr)
    {
        if (winCanvasObj == null)
        {
            winCanvasObj = Instantiate(winCanvasPf);
        }

        GameObject redWinsObj = winCanvasObj.transform.Find("RedWins").gameObject;
        GameObject blueWinsObj = winCanvasObj.transform.Find("BlueWins").gameObject;
        GameObject tieObj = winCanvasObj.transform.Find("Tie").gameObject;

        if (factionStr == "blue")
        {
            redWinsObj.SetActive(false);
            tieObj.SetActive(false);
            blueWinsObj.SetActive(true);
        }
        else if (factionStr == "red")
        {
            blueWinsObj.SetActive(false);
            tieObj.SetActive(false);
            redWinsObj.SetActive(true);
        }
        else if (factionStr == "tie")
        {
            blueWinsObj.SetActive(false);
            redWinsObj.SetActive(false);
            tieObj.SetActive(true);
        }

    }

    public void ToggleTutorialCanvas()
    {
        if (tutorialCanvasObj == null)
        {
            tutorialCanvasObj = Instantiate(tutorialCanvasPf);
        } 
        else
        {
            Destroy(tutorialCanvasObj);
            tutorialCanvasObj = null;
        }
    }

}
