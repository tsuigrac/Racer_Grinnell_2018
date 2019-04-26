using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMainMenu : MonoBehaviour {
    public AudioSource buttonSound;

    public void Play() {
        if (!MainMenuController.Instance.IsIDsEmpty()) {
                buttonSound.Play();
        }
    }

}