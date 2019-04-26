using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelector : MonoBehaviour {

	public GameObject[] cars;
	public int activeIndex;

    public GameObject panel;

    public Dropdown dropDown;
	public string nextSceneName;

	// Use this for initialization
	void Start () {
		activeIndex = 0;
		cars [0].SetActive(true);
		for (int i = 1; i < cars.Length; i++) {
			cars[i].SetActive (false);
		}
	}

	void Update () {
        // Load corresponding scene according to selection from the dropdown
        LoadCars(dropDown.value);
    }

	public void LoadCars(int index) {
		if (index != activeIndex) {
            panel.SetActive(false);
			for (int i = 0; i < cars.Length; i++) {
				if (i == activeIndex) {
					cars [i].SetActive (false);
				}
				if (i == index) {
					cars [i].SetActive (true);
				}
			}
			activeIndex = index;
		}
		if (index == 0) {
			cars [0].SetActive (true);
		}
	}

	public void Next() {
		if (activeIndex != 0) {
			List<Dropdown.OptionData> menuOptions = dropDown.GetComponent<Dropdown> ().options;
			string carName = menuOptions [activeIndex].text;

			DataManager.Instance.body = activeIndex - 1;
			DataManager.Instance.engine = activeIndex - 1;
			DataManager.Instance.tire = activeIndex - 1;

			SceneManager.LoadScene (nextSceneName);
		} else {
            panel.SetActive(true);
        }
    }

	public void Back() {
		SceneManager.LoadScene ("MainMenu");
	}
}
