using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public GameObject mainMenuHolder;
	public GameObject optionsMenuHolder;
	public Slider[] volumeSliders;

	void Start() {
		volumeSliders [0].value = AudioManager.instance.musicVolumePercent;
		volumeSliders [1].value = AudioManager.instance.sfxVolumePercent;

	}


	public void Play() {
		SceneManager.LoadScene ("Game");
	}

	public void Quit() {
		Application.Quit ();
	}

	public void OptionsMenu() {
		mainMenuHolder.SetActive (false);
		optionsMenuHolder.SetActive (true);
	}

	public void MainMenu() {
		mainMenuHolder.SetActive (true);
		optionsMenuHolder.SetActive (false);
	}

	public void SetMusicVolume(float value) {
		AudioManager.instance.SetVolume (value, AudioManager.AudioChannel.Music);
	}

	public void SetSfxVolume(float value) {
		AudioManager.instance.SetVolume (value, AudioManager.AudioChannel.Sfx);
	}

	public void WatchSeries() {
		Application.OpenURL ("https://www.youtube.com/watch?v=4sevDqyUquM&list=PLTihuHlzTc0vXfTwqz2sTAjaG0vbUqOVN&ab_channel=Joose");
	}
}
