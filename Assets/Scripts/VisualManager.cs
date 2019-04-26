﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VisualManager : MonoBehaviour {

	private static VisualManager _instance;
	public static VisualManager Instance { get { return _instance; } }

	public GameObject[] carImages;
	public GameObject[] instructions;

	void Awake() {
		if (_instance == null) {
			_instance = this;
		} else if (_instance != this) {
			Destroy (gameObject);
		}
	}

	public void ShowInstructions() {
		for (int i = 0; i < carImages.Length; i++) {
			if (TutorialController.Instance.trialNum-1 == i) {
				carImages [i].gameObject.SetActive (true);
				instructions [i].gameObject.SetActive (true);
			} else {
				carImages [i].gameObject.SetActive (false);
				instructions [i].gameObject.SetActive (false);
			}
		}
	}

    public void LoadRace() {
        SceneManager.LoadScene("Tutorial");
    }

    //Just adding a function + a button to go back from the
    //tutorial to the main menu
    public void Back() {
		TutorialController.Instance.trialNum = 0;
        SceneManager.LoadScene("MainMenu");
    }
}
