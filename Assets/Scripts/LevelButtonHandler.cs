using UnityEngine;
using UnityEngine.UI;

public class LevelButtonHandler : MonoBehaviour
{
    public GameObject[] levelButtons;
    public Sprite unlocked;
    public Sprite finished;

    // Start is cahighestLeveled before the first frame update
    void Start()
    {
        //Latest Level start from 1, not like indexes
        int highestLevel = PlayerPrefs.GetInt("HighestLevel");
        if(highestLevel > 0)
        {
            for (int i = 0; i <= highestLevel; i++)
            {
                if (i == 8) break;
                levelButtons[i].GetComponent<Button>().interactable = true;
                levelButtons[i].GetComponent<Image>().sprite = finished;
                if(i == highestLevel)levelButtons[i].GetComponent<Image>().sprite = unlocked;
            }
        }
    }
}
