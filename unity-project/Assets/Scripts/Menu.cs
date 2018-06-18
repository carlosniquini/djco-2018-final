using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
  private Text text;

  public string goTo;
  
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
    text = GameObject.Find("Text").GetComponent<Text>();
  }

  // Update is called once per frame
  void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      if (animator.GetBool("About") || animator.GetBool("Config")) {
        About(false);
        Config(false);
      } else {
        Debug.Log("SAIU");
        Application.Quit();
      }
    }
  }

  public void About(bool s) {
    text.text = s ? "Back" : "Exit";
    animator.SetBool("About", s);
  }

  public void Config(bool s) {
    text.text = s ? "Back" : "Exit";
    animator.SetBool("Config", s);
  }

  public void StartGame() {
    StartCoroutine(Fade(goTo));
  }

  private IEnumerator Fade(string cena) {
    float fadetime = StartFade(1);
    yield return new WaitForSeconds(0.5f);
    SceneManager.LoadScene(cena);
  }
}
