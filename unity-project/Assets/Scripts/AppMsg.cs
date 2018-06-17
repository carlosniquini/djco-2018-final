using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppMsg : MonoBehaviour {

  private bool isDisplayed = false;
  private bool isFirst = true;
  private Smartphone smartphone;
  public GameObject desktop;
  public Text signal, percentage;

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
    this.GetComponent<AudioSource>().Play();
    this.GetComponent<Animator>().SetBool("isDisplayed", isDisplayed = !isDisplayed);
    if (isDisplayed) {
      signal.color = Color.black;
      percentage.color = Color.black;
    } else {
      signal.color = Color.white;
      percentage.color = Color.white;
    }
    desktop.SetActive(!isDisplayed);
  }
}
