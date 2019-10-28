using UnityEngine;

public class GridController : MonoBehaviour
{
    public Transform[] tiles;

    private void Awake()
    {
        Blackboard.Instance.TilesPosition = this.tiles;
    }

    public static Vector3 FindTilePos(int x, int y)
    {
        int i = x + (y * 4);
        return Blackboard.Instance.TilesPosition[i].position;
    }
}