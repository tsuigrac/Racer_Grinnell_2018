using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTimer : MonoBehaviour {

	public void StartTiming() {
		GameController.Instance.timing = true;
	}
}
