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

    Blackboard()
    {
        playCount = 0;
    }

    //PROPERTIES
    public Transform[] TilesPosition { get; set; }
    public GameManager GameManager { get; set; }
    public TileSpriteHandler TileSpriteHandler { get; set; }
    public LevelObjective LevelObjective { get; set; }
    public Objective Objective { get; set; }
    public LevelLibrary LevelLibrary { get; set; }
    public int playCount;
}
