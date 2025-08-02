using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingHandler : MonoBehaviour
{
    [SerializeField]
    private Volume volume;
    private ChannelMixer channelMixer;
    private Vignette vignette;
    void Start()
    {
        volume.profile.TryGet<Vignette>(out vignette);
        volume.profile.TryGet<ChannelMixer>(out channelMixer);
    }
    public void shiftToHappy()
    {
        StartCoroutine(2f.Tweeng((p) => vignette.intensity.value = p, vignette.intensity.value, 0.2f));
        StartCoroutine(2f.Tweeng((p) => vignette.smoothness.value = p, vignette.intensity.value, 0.2f));
        StartCoroutine(2f.Tweeng((p) => channelMixer.redOutRedIn.value = p, channelMixer.redOutRedIn.value, 100f));
    }
    public void shiftToSad()
    {
        Debug.Log("hmmm");
        StartCoroutine(2f.Tweeng((p) => vignette.intensity.value = p, vignette.intensity.value, 0.6f));
        StartCoroutine(2f.Tweeng((p) => vignette.smoothness.value = p, vignette.intensity.value, 0.5f));
        StartCoroutine(2f.Tweeng((p) => channelMixer.redOutRedIn.value = p, channelMixer.redOutRedIn.value, 40f));
    }
}
