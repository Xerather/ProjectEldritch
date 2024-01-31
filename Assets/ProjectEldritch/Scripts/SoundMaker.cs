using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour
{
	public List<AudioSource> audioClipList = new List<AudioSource>();

	public void PlaySfx(string targetName)
	{
		AudioSource source = audioClipList.Find((x) => x.gameObject.name == targetName);
		if (source == null) return;
		source.Play();
	}
}
