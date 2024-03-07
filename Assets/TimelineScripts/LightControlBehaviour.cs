using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class LightControlBehaviour : PlayableBehaviour
{
    public Light light = null;
    public Color color = Color.white;
    public float intensity = 1f;

    [SerializeField] Color defaultColor;
    [SerializeField] float defaultIntensity;

    public ClipCaps clipCaps { get { return ClipCaps.Blending; } }

    public override void OnPlayableCreate(Playable playable)
    {
        if (light == null)
            return;

        defaultColor = color;
        defaultIntensity = intensity;
    }
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {

        if (light == null)
            return;

        light.color = Color.Lerp(defaultColor, color, (float)playable.GetDuration());
        light.intensity = intensity;
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (light == null)
            return;

        light.color = color;
        light.intensity = defaultIntensity;

    }
}
