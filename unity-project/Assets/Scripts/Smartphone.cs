using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Smartphone : MonoBehaviour {

  private FirstPersonController fpsController;
  private bool isDisplayed = false;

  // Use this for initialization
  void Start () {
    fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
  }

  // Update is called once per frame
  void Update () {
    if (Input.GetKeyDown("e")) {
      this.GetComponent<Animator>().SetBool("isDisplayed", isDisplayed = !isDisplayed);
      fpsController.m_MouseLook.SetCursorLock(!isDisplayed);
    }
  }
}
