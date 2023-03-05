using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCommandManager : MonoBehaviour
{
    public void EndBlueTurn()
    {
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        Controller controller = gameManager.GetComponent<Controller>();
        UIManager uiManager = gameManager.GetComponent<UIManager>();

        controller.BlueEndTurn();
        uiManager.ShowRedButton();
    }

    public void EndRedTurn()
    {
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        Controller controller = gameManager.GetComponent<Controller>();
        UIManager uiManager = gameManager.GetComponent<UIManager>();

        controller.RedEndTurn();
        uiManager.ShowBlueButton();
    }

    public void ToggleTutorial()
    {
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        UIManager uiManager = gameManager.GetComponent<UIManager>();
        uiManager.ToggleTutorialCanvas();
    }
}
