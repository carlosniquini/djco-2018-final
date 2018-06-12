using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options: MonoBehaviour {

  private Animator animator;
  //private Item target;
  private GameObject target;
  private Camera camera_;
  private RectTransform canvasRectTransform;
  private GameObject imgPick, imgInv, imgDia, imgAct;

  void Start() {
    animator = this.GetComponent<Animator>();
    imgPick = this.gameObject.transform.GetChild(0).gameObject;
    imgInv = this.gameObject.transform.GetChild(1).gameObject;
    imgDia = this.gameObject.transform.GetChild(2).gameObject;
    imgAct = this.gameObject.transform.GetChild(3).gameObject;
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

      if (target.GetComponent<Item>()) {
        if (target.GetComponent<Item>().isPickable) {
          HideAll();
          imgPick.SetActive(true);
          //imgInv.SetActive(false);
        }
        if (target.GetComponent<Item>().inventoryItem) {
          //imgPick.SetActive(false);
          HideAll();
          imgInv.SetActive(true);
        }
      }
      if (target.GetComponent<Task>()) {
        if (target.GetComponent<Task>().dialog) {
          HideAll();
          imgDia.SetActive(true);
        }
        if (target.GetComponent<Task>().action) {
          HideAll();
          imgAct.SetActive(true);
        }
      }
      return;
    }
    //else {
    //imgPick.SetActive(true);
    //imgInv.SetActive(true);
    imgInv.SetActive(false);
    imgPick.SetActive(false);
    imgDia.SetActive(false);
    imgAct.SetActive(false);
    //}


  }

  private void HideAll() {
    imgInv.SetActive(false);
    imgPick.SetActive(false);
    imgDia.SetActive(false);
    imgAct.SetActive(false);
  }

  public void SetTarget(Item go) {
    target = go.gameObject;
  }

  public void SetTarget(Task go) {
    target = go.gameObject;
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
