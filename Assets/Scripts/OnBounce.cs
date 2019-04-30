using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBounce : MonoBehaviour
{
    private AudioManager sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionExit(Collision collision)
    {
        sound.PlayFxSound(AudioManager.FXSound.Bounce);
    }
}
