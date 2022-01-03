using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public enum AudioChannel {Sfx, Music};

	public float sfxVolumePercent { get; private set; }
	public float musicVolumePercent { get; private set; }

	AudioSource sfx2DSource;
	AudioSource[] musicSources;
	int activeMusicSourceIndex;

	public static AudioManager instance;

	Transform audioListener;
	Transform playerT;

	SoundLibrary library;

	void Awake() {
		if (instance != null) {
			Destroy (gameObject);
		} else {

			instance = this;
			DontDestroyOnLoad (gameObject);

			library = GetComponent<SoundLibrary> ();

			musicSources = new AudioSource[2];
			for (int i = 0; i < 2; i++) {
				GameObject newMusicSource = new GameObject ("Music source " + (i + 1));
				musicSources [i] = newMusicSource.AddComponent<AudioSource> ();
				newMusicSource.transform.parent = transform;
			}
			GameObject newSfx2Dsource = new GameObject ("2D sfx source");
			sfx2DSource = newSfx2Dsource.AddComponent<AudioSource> ();
			newSfx2Dsource.transform.parent = transform;

			audioListener = FindObjectOfType<AudioListener> ().transform;
			if (FindObjectOfType<Player> () != null) {
				playerT = FindObjectOfType<Player> ().transform;
			}

			sfxVolumePercent = PlayerPrefs.GetFloat ("sfx vol", 1);
			musicVolumePercent = PlayerPrefs.GetFloat ("music vol", 1);
		}
	}

	void OnLevelWasLoaded(int index) {
		if (playerT == null) {
			if (FindObjectOfType<Player> () != null) {
				playerT = FindObjectOfType<Player> ().transform;
			}
		}
	}
// Listener follows the player
	void Update() {
		if (playerT != null) {
			audioListener.position = playerT.position;
		}
	}

	public void SetVolume(float volumePercent, AudioChannel channel) {
		switch (channel) {
		case AudioChannel.Sfx:
			sfxVolumePercent = volumePercent;
			break;
		case AudioChannel.Music:
			musicVolumePercent = volumePercent;
			break;
		}

		PlayerPrefs.SetFloat ("sfx vol", sfxVolumePercent);
		PlayerPrefs.SetFloat ("music vol", musicVolumePercent);
		PlayerPrefs.Save ();
	}

	public void PlayMusic(AudioClip clip, float fadeDuration = 1) {
		activeMusicSourceIndex = 1 - activeMusicSourceIndex;
		musicSources [activeMusicSourceIndex].clip = clip;
		musicSources [activeMusicSourceIndex].Play ();

		StartCoroutine(AnimateMusicCrossfade(fadeDuration));
	}
// Plays if audiofile != null
	public void PlaySound(AudioClip clip, Vector3 pos) {
		if (clip != null) {
			AudioSource.PlayClipAtPoint (clip, pos, sfxVolumePercent);
		}
	}

	public void PlaySound(string soundName, Vector3 pos) {
		PlaySound (library.GetClipFromName (soundName), pos);
	}

	public void PlaySound2D(string soundName) {
		sfx2DSource.PlayOneShot (library.GetClipFromName (soundName), sfxVolumePercent);
	}


	IEnumerator AnimateMusicCrossfade(float duration) {
		float percent = 0;

		while (percent < 1) {
			percent += Time.deltaTime * 1 / duration;
			musicSources [activeMusicSourceIndex].volume = Mathf.Lerp (0, musicVolumePercent, percent);
			musicSources [1-activeMusicSourceIndex].volume = Mathf.Lerp (musicVolumePercent, 0, percent);
			yield return null;
		}
	}
}
