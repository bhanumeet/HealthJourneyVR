using UnityEngine;
using TMPro;
using System.Collections;

#if ENABLE_XR_INTERACTION_TOOLKIT
using UnityEngine.XR.Interaction.Toolkit;
#endif

public class AudioSubtitleManager : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI subtitleText;

    #if ENABLE_XR_INTERACTION_TOOLKIT
    public XRBaseInteractable xrButton;
    #endif

    public UnityEngine.UI.Button uiButton; // Fallback for non-VR testing

    [System.Serializable]
    public struct Subtitle
    {
        public string text;
        public float startTime;
        public float endTime;
    }

    public Subtitle[] subtitles;

    private void Start()
    {
        // Ensure the subtitle text is empty at start
        subtitleText.text = "";

        #if ENABLE_XR_INTERACTION_TOOLKIT
        if (xrButton != null)
        {
            xrButton.activated.AddListener(OnButtonPressed);
        }
        #endif

        if (uiButton != null)
        {
            uiButton.onClick.AddListener(OnButtonPressed);
        }
    }

    #if ENABLE_XR_INTERACTION_TOOLKIT
    private void OnButtonPressed(ActivateEventArgs args)
    {
        PlayAudioAndSubtitles();
    }
    #endif

    private void OnButtonPressed()
    {
        PlayAudioAndSubtitles();
    }

    private void PlayAudioAndSubtitles()
    {
        // Play the audio
        audioSource.Play();

        // Start displaying subtitles
        StartCoroutine(DisplaySubtitles());
    }

    private IEnumerator DisplaySubtitles()
    {
        foreach (Subtitle subtitle in subtitles)
        {
            // Wait until it's time to display this subtitle
            yield return new WaitForSeconds(subtitle.startTime - audioSource.time);

            // Display the subtitle
            subtitleText.text = subtitle.text;

            // Wait until it's time to clear this subtitle
            yield return new WaitForSeconds(subtitle.endTime - subtitle.startTime);

            // Clear the subtitle
            subtitleText.text = "";
        }
    }
}