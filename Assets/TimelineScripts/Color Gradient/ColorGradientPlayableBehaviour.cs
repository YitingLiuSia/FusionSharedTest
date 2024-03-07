
using UnityEngine;
using UnityEngine.Playables;

public class ColorGradientPlayableBehaviour : PlayableBehaviour
{
    public Color fromColor; 
    public Color toColor;
    public MeshRenderer mr=null;
  
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (mr == null)
            return;

        var currentTime = (float)playable.GetTime() / (float)playable.GetDuration();
        Color currentColor = Color.Lerp(fromColor, toColor, currentTime);
        mr.material.color = currentColor;
    }



}

