using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

/// <summary>
/// Changes volume status of a specified audio source.
/// </summary>
public class GameSound : MonoBehaviour 
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private Sprite soundOn;
    
    [SerializeField]
    private Sprite soundOff;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void ChangeVolumeState()
    {
        audioSource.mute = !audioSource.mute;

        if (audioSource.mute)
        {
            image.sprite = soundOff;
        }
        else
        {
            image.sprite = soundOn;
        }
    }
}
