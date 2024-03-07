using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class LightControlAsset : PlayableAsset, ITimelineClipAsset
{
    public ExposedReference<Light> light;
    public Color color = Color.white;
    public float intensity = 1f;
    public float bounceIntensity = 1f;
    public float range = 10f;
    public ClipCaps clipCaps { get { return ClipCaps.Blending; } }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<LightControlBehaviour>.Create(graph);

        var lightControlBehavior = playable.GetBehaviour();

        lightControlBehavior.light = light.Resolve(graph.GetResolver());
        lightControlBehavior.intensity = intensity;
        lightControlBehavior.light.bounceIntensity = bounceIntensity;
        lightControlBehavior.light.range = range;
        lightControlBehavior.color = color;

        return playable;

    }

}
