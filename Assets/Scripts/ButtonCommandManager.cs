using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCommandManager : MonoBehaviour
{
    public void EndBlueTurn()
    {
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        Controller controller = gameManager.GetComponent<Controller>();

        controller.BlueEndTurn();
    }

    public void EndRedTurn()
    {
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        Controller controller = gameManager.GetComponent<Controller>();

        controller.RedEndTurn();
    }
}
