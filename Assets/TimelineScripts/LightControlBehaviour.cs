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
    float bounceIntensity = 0f;
    float range = 10f;

    [SerializeField] Color defaultColor;
    [SerializeField] float defaultIntensity;
    [SerializeField] float defaultBounceIntensity;
    [SerializeField] float defaultRange;

    public ClipCaps clipCaps { get { return ClipCaps.Blending; } }

    public override void OnPlayableCreate(Playable playable)
    {
        if (light == null)
            return;

        defaultColor = color;
        defaultIntensity = intensity;
        defaultBounceIntensity = bounceIntensity;
        defaultRange = range;
    }
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (light == null)
            return;

        Debug.Log("process frame in the behaviour");
      
        light.color = color; 
        light.intensity = intensity;    
        light.bounceIntensity = bounceIntensity;
        light.range = range;    

    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (light == null)
            return;

        light.color = defaultColor;
        light.intensity = defaultIntensity;
        light.bounceIntensity = defaultBounceIntensity;
        light.range = defaultRange;

    }
}
