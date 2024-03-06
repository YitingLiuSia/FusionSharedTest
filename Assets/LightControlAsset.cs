
using UnityEngine;
using UnityEngine.Playables;


public class LightControlAsset : PlayableAsset
{
    public ExposedReference<Light> light;
    public Color color = Color.white;
    public float intensity = 1f;


    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<LightControlBehaviour>.Create(graph);

        var lightControlBehavior = playable.GetBehaviour();

        lightControlBehavior.light = light.Resolve(graph.GetResolver());
        lightControlBehavior.color = color; 
        lightControlBehavior.intensity = intensity;

        return playable;

    }

}
