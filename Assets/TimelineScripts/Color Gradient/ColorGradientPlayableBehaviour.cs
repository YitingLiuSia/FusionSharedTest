
using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class ColorGradientPlayableBehaviour : PlayableBehaviour
{
    public Color fromColor;
    public Color toColor;
    public Renderer mr = null;

    public Color defaultColor;
    Color currentColor;

    public override void OnPlayableCreate(Playable playable)
    {
        if (mr == null)
            return;

        defaultColor = Color.white;
        mr.sharedMaterial.color = defaultColor;

   }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        mr = playerData as Renderer;
        if (mr == null)
            return;

        var dur = (float)playable.GetDuration();
        var currentTime = (float)playable.GetTime();
        var fraction = currentTime / dur; 

        currentColor = Color.Lerp(fromColor, toColor, currentTime/dur);// t is from 0-1
        mr.sharedMaterial.color= currentColor;

    }
    public override void OnPlayableDestroy(Playable playable)
    {
        if (mr == null)
            return;
        mr.sharedMaterial.color = defaultColor;

    }
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (mr == null)
            return;

        mr.sharedMaterial.color = fromColor;

    }
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {

        if (mr == null)
            return;

        mr.sharedMaterial.color = defaultColor;

        Debug.Log("OnBehaviourPause current material color is " + mr.sharedMaterial.color);

    }

}

