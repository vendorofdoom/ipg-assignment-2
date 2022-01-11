using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{

    [Header("ButtonImages")]
    public Sprite audioOn;
    public Sprite audioOff;

    public Image img;

    public bool mute = false;

    private void Start()
    {
        SetImage();
    }

    public void ToggleMute()
    {
        mute = !mute;
        AudioListener.volume = mute ? 0f : 1f;
        SetImage();
    }

    private void SetImage()
    {
        if (!mute)
        {
            img.sprite = audioOn;
        }
        else
        {
            img.sprite = audioOff;
        }
    }

}
