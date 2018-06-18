using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Video : MonoBehaviour {

  private VideoPlayer videoPlayer;
  public string goTo;

  // Use this for initialization
  void Start () {
    videoPlayer = this.GetComponent<VideoPlayer>();
    StartCoroutine(playVideo());
  }

  IEnumerator playVideo() {
    //videoPlayer.Play();
    yield return new WaitForSeconds((float)videoPlayer.clip.length);
    SceneManager.LoadScene(goTo);
  }

  // Update is called once per frame
  void Update () {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      SceneManager.LoadScene(goTo);
    }
  }
}
