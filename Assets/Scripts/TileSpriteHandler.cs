using UnityEngine;
using UnityEngine.UI;

public class TileSpriteHandler : MonoBehaviour
{
    public Sprite[] chickenStagesSprites;


    private void Awake()
    {
        Blackboard.Instance.TileSpriteHandler = this;
    }

    public void SetNewSprite(int stage, Tile.Gender gender, SpriteRenderer spr, bool isTimesTwo)
    {
        switch (stage)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                spr.sprite = chickenStagesSprites[stage];
                break;
            case 4:
                if (gender == Tile.Gender.MALE)
                {
                    spr.sprite = chickenStagesSprites[4];
                }
                else if (gender == Tile.Gender.FEMALE)
                {
                    spr.sprite = chickenStagesSprites[5];
                }
                break;
        }
        if (isTimesTwo)
        {
            spr.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            spr.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void SetNewTargetSprite(int stage, Image img)
    {
        img.sprite = chickenStagesSprites[stage];
    }
}
