using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppMsg : MonoBehaviour {

  private bool isDisplayed = false;
  private bool isFirst = true;
  private Smartphone smartphone;
  public GameObject desktop;

  // Use this for initialization
  void Start () {
    smartphone = this.GetComponentInParent<Smartphone>();
  }

  // Update is called once per frame
  void Update () {

  }

  public void Show() {
    if (isFirst) {
      smartphone.PlayStinger();
      isFirst = false;
    }
    this.GetComponent<Animator>().SetBool("isDisplayed", isDisplayed = !isDisplayed);
    desktop.SetActive(!isDisplayed);
  }
}
