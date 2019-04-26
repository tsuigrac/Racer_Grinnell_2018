using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionTextShow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        this.gameObject.SetActive(true);
    }

    private void Update() {
        if (GameController.Instance.timing && Input.GetKeyDown("up")) {
            this.gameObject.SetActive(false);
        }
    }
}
