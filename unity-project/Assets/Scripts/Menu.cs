using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

  private Animator animator;
  private bool about = false;
  private bool config = false;
  public Texture2D fadeTexture;
  public float speed = 0.8f;
  private int drawDepth = -1000;
  private float alpha = 1.0f;
  private int fadeDir = -1;

  private void OnGUI() {
    alpha += fadeDir * speed * Time.deltaTime;
    alpha = Mathf.Clamp01(alpha);
    GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
    GUI.depth = drawDepth;
    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
  }

  private float StartFade(int direction) {
    fadeDir = direction;
    return (speed);
  }

  private void OnLevelWasLoaded() {
    StartFade(-1);
  }
  // Use this for initialization
  void Start () {
    animator = GameObject.Find("Main Camera").GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update () {
    if (Input.GetKey(KeyCode.Escape)) {
      About(false);
      Config(false);
    }
  }

  public void About(bool s) {
    animator.SetBool("About", s);
  }

  public void Config(bool s) {
    animator.SetBool("Config", s);
  }

  public void StartGame() {
    StartCoroutine(Fade("intro"));
  }

  private IEnumerator Fade(string cena) {
    float fadetime = StartFade(1);
    yield return new WaitForSeconds(0.5f);
    SceneManager.LoadScene(cena);
  }
}
