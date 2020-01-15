using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugging : MonoBehaviour
{
    public void ResetLevel()
    {
        PlayerPrefs.SetInt("HighestLevel", 0);   
    }

    public void MaxLevel()
    {
        PlayerPrefs.SetInt("HighestLevel", 8);
    }
}
