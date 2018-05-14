using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options: MonoBehaviour {

  private Animator animator;
  private Transform target;
  private Camera camera_;
  private RectTransform canvasRectTransform;

  void Start() {
    animator = this.GetComponent<Animator>();

  }

  void Update() {
    canvasRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();
    camera_ = GameObject.Find("FPSController").GetComponentInChildren<Camera>();
    if (target != null) {
      Vector2 ViewportPosition = camera_.WorldToViewportPoint(target.transform.position);
      Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * canvasRectTransform.sizeDelta.x) - (canvasRectTransform.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * canvasRectTransform.sizeDelta.y) - (canvasRectTransform.sizeDelta.y * 0.5f)));

      this.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
    }

  }

  public void SetTarget(Transform go) {
    target = go;
  }

  public void SetOptios(Animator ani) {
    animator = ani;
  }

  public void ShowOptions() {
    animator.SetBool("isDisplayed", true);
  }

  public void HideOptions(GameObject go) {
    if(target != null)
      if (go == target.gameObject) {
        //if(animator.GetBool("isDisplayed"))
        animator.SetBool("isDisplayed", false);
      }

  }

}
