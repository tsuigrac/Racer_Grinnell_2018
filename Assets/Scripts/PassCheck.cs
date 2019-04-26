using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCheck : MonoBehaviour {

	void OnTriggerStay(Collider other) {
		if (other.tag == "Finish") {
			GameController.Instance.StopTimer ();
		}
		if (other.tag == "CheckPoint1") {
            GameController.Instance.PassCheckPoint (1);
		}
		if (other.tag == "CheckPoint2") {
            GameController.Instance.PassCheckPoint (2);
		}
		if (other.tag == "CheckPoint3") {
            GameController.Instance.PassCheckPoint (3);
		}
	}
}
