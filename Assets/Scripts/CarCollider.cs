using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollider : MonoBehaviour {

	private static CarCollider _instance;
	public static CarCollider Instance { get { return _instance; } }

	private Rigidbody rigidBody;


	public void Awake() {
		if (_instance == null) {
			_instance = this;
		}   else if (_instance != this) {
			Destroy (gameObject);
		}
		rigidBody = GetComponent<Rigidbody> ();
	}

	public void Update() {
		RaycastHit hit;
		Ray ray = new Ray(transform.position,-Vector3.up * 0.5f);
		if (Physics.Raycast (ray, out hit, 0.5f) 
			&& hit.collider.gameObject.tag.Equals ("Track") 
			&& !CarData.Instance.onTrack) {
			rigidBody.drag -= 0.7f;
			CarData.Instance.onTrack = true;
		} else if (Physics.Raycast (ray, out hit, 0.1f)
			&& hit.collider.gameObject.tag.Equals ("Terrain") 
			&& CarData.Instance.onTrack) {
			rigidBody.drag += 0.7f;
			CarData.Instance.onTrack = false;
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Submit")) {
			GameController.Instance.SubmitData(true);
		} else if (other.gameObject.tag.Equals ("Finish")) {
			GameController.Instance.SubmitData(false);
		}
	}
}
