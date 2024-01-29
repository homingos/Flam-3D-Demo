using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationButtonController : MonoBehaviour
{
    public Animator characterAnimator; // Reference to the Animator component on your character
    public AnimationButtonPair[] animationButtonPairs; // Array of animation-button pairs
    public Button[] menuButtons; // Array of menu buttons to stop animations and sounds

    private AudioSource audioSource; // Reuse a single AudioSource component

    [System.Serializable]
    public class AnimationButtonPair
    {
        public string animationName; // Name of the animation to play
        public Button button; // Reference to the UI Button component
        public AudioClip animationAudio; // Audio clip to play when this animation is triggered
    }

    private void Start()
    {
        if (characterAnimator == null)
        {
            Debug.LogError("Make sure to assign the characterAnimator!");
        }

        // Create or get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = true; // Set the audio source to loop

        foreach (var pair in animationButtonPairs)
        {
            if (pair.button == null)
            {
                Debug.LogError("One or more buttons in the animationButtonPairs array are not assigned!");
            }
            else
            {
                pair.button.onClick.AddListener(() => PlayAnimation(pair.animationName, pair.animationAudio));
            }
        }

        foreach (var menuButton in menuButtons)
        {
            if (menuButton == null)
            {
                Debug.LogError("One or more buttons in the menuButtons array are not assigned!");
            }
            else
            {
                menuButton.onClick.AddListener(StopAnimationAndSound);
            }
        }
    }

    private void PlayAnimation(string animationName, AudioClip audioClip)
    {
        // Stop the previous audio clip if it's playing
        audioSource.Stop();

        // Play the animation
        characterAnimator.Play(animationName);

        // Play the associated audio clip in a loop
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    private void StopAnimationAndSound()
    {
        // Stop the animation and sound
        characterAnimator.Play("Idle"); // Replace "Idle" with the name of your idle animation
        audioSource.Stop();
    }
}
