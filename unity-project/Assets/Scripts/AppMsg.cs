using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppMsg : MonoBehaviour {

  private bool isDisplayed = false;
  public GameObject desktop;

  // Use this for initialization
  void Start () {

  }

  // Update is called once per frame
  void Update () {

  }

  public void Show() {
    this.GetComponent<Animator>().SetBool("isDisplayed", isDisplayed = !isDisplayed);
    desktop.SetActive(!isDisplayed);
  }
}
