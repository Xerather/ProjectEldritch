using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
	public static SoundManager that;
	[SerializeField] private AudioMixer audioMixer;
	[SerializeField] private AudioSource sfxSource;
	[SerializeField] private AudioSource voiceSource;
	[SerializeField] private AudioSource newBgmSrc, lastBgmSrc;
	[SerializeField] private AudioSource newAmbSrc, lastAmbSrc;
	[Space(10)]
	public AudioClip useSfx;

	[Header("Event Ilustration")]
	public AudioClip eventBar;
	public AudioClip eventFlair, eventCharm, eventBordello, eventBrothel, eventAngelic, eventDemonic, eventPark;

	private bool isFadingBgm, isFadingAmb, isStoppingBgm;
	public static AudioClip currentBgm;
	public Dictionary<AudioClip, int> bgmTimekeeper;

	private string lastBgmParam = "LastMusicVol";
	private string newBgmParam = "NewMusicVol";

	private void Awake()
	{
		that = this;
		bgmTimekeeper = new Dictionary<AudioClip, int>();
	}

	// Use this for initialization
	private void Start()
	{

	}

	// Update is called once per frame
	private void Update()
	{
		if (isFadingBgm)
		{
			// float vol = lastBgmSrc.volume;
			// vol -= Time.deltaTime * 0.5f;
			// if (vol < 0) vol = 0;
			// lastBgmSrc.volume = vol;
			// newBgmSrc.volume = (1 - vol);

			float vol;
			audioMixer.GetFloat(lastBgmParam, out vol);
			vol = Mathf.Pow(10, vol / 20);
			vol -= Time.deltaTime * 0.5f;
			vol = Mathf.Clamp(vol, 0.0001f, 1);
			audioMixer.SetFloat(lastBgmParam, Mathf.Log10(vol) * 20);
			audioMixer.SetFloat(newBgmParam, Mathf.Log10(1 - vol) * 20);

			if (vol == 0.0001f)
			{
				isFadingBgm = false;
				lastBgmSrc.Stop();

				AudioSource temp = lastBgmSrc;
				lastBgmSrc = newBgmSrc;             // Swap the sources so new requests will always assigned to "newSrc"
				newBgmSrc = temp;

				string tempParam = lastBgmParam;
				lastBgmParam = newBgmParam;
				newBgmParam = tempParam;
			}
		}

		if (isFadingAmb)
		{
			float vol = lastAmbSrc.volume;
			vol -= Time.deltaTime * 0.5f;
			if (vol < 0) vol = 0;
			lastAmbSrc.volume = vol;
			newAmbSrc.volume = (0.5f - vol);
			if (vol == 0)
			{
				isFadingAmb = false;
				lastAmbSrc.Stop();

				AudioSource temp = lastAmbSrc;
				lastAmbSrc = newAmbSrc;
				newAmbSrc = temp;
			}
		}
		if (isStoppingBgm)
		{
			float vol;
			audioMixer.GetFloat(lastBgmParam, out vol);
			vol = Mathf.Pow(10, vol / 20);
			vol -= Time.deltaTime * 0.5f;
			vol = Mathf.Clamp(vol, 0.0001f, 1);
			audioMixer.SetFloat(lastBgmParam, Mathf.Log10(vol) * 20);

			if (vol == 0.0001f)
			{
				isStoppingBgm = false;
				lastBgmSrc.Stop();
			}
		}
	}

	public static void MuteVoice(bool isMute)
	{
		if (that != null) that.voiceSource.mute = isMute;
	}

	public static void PlayVoice(AudioClip voice)
	{
		if (voice != null) that.voiceSource.PlayOneShot(voice);
	}

	public static void PlaySfx(AudioClip sfx)
	{
		if (sfx != null) that.sfxSource.PlayOneShot(sfx);
	}


	public static void PlayAmb(AudioClip amb)
	{
		// On ambience, there is no need to track the last played ambience.
		if (!that.lastAmbSrc.isPlaying)
		{
			that.lastAmbSrc.clip = amb;
			that.lastAmbSrc.loop = true;
			that.lastAmbSrc.volume = 1;
			that.lastAmbSrc.Play();
		}
		else
		{
			that.isFadingBgm = true;
			that.newAmbSrc.clip = amb;
			that.newAmbSrc.loop = true;
			that.newAmbSrc.volume = 0;
			that.newAmbSrc.Play();
		}
	}

	public static void StopAmb()
	{
		that.lastAmbSrc.Stop();
	}

	public static void PlayBGM(AudioClip bgm, bool isContinueLast = true)
	{
		if (bgm == null)
		{
			return;
		}
		if (currentBgm == bgm && that.lastBgmSrc.isPlaying) return;

		float lastVol;
		that.audioMixer.GetFloat(that.lastBgmParam, out lastVol);
		if (that.isFadingBgm && lastVol < 0.5f)
		{
			that.audioMixer.SetFloat(that.lastBgmParam, Mathf.Log10(0.0001f) * 20);
			// that.lastBgmSrc.volume = 0;
			that.lastBgmSrc.Stop();

			AudioSource temp = that.lastBgmSrc;
			that.lastBgmSrc = that.newBgmSrc;
			that.newBgmSrc = temp;

			string tempParam = that.lastBgmParam;
			that.lastBgmParam = that.newBgmParam;
			that.newBgmParam = tempParam;
		}
		that.isFadingBgm = true;

		int pcmSample = that.lastBgmSrc.timeSamples;
		if (that.lastBgmSrc.clip != null)
		{
			that.bgmTimekeeper[that.lastBgmSrc.clip] = pcmSample;
		}

		that.newBgmSrc.clip = bgm;
		that.newBgmSrc.volume = 1;

		if (bgm != null && that.bgmTimekeeper.ContainsKey(bgm)) pcmSample = that.bgmTimekeeper[bgm];
		else
		{
			if (bgm != null) that.bgmTimekeeper.Add(bgm, 0);
			pcmSample = 0;
		}

		that.newBgmSrc.timeSamples = 0;
		that.newBgmSrc.time = 0;
		that.newBgmSrc.Play();
		if (isContinueLast) that.newBgmSrc.timeSamples = pcmSample;
		else that.newBgmSrc.timeSamples = 0;

		currentBgm = bgm;
	}

	public static void StopBgm()
	{
		if (that == null) return;
		that.isStoppingBgm = true;
		// that.lastBgmSrc.Stop();
		// that.newBgmSrc.Stop();
	}
}
