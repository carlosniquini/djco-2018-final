using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTips : MonoBehaviour {

  private Animator animator;
  private AudioSource audio;
  private Text text;

  // Use this for initialization
  void Start () {
    animator = this.GetComponent<Animator>();
    audio = this.GetComponent<AudioSource>();
    text = this.GetComponentInChildren<Text>();
  }

  // Update is called once per frame
  void Update () {

  }

  public void ShowTip(string s) {
    StartCoroutine(ShowTipCoroutine(s));
  }

  public void ShowTipUntil(string s, string k) {
    StartCoroutine(ShowTipUntilCoroutine(s, k));
  }

  private IEnumerator ShowTipCoroutine(string s) {
    animator.SetBool("isDisplayed", true);
    text.text = s;
    audio.Play();
    yield return new WaitForSeconds(5);
    animator.SetBool("isDisplayed", false);
  }

  private IEnumerator ShowTipUntilCoroutine(string s, string k) {
    animator.SetBool("isDisplayed", true);
    text.text = s;
    audio.Play();
    bool aux = true;
    while (aux) {
      if (Input.GetKeyDown(k)) {
        animator.SetBool("isDisplayed", false);
        aux = false;
      }
      yield return new WaitForSeconds(0);
    }
  }


}
