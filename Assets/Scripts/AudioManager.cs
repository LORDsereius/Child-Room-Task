using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] joyfulClips;
    [SerializeField] private AudioClip[] sadClips;

    [SerializeField] private bool playJoyful = true;

    private void Start()
    {
        PlayRandomMusic();
    }

    public void PlayRandomMusic()
    {
        AudioClip[] selectedClips = playJoyful ? joyfulClips : sadClips;

        if (selectedClips.Length == 0)
        {
            Debug.LogWarning("No music clips assigned.");
            return;
        }

        AudioClip randomClip = selectedClips[Random.Range(0, selectedClips.Length)];
        audioSource.clip = randomClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void SwitchMood(bool joyful)
    {
        playJoyful = joyful;
        PlayRandomMusic();
    }
    private void Update()
{
    if (!audioSource.isPlaying)
    {
        PlayRandomMusic();
    }
}
}
