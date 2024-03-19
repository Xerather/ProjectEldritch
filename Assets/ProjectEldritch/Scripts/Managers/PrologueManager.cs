using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueManager : MonoBehaviour
{

	public void PlayTutorial()
	{
		SceneManager.LoadScene(2);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PlayTutorial();
		}
	}
}
