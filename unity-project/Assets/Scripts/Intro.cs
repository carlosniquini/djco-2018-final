using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

  private AudioSource audioSource;

  // Use this for initialization
  void Start () {
    audioSource = this.GetComponent<AudioSource>();
    StartCoroutine(playSound());
  }

  IEnumerator playSound() {
    audioSource.Play();
    yield return new WaitForSeconds(audioSource.clip.length);
    SceneManager.LoadScene("main");
  }

  // Update is called once per frame
  void Update () {

  }
}
