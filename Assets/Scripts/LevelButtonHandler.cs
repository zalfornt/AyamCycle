using UnityEngine;
using UnityEngine.UI;

public class LevelButtonHandler : MonoBehaviour
{
    public GameObject[] levelButtons;
    public Sprite unlocked;
    public Sprite finished;

    // Start is called before the first frame update
    void Start()
    {
        //Latest Level start from 1, not like indexes
        int ll = PlayerPrefs.GetInt("HighestLevel");
        if(ll > 0)
        {
            for (int i = 0; i <= ll; i++)
            {
                levelButtons[i].GetComponent<Button>().interactable = true;
                levelButtons[i].GetComponent<Image>().sprite = finished;
                if(i == ll)levelButtons[i].GetComponent<Image>().sprite = unlocked;
            }
        }
    }
}
