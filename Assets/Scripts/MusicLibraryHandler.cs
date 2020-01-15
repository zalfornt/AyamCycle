using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLibraryHandler : MonoBehaviour
{
    public MusicLibrary library;

    private void Awake()
    {
        library = MusicPlayer.Instance.library;
    }

    /// <summary>
    /// 0: Menu Song
    /// 1: Menu Song 2 (scrapped)
    /// </summary>
    /// <param name="index">Put the number exaclty like listed in summary</param>
    public void PlaySongByIndex(int index)
    {
        MusicPlayer.PlayAudio(library.audios[index]);
    }
}
