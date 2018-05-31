using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Smartphone : MonoBehaviour {

  private FirstPersonController fpsController;
  private bool isDisplayed = false;
  public GameObject desktop;

  // Use this for initialization
  void Start () {
    fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
    desktop.SetActive(true);

  }

  // Update is called once per frame
  void Update () {
    if (Input.GetKeyDown("q")) {
      this.GetComponent<Animator>().SetBool("isDisplayed", isDisplayed = !isDisplayed);
      fpsController.m_MouseLook.SetCursorLock(!isDisplayed);
    }
  }

  public void Display() {
    this.GetComponent<Animator>().SetBool("isDisplayed", isDisplayed = !isDisplayed);
    fpsController.m_MouseLook.SetCursorLock(!isDisplayed);
  }
}
