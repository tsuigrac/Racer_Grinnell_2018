using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

//this script sends game data to the server
public class SubmitData : MonoBehaviour {

	private static SubmitData _instance;
	public static SubmitData Instance { get { return _instance; } }

	String playerID = "";
	String groupID = "";

	public void Awake() {
		if (_instance == null) {
			_instance = this;
		} else if (_instance != this) {
			Destroy (gameObject);
		}
	}

	public void Start() {
		playerID = PlayerData.Instance.playerId.ToLower();
		groupID = PlayerData.Instance.groupId.ToLower();
	}

	public void SubmitUpload(){
		StartCoroutine (Upload ());
	}
	
	IEnumerator Upload() {

		int gameNum = -1;
		int orderNum = -1;

		WWW getGameNum = new WWW ("https://stat2games.sites.grinnell.edu/php/getracergamenum.php");
		yield return getGameNum;

		try {
			gameNum = int.Parse (getGameNum.text);
		} catch (Exception e) {
			Debug.Log ("Fetching game number failed.  Error message: " + e.ToString ());
			gameNum = 1;
		}

		WWWForm ids = new WWWForm();

		ids.AddField("playerID", playerID);
		ids.AddField("groupID", groupID);
		WWW getOrderNum = new WWW ("https://stat2games.sites.grinnell.edu/php/getracerordernum.php", ids);
		yield return getOrderNum;

		try {
			orderNum = int.Parse (getOrderNum.text);
		} catch (Exception e) {
			Debug.Log ("Fetching order number failed.  Error message: " + e.ToString ());
			orderNum = 1;
		}

		WWWForm form = new WWWForm ();
		form.AddField ("game", gameNum);
		form.AddField ("date", DateTime.Now.ToString ());
		form.AddField ("playerID", playerID);
		form.AddField ("groupID", groupID);
		form.AddField ("level", DataManager.Instance.GetLevel());
		form.AddField ("order", orderNum);
		form.AddField ("track", SceneManager.GetActiveScene ().name);
		form.AddField ("car", DataManager.Instance.car);
		form.AddField ("body", DataManager.Instance.bodyName);
		form.AddField ("engine", DataManager.Instance.engineName);
		form.AddField ("tire", DataManager.Instance.tireName);
		form.AddField ("finished", (!GameController.Instance.racing) ? 1 : 0);
		form.AddField ("finishTime", Truncate(GameController.Instance.time).ToString());
		form.AddField ("topSpeedReached", Truncate(CarData.Instance.maxSpeed).ToString());
		form.AddField ("timeTo30", Truncate(CarData.Instance.timeTo30).ToString());
		form.AddField ("timeTo60", Truncate(CarData.Instance.timeTo60).ToString());
		form.AddField ("timeTo100", Truncate(CarData.Instance.timeTo100).ToString());
		form.AddField ("checkPoint1", Truncate(DataManager.Instance.checkPoints[0]).ToString());
		form.AddField ("checkPoint2", Truncate(DataManager.Instance.checkPoints[1]).ToString());
		form.AddField ("checkPoint3", Truncate(DataManager.Instance.checkPoints[2]).ToString());
		form.AddField("timeOnTrack", Truncate(
			GameController.Instance.time - GameController.Instance.offTrackTime).ToString());
		form.AddField ("timeOffTrack", Truncate(GameController.Instance.offTrackTime).ToString());
		form.AddField ("mass", Truncate(DataManager.Instance.mass).ToString());
		form.AddField ("drag", Truncate(DataManager.Instance.drag).ToString());
		form.AddField ("traction", Truncate(DataManager.Instance.traction).ToString());
		form.AddField ("fullTorqueOverAllWheel", Truncate(DataManager.Instance.torque).ToString());
		form.AddField ("topSpeed", Truncate(DataManager.Instance.topSpeed).ToString());
		form.AddField ("tireRadius", Truncate(DataManager.Instance.tireRadius).ToString());
        form.AddField("variable1", PlayerData.Instance.variable1);
        form.AddField("variable2", PlayerData.Instance.variable2);
        form.AddField("variable3", PlayerData.Instance.variable3);
#pragma warning disable CS0618 // Type or member is obsolete
        using (WWW www = new WWW("https://stat2games.sites.grinnell.edu/php/sendracergameinfo.php", form))
#pragma warning restore CS0618 // Type or member is obsolete
        {
            yield return www;

            if (www.text == "0")
            {
                Debug.Log("Player data created successfully.");
            }
            else
            {
                Debug.Log("Player data creation failed. Error # " + www.text);
            }
        }
    }

    private double Truncate(float f) {
		return (double)(Math.Truncate((double)f*100.0) / 100.0);
	}
}