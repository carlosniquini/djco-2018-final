using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Smartphone : MonoBehaviour {

  private FirstPersonController fpsController;
  private Player player;
  private AudioSource audioSource;
  private bool isDisplayed = false;
  private bool isFirst = true;
  public GameObject desktop;
  public AudioClip audioClip;

  // Use this for initialization
  void Start () {
    fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
    player = GameObject.Find("FPSController").GetComponent<Player>();
    desktop.SetActive(true);
    audioSource = this.GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update () {
    if (Input.GetKeyDown("q")) {
      if (isFirst) {
        StartCoroutine(player.VisualStingerSaturation());
        StartCoroutine(player.VisualStingerFocus());
        PlayStinger();
      }
      this.GetComponent<Animator>().SetBool("isDisplayed", isDisplayed = !isDisplayed);
      fpsController.m_MouseLook.SetCursorLock(!isDisplayed);
    }
  }

  public void Display() {
    this.GetComponent<Animator>().SetBool("isDisplayed", isDisplayed = !isDisplayed);
    fpsController.m_MouseLook.SetCursorLock(!isDisplayed);
  }

  public void PlayStinger() {
    isFirst = false;
    StartCoroutine(Stinger());
  }

  private IEnumerator Stinger() {
    yield return new WaitForSeconds(0.5f);
    audioSource.clip = audioClip;
    audioSource.Play();
  }
}
