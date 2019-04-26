using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownAudio : MonoBehaviour {

	public AudioSource audio1;
	public AudioSource audio2;


	void Start() {
		StartCoroutine (PlayCountdown());
	}

	IEnumerator PlayCountdown() {
		audio1.Play ();
		yield return new WaitForSeconds (1);
		audio1.Play ();
		yield return new WaitForSeconds (1);
		audio1.Play ();
		yield return new WaitForSeconds (1);
		audio1.Play ();
		audio2.Play ();
	}
}
