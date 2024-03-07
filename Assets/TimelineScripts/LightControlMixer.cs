using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LightControlMixer : PlayableBehaviour
{

    private Light light;

    Color blendedColor = Color.clear;
    float blendedIntensity = 0f;
    float blendedBounceIntensity = 0f;
    float blendedRange = 0f;
    float totalWeight = 0f;

    Color defaultColor;
    float defaultBlendedIntensity;
    float defaultBlendedBouncedIntensity;
    float defaultRange;

    bool firstFrameHappened;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Debug.Log("MIXER proecss frame");
        if (light == null)
            return;

        if (!firstFrameHappened)
        {
            firstFrameHappened = true;
            defaultColor = light.color;
            defaultBlendedIntensity = light.intensity;
            defaultBlendedBouncedIntensity = light.bounceIntensity;
            defaultRange = light.range;
        }
        else
        {
            int inputCount = playable.GetOutputCount();

            for (int i = 0; i < inputCount; i++)
            {
                float inputWeigt = playable.GetInputWeight(i);
                ScriptPlayable<LightControlBehaviour> inputPlayable = (ScriptPlayable<LightControlBehaviour>)playable.GetInput(i);
                LightControlBehaviour behavior = inputPlayable.GetBehaviour();

                blendedColor += inputWeigt * behavior.color;
                blendedIntensity += inputWeigt * behavior.intensity;
                blendedBounceIntensity += inputWeigt * behavior.light.bounceIntensity;
                blendedRange += inputWeigt * behavior.light.range;

                Debug.Log($"defaultColor {defaultColor}" +
                    $"defaultBlendedIntensity {defaultBlendedIntensity}" +
                    $"defaultBlendedBouncedIntensity {defaultBlendedBouncedIntensity}" +
                    $"defaultRange {defaultRange}");

                totalWeight += inputWeigt;
            }
            float remainingWeight = 1 - totalWeight;

            light.color = blendedColor + remainingWeight * defaultColor;
            light.intensity = blendedIntensity + remainingWeight * defaultBlendedIntensity;
            light.bounceIntensity = blendedBounceIntensity + remainingWeight * defaultBlendedBouncedIntensity;
            light.range = blendedRange + remainingWeight * defaultRange;
        }
    }

    public override void OnPlayableDestroy(Playable playable)
    {
        firstFrameHappened = false;

        if (light == null)
            return;

        light.color = defaultColor;
        light.intensity = defaultBlendedIntensity;
        light.range = defaultRange;
        light.bounceIntensity = defaultBlendedBouncedIntensity;

    }
}
