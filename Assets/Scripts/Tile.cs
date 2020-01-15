using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Gender { MALE, FEMALE };

    public SpriteRenderer spr;
    public Animator anim;

    private Tile combinedTile;
    private Vector3 currPos;
    private Vector3 newPos;
    private int moveSpeed;

    public int stage;
    public Gender gender;
    public bool combineable;
    public bool moving;
    public bool moveAndCombine;
    public bool isX2; //isTimesTwo

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 35;
        //spr = GetComponent<SpriteRenderer>();
        isX2 = false;
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
            transform.position = Vector3.MoveTowards(transform.position, newPos, moveSpeed * Time.deltaTime);
        }
        else
        {
            if (moveAndCombine)
            {
                combinedTile.TriggerNextStage();
                //Blackboard.Instance.GameManager.movingTiles--;
                moveAndCombine = false;
                Destroy(this.gameObject);
            }
            if (moving)
            {
                //Blackboard.Instance.GameManager.movingTiles--;
                moving = false;
            }
            //Blackboard.Instance.GameManager.ReadyToMove = true;
        }
    }

    public void SetNewDestination(int oldX, int oldY, int newX, int newY)
    {
        if (!moving && !moveAndCombine)
        {
            moving = true;
            //Blackboard.Instance.GameManager.movingTiles++;
            Blackboard.Instance.GameManager.movingTiles.Add(this);
            Blackboard.Instance.GameManager.grid[oldX, oldY] = null;
            newPos = GridController.FindTilePos(newX, newY);
            Blackboard.Instance.GameManager.grid[newX, newY] = this;
        }
    }

    public void SetNewDestination(int oldX, int oldY, int newX, int newY, bool combine, Tile _combinedTile)
    {
        if (!moving && !moveAndCombine)
        {
            moveAndCombine = true;
            //Blackboard.Instance.GameManager.movingTiles++;
            Blackboard.Instance.GameManager.movingTiles.Add(this);
            Blackboard.Instance.GameManager.grid[oldX, oldY] = null;
            newPos = GridController.FindTilePos(newX, newY);
            combinedTile = _combinedTile;
            if (isX2 || combinedTile.isX2) combinedTile.isX2 = true;
            if (combinedTile.stage == 4)
            {
                combinedTile.stage = 0;
                if (!combinedTile.isX2)
                {
                    float rand = Random.Range(0f, 1.0f);
                    if (rand < 0.8f) combinedTile.isX2 = true;
                }
            }
            else
            {
                combinedTile.stage += 1;
            }
            combinedTile.combineable = false;
        }
    }

    public void TriggerNextStage()
    {
        Blackboard.Instance.TileSpriteHandler.SetNewSprite(stage, gender, spr, isX2);
        Blackboard.Instance.LevelObjective.AddToObjectives(stage, gender, isX2);
        //Debug.Log("combined");
        anim.SetTrigger("combined");
    }
}