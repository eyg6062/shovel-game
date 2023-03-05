using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMapScene : MonoBehaviour
{
    public void ChangeSceneMap1()
    {
        SceneManager.LoadScene(2);
    }

    public void ChangeSceneMap2()
    {
        SceneManager.LoadScene(3);
    }

    public void ChangeSceneMap3()
    {
        SceneManager.LoadScene(4);
    }
}
