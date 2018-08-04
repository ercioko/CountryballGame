using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	public GameObject menu, gameplay, ball, store, character;
	public AudioSource ambient, gameMusic;
	public Image gameUI;
	public Text musicText;
	bool pause, musicOff;
	
	private void Start() {
		ExitToMenu();
		PlayerData.Load();
	}
	private void Update() {
		PlayerData.Save();
	}
	public void CharacterMenu(){
		character.SetActive(true);
		menu.SetActive(false);
	}
	public void Store() {
    	store.SetActive(true);
		menu.SetActive(false);
 	}
	public void StartGame(){
		ambient.Stop();
		Screen.sleepTimeout = 600;

		menu.SetActive(false);
		gameplay.SetActive(true);

		PlayerData.score = 0;
		PlatformCreator.Reset();
		gameMusic.Play();
		ball.GetComponent<AudioSource>().volume = 0.05f;
	}
	public void PauseGame(){
		pause=!pause;
		if(pause){
			Time.timeScale = 0;
		}else{
			Time.timeScale = 1;
		}
	}

	public void ExitToMenu(){
		gameMusic.Stop();
		ambient.Play();
		gameUI.color = new Color(1f,1f,1f,0f);
		gameplay.GetComponent<Backround>().Reset();
		ball.GetComponent<Ball>().Reset();
		ball.GetComponent<AudioSource>().volume = 0f;
		Screen.sleepTimeout = SleepTimeout.SystemSetting;

		character.SetActive(false);
		store.SetActive(false);
		gameplay.SetActive(false);
		menu.SetActive(true);
	}
	public void MusicOff(){
		musicOff = !musicOff;
		if(musicOff){
			gameMusic.volume = 0;
			ambient.volume = 0;
			ambient.Pause();
			musicText.text = "Music OFF";
		}else{
			gameMusic.volume = 0.8f;
			ambient.volume = 0.5f;
			ambient.Play();
			musicText.text = "Music ON";
		}
	}
}
