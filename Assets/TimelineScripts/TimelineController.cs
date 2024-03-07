using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineController : MonoBehaviour
{

    public List<PlayableDirector> playableDirectors;
    public List<TimelineAsset> timelineAssets;
    public void Play()
    {

        foreach (var director in playableDirectors) { director.Play(); }
        Debug.Log("Play");

    }
    public void Stop() {
        foreach (var director in playableDirectors) { director.Stop(); }

        Debug.Log("Stop");


    }

    public void PlayFromIndex(int index)
    {

        Debug.Log( "playable asset "+index);

        if (index < timelineAssets.Count)
        {
            playableDirectors[0].playableAsset = timelineAssets[index];
        }
        else
        {

            playableDirectors[0].playableAsset = timelineAssets[timelineAssets.Count - 1];
        }

    }
}
