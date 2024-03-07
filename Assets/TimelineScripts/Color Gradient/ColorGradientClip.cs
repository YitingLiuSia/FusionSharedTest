using UnityEngine.Playables;
using UnityEngine.Timeline;

using System;
using UnityEngine;

[Serializable]
public class ColorGradientClip : PlayableAsset, ITimelineClipAsset
{
    public ColorGradientPlayableBehaviour template = new ColorGradientPlayableBehaviour();
    public ExposedReference< Renderer> _mr; 
    public Color _fromColor;
    public Color _toColor;

    public ClipCaps clipCaps { get { return ClipCaps.Blending; } }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<ColorGradientPlayableBehaviour>.Create(graph);
        template = playable.GetBehaviour();
        template.fromColor = _fromColor; 
        template.toColor = _toColor;
        template.mr = (Renderer)_mr.Resolve(graph.GetResolver());

        return playable;
    }
}