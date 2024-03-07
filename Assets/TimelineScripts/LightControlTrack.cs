using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackBindingType(typeof(Light))]
[TrackClipType(typeof(LightControlAsset))]
public class LightControlTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {

        return ScriptPlayable<LightControlMixer>.Create(graph, inputCount);
    }
}
