using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PartSelector : MonoBehaviour {

	public GameObject[] cars;
	public int bodyIndex;
	public int engineIndex;
	public int wheelIndex;

	public Dropdown bodyDropDown;
	public Dropdown engineDropDown;
	public Dropdown wheelDropDown;
	public string nextSceneName;

    public GameObject bodyPanel;
    public GameObject enginePanel;
    public GameObject wheelPanel;

    // Use this for initialization
    void Start () {
		bodyIndex = 0;
		engineIndex = 0;
		wheelIndex = 0;
		cars [0].SetActive(true);
		for (int i = 1; i < cars.Length; i++) {
			cars[i].SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
        LoadCars(bodyDropDown.value, engineDropDown.value, wheelDropDown.value);
		bodyIndex = bodyDropDown.value;
		engineIndex = engineDropDown.value;
		wheelIndex = wheelDropDown.value;
	}

	public void LoadCars(int curBody, int curEngine, int curWheel) {
		if (curBody != bodyIndex) {
            bodyPanel.SetActive(false);
			for (int i = 0; i < cars.Length; i++) {
				if (i == bodyIndex) {
					cars [i].SetActive (false);
				}
				if (i == curBody) {
					cars [i].SetActive (true);
				}
			}
		}
		if (curBody == 0) {
			cars [0].SetActive (true);
		}
        if (curEngine != engineIndex) {
            enginePanel.SetActive(false);
        }
        if (curEngine != wheelIndex) {
            wheelPanel.SetActive(false);
        }
    }

	public void Next() {
		if (bodyIndex != 0 && engineIndex != 0 && wheelIndex != 0) {
			DataManager.Instance.body = bodyIndex - 1;
			DataManager.Instance.engine = engineIndex - 1;
			DataManager.Instance.tire = wheelIndex - 1;
			SceneManager.LoadScene (nextSceneName);
		}
        else {
            if (bodyIndex == 0) {
                bodyPanel.SetActive(true);
            }
            if (engineIndex == 0) {
                enginePanel.SetActive(true);
            }
            if (wheelIndex == 0) {
                wheelPanel.SetActive(true);
            }
        }
    }

	public void Back() {
		SceneManager.LoadScene ("MainMenu");
	}
}
