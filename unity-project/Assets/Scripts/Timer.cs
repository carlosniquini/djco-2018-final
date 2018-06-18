using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
  public Image FullBatteryImage;
  public Image RedBatteryImage;
  //public Image batterySaving; //If we make a battery saving mode
  private float timeLeft;
  public float MissingPercentage = 55;
  public Text percentageText;
  public float TotalTimeOfGameInMinutes = 250; //max time of the game in MINUTOS --> 15000 sec = 250 min = 4.16 horas
  public float RedBatteryStartsInMinutes = 20;
  //public GameObject gameOverText;
  public Text clock;
  private int hh = 21; 
  private int mm = 47;
  private float aux = 60f;
  private FlashLight flashLight;
  private GameController gameController;
  private Smartphone smartphone;

  // Use this for initialization
  void Start () {
    flashLight = GameObject.Find("flashlight").GetComponent<FlashLight>();
    gameController = GameObject.Find("GameController").GetComponent<GameController>();
	smartphone = GameObject.Find("Smartphone").GetComponent<Smartphone>();
    //gameOverText.SetActive(false);
    float percent = TotalTimeOfGameInMinutes * (MissingPercentage / 100); //percentage that lack of battery to the cell
    //Debug.Log(percent);
    //percent = 137.5f;
    timeLeft = (TotalTimeOfGameInMinutes - percent) * 60;
    if (percentageText != null) percentageText.text = (100 - MissingPercentage).ToString() + "%";

  }

  // Update is called once per frame
  void Update () {
    //Debug.Log(TotalTimeOfGameInMinutes * 60 - timeLeft);
	Clock();
    if (timeLeft > RedBatteryStartsInMinutes * 60) {

      RedBatteryImage.enabled = false;
      timeLeft -= Time.deltaTime * (flashLight.activateFlashlight ? 10 : 1) * (smartphone.IsDisplayed ? 5 : 1);
      FullBatteryImage.fillAmount = timeLeft / ((TotalTimeOfGameInMinutes) * 60 );
      updatePercentage();

    } else if (timeLeft > 0) { // Low battery

      RedBatteryImage.enabled = true;
      FullBatteryImage.enabled = false;
      timeLeft -= Time.deltaTime;
      RedBatteryImage.fillAmount = timeLeft / (TotalTimeOfGameInMinutes * 60 );
      updatePercentage();

    } else {
      //TIME OUT GAME OVER!!!
      GameOver();
    }
  }

  void GameOver() {
    gameController.GameOver();
    //gameOverText.SetActive(true);
    //Time.timeScale = 0;

  }

  void updatePercentage() {

    if (percentageText != null) percentageText.text = (Math.Round(this.timeLeft / ((this.TotalTimeOfGameInMinutes) * 60 ) * 100)).ToString() + "%";

  }
  
  void Clock(){
	  if(clock != null){
		  aux -= Time.deltaTime;
		  if(aux <= 0){
			  mm++;
			  aux = 60f;
		  }
		  if(mm == 60){
			  hh++;
			  mm = 0;
		  }
		  if(hh == 25){
			  hh = 0;
		  }
		  clock.text = "" + hh + ":" + mm.ToString("00");
	  }
	  
  }
}
