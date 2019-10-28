using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpriteHandler : MonoBehaviour
{
    public Sprite[] chickenStagesSprites; 


    private void Awake()
    {
        Blackboard.Instance.TileSpriteHandler = this;
    }

    public void SetNewSprite(int stage, Tile.Gender gender, SpriteRenderer spr)
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
                if(gender == Tile.Gender.MALE)
                {
                    spr.sprite = chickenStagesSprites[4];
                }
                else if (gender == Tile.Gender.FEMALE)
                {
                    spr.sprite = chickenStagesSprites[5];
                }
                break;
        }
    }
}
