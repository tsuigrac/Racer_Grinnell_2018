using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackSelector : MonoBehaviour {

	[System.Serializable]
	private class TrackInfo {
		public string trackLevelName;
		public GameObject image;
		public GameObject text;

		public TrackInfo() {
			trackLevelName = "";
			image = null;
			text = null;
		}
	}

    public int curTrack = 0;

    [SerializeField] private TrackInfo[] trackInfos;
    void Start() {
        UpdateTrack();

    }

    public void NextTrack() {
		curTrack = Mathf.Abs((curTrack + 1) % trackInfos.Length);
        UpdateTrack();
	}

	public void PrevTrack() {
		if (curTrack == 0) {
            curTrack = trackInfos.Length - 1;
        }
        else {
            curTrack = Mathf.Abs((curTrack - 1) % trackInfos.Length);
        }
        UpdateTrack();
	}

	public void UpdateTrack() {
		for (int i = 0; i < trackInfos.Length; i++) {
			if (i == curTrack) {
				trackInfos [i].image.SetActive (true);
				trackInfos [i].text.SetActive (true);
			} else {
				trackInfos [i].image.SetActive (false);
				trackInfos [i].text.SetActive (false);
			}
		}
	}

	public void LoadTrack() {
		SceneManager.LoadScene (trackInfos [curTrack].trackLevelName);
	}

	public void Back() {
		if (DataManager.Instance.level == 1) {
			SceneManager.LoadScene ("CarSelect");
		} else if (DataManager.Instance.level == 2) {
			SceneManager.LoadScene ("PartsSelect");
		}
	}
}