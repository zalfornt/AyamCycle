using UnityEngine;

[CreateAssetMenu(fileName = "MusicLibrary", menuName = "ScriptableObjects/Muisc Library", order = 2)]
public class MusicLibrary : ScriptableObject
{
    public AudioClip[] audios;
}
