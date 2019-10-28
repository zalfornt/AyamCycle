using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Gender { MALE, FEMALE };

    public SpriteRenderer spr;

    private Tile combinedTile;
    private Vector3 currPos;
    private Vector3 newPos;

    public int stage;
    public Gender gender;
    public bool combineable;

    private bool moving;
    private bool moveAndCombine;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //spr = GetComponent<SpriteRenderer>();
        combineable = true;
        moving = false;
        moveAndCombine = false;
        combinedTile = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != newPos && (moving || moveAndCombine))
        {
            Blackboard.Instance.GameManager.ReadyToMove = false;
            transform.position = Vector3.MoveTowards(transform.position, newPos, 100 * Time.deltaTime);
        }
        else
        {
            if (moveAndCombine)
            {
                combinedTile.TriggerNextStage();
                Destroy(this.gameObject);
            }
            Blackboard.Instance.GameManager.ReadyToMove = true;
            moving = false;
        }
    }

    public void SetNewDestination(int oldX, int oldY, int newX, int newY)
    {
        moving = true;
        Blackboard.Instance.GameManager.grid[oldX, oldY] = null;
        newPos = GridController.FindTilePos(newX, newY);
        Blackboard.Instance.GameManager.grid[newX, newY] = this;
    }

    public void SetNewDestination(int oldX, int oldY, int newX, int newY, bool combine, Tile _combinedTile)
    {
        moveAndCombine = true;
        Blackboard.Instance.GameManager.grid[oldX, oldY] = null;
        newPos = GridController.FindTilePos(newX, newY);
        combinedTile = _combinedTile;
    }


    public void TriggerNextStage()
    {
        if (stage == 4)
        {
            Blackboard.Instance.TileSpriteHandler.SetNewSprite(0, gender, spr);
            stage = 0;
        }
        else
        {
            Blackboard.Instance.TileSpriteHandler.SetNewSprite(stage + 1, gender, spr);
            stage += 1;
        }
        //Debug.Log("combined");
        //set vfx(?)
        //gives score
    }
}