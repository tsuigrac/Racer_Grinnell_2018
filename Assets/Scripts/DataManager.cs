using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour {

	private static DataManager _instance;
	public static DataManager Instance { get { return _instance; } }

	public int level;

	public int body;
	public int engine;
	public int tire;

	public string bodyName;
	public string engineName;
	public string tireName;
	public int car;

	public float mass;
	public float drag;
	public float traction;
	public float torque;
	public float topSpeed;
	public float tireRadius;

	public float[] checkPoints;

	void Awake() {
		if (_instance == null) {
			DontDestroyOnLoad (gameObject);
			_instance = this;
		} else if (_instance != this) {
			Destroy (gameObject);
		}
	}

	void Start() {
		SceneManager.sceneLoaded += OnLevelLoaded;
		car = -1;
		if (!SceneManager.GetActiveScene ().name.Equals ("MainMenu")
			&& !SceneManager.GetActiveScene ().name.Equals ("PartsSelect")
			&& !SceneManager.GetActiveScene ().name.Equals ("CarSelect")) {
            SetCarSettings();
		}
		for (int i = 0; i < checkPoints.Length; i++) {
			checkPoints [i] = -1.0f;
		}
		traction = 1.0f;
		topSpeed = 150.0f;
	}

	void OnLevelLoaded(Scene scene, LoadSceneMode mode) { 
		if (!scene.name.Equals("MainMenu") && !scene.name.Equals("PartsSelect") 
			&& !scene.name.Equals("CarSelect") && !scene.name.Equals("TrackSelect")) {
            SetCarSettings();
		} 
	}

    public void SetCarSettings() {
        car = 9 * body + 3 * engine + tire + 1;
        switch (tire) {
            case 0:
                tireName = "Bayes";
                tireRadius = 0.4f;
                break;
            case 1:
                tireName = "Nightingale";
                tireRadius = 0.45f;
                break;
            case 2:
                tireName = "Gauss";
                tireRadius = 0.37f;
                break;
        }

        if (body == 0) {
            bodyName = "Bayes";
            switch (engine) {
                case 0:
                    engineName = "Bayes";
                    mass = 1500.0f;
                    drag = 0.15f;
                    torque = 2500.0f;
                    break;
                case 1:
                    engineName = "Nightingale";
                    mass = 1500.0f;
                    drag = 0.05f;
                    torque = 2150.0f;
                    break;
                case 2:
                    engineName = "Gauss";
                    mass = 1500.0f;
                    drag = 0.1f;
                    torque = 2500.0f;
                    break;
            }
        }
        else if (body == 1) {
            bodyName = "Nightingale";
            switch (engine) {
                case 0:
                    engineName = "Bayes";
                    mass = 1900.0f;
                    drag = 0.05f;
                    torque = 2500.0f;
                    break;
                case 1:
                    engineName = "Nightingale";
                    mass = 1900.0f;
                    drag = 0.1f;
                    torque = 2500.0f;
                    break;
                case 2:
                    engineName = "Gauss";
                    mass = 1900.0f;
                    drag = 0.1f;
                    torque = 3000.0f;
                    break;
            }
        }
        else if (body == 2) {
            bodyName = "Gauss";
            switch (engine) {
                case 0:
                    engineName = "Bayes";
                    mass = 1500.0f;
                    drag = 0.1f;
                    torque = 2150.0f;
                    break;
                case 1:
                    engineName = "Nightingale";
                    mass = 1900.0f;
                    drag = 0.1f;
                    torque = 2150.0f;
                    break;
                case 2:
                    engineName = "Gauss";
                    mass = 1500.0f;
                    drag = 0.05f;
                    torque = 2500.0f;
                    break;
            }
        }
        GameController.Instance.SetCarSettings(mass, drag, torque, tireRadius);
    }

    public void SetLevel(int level) {
        this.level = level;
    }

    public String GetLevel() {
        switch (level) {
            case 0:
                return "Tutorial";
            case 1:
                return "ChooseCar";
            case 2:
                return "CreateeCar";
            default:
                return "";
        }
    }
}
