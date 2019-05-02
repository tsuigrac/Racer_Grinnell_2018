using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    private static MainMenuController _instance;
    public static MainMenuController Instance { get { return _instance; } }

    public InputField playerIDText;
	public InputField groupIDText;
    public InputField[] optionalVariables;
    public GameObject[] panels; //objects that appear when a player tries to continue without entering IDs

    void Awake() {
        if (_instance == null) {
            _instance = this;
        } else if (_instance != this) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        if (!string.IsNullOrEmpty(PlayerData.Instance.playerId)) {
            playerIDText.text = PlayerData.Instance.playerId;
        }
        if (!string.IsNullOrEmpty(PlayerData.Instance.groupId)) {
            groupIDText.text = PlayerData.Instance.groupId;
        }
    }

    public void LoadTutorial() {
        if (IsIDsEmpty()) {
            foreach(GameObject panel in panels) {
                panel.SetActive(true);
            } 
        } else {
            SetInputs();
            SceneManager.LoadScene ("TutorialInstructions");
        }
	}

	public void LoadMainMenu() {
		SceneManager.LoadScene ("MainMenu");
	}

	public void LoadCarSelect() {
        if (IsIDsEmpty()) {
            foreach (GameObject panel in panels) {
                panel.SetActive(true);
            }
        } else {
            SetInputs();
            SceneManager.LoadScene("CarSelect");
        }
    }

	public void LoadPartsSelect() {
        if (IsIDsEmpty()) {
            foreach (GameObject panel in panels) {
                panel.SetActive(true);
            }
        } else {
            SetInputs();
            SceneManager.LoadScene ("PartsSelect");
       }
	}

	public void OpenDataPage() {
		Application.OpenURL("https://www.stat2games.sites.grinnell.edu/data/racer/racer.php");
	}

	public void SetInputs() {
		PlayerData.Instance.playerId = playerIDText.text;
		PlayerData.Instance.groupId = groupIDText.text;
        PlayerData.Instance.variable1 = optionalVariables[0].text;
        PlayerData.Instance.variable2 = optionalVariables[1].text;
        PlayerData.Instance.variable3 = optionalVariables[2].text;
    }

    public bool IsIDsEmpty() {
        return (string.IsNullOrEmpty(groupIDText.text) || string.IsNullOrEmpty(playerIDText.text));
    }
}
