using UnityEngine;

public class LevelLibrary : MonoBehaviour
{
    public Objective[] levels;

    // Start is called before the first frame update
    void Awake()
    {
        Blackboard.Instance.LevelLibrary = this;
    }
}
