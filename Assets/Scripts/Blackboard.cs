using UnityEngine;

public class Blackboard
{
    private static Blackboard instance;
    public static Blackboard Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Blackboard();
            }
            return instance;
        }
    }

    //PROPERTIES
    public Transform[] TilesPosition { get; set; }
    public GameManager GameManager { get; set; }
    public TileSpriteHandler TileSpriteHandler { get; set; }
    public LevelObjective LevelObjective { get; set; }
}
