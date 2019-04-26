using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	private static GameController _instance;
	public static GameController Instance { get { return _instance; } }

	public Text timer;
	public bool racing;
	public bool timing;

	public GameObject[] endObjects;
	public Text[] endStats;

	public bool submitted = false;

	public float time;
	public float offTrackTime;

	// Use this for initialization
	void Awake () {
		if (_instance == null) {
			_instance = this;
		} else if (_instance != this) {
			Destroy (gameObject);
		}
		racing = true;
		time = 0;
		offTrackTime = 0;
	}

	void Start() {
		foreach (GameObject endObject in endObjects) {
			endObject.SetActive (false);
		}
		timing = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (timing) {
			time += Time.deltaTime;
			timer.text = (Math.Truncate ((double)time * 100.0) / 100.0).ToString ();
			if (!CarData.Instance.onTrack) {
				offTrackTime += Time.deltaTime;
			}
		}
	}

	public void SetCarSettings(float mass, float drag, float torque, float tireRadius) {
		CarData.Instance.SetCarSettings (mass, drag, torque, tireRadius);
	}

	public void StopTimer() {
		if (racing || timing) {
			timing = false;
			racing = false;
		}
	}

	public void PassCheckPoint(int n) {
		if (DataManager.Instance.checkPoints [n - 1] == -1.0f) {
			DataManager.Instance.checkPoints [n - 1] = time;
		}
	}

	public void SubmitData(bool submit) {
		if (!submitted && submit) {
            global::SubmitData.Instance.SubmitUpload ();
			submitted = true;
			StopTimer ();
		}
		foreach (GameObject endObject in endObjects) {
			endObject.SetActive (true);
		}
		foreach (Text stat in endStats) {
			if (stat.name.Equals("Player ID")) {
				stat.text = "Player ID: " + PlayerData.Instance.playerId;
			} else if (stat.name.Equals("Group ID")) {
				stat.text = "Group ID: " + PlayerData.Instance.groupId;
			} else if (stat.name.Equals ("Body")) {
				stat.text = "Body: " + DataManager.Instance.bodyName;
			} else if (stat.name.Equals ("Engine")) {
				stat.text = "Engine: " + DataManager.Instance.engineName;
			} else if (stat.name.Equals ("Tire")) {
				stat.text = "Tire: " + DataManager.Instance.tireName;
			} else if (stat.name.Equals ("Time")) {
				stat.text = "Time: " + (Math.Truncate ((double)time * 100.0) / 100.0).ToString ();
			} 
		}

	}

	public void PlayAgain() {
		SceneManager.LoadScene ("MainMenu");
	}

    public void OpenDataPage() {
        Application.OpenURL("https://www.stat2games.sites.grinnell.edu/data/racer/racer.php");
    }
}
