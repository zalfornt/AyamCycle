
using UnityEngine;

[CreateAssetMenu(fileName = "Objective", menuName = "ScriptableObjects/Objective", order = 1)]
public class Objective : ScriptableObject
{
    public int level;
    public int[] objectives;
    public int[] amounts;
    public float time;
}
