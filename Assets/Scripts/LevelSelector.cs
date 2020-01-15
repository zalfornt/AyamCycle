using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public Objective obj;
    public int objCount;

    public void LoadLevel()
    {
        Blackboard.Instance.Objective = obj;
        if (objCount == 1)
        {
            SceneManager.LoadScene("Level-1O");
        }
        else if (objCount == 2)
        {
            SceneManager.LoadScene("Level-2O");
        }
        else if (objCount == 3)
        {
            SceneManager.LoadScene("Level-3O");
        }
        else if (objCount == 4)
        {
            SceneManager.LoadScene("Endless");
        }
    }
}
