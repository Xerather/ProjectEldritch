using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
	public void PlayTutorial()
	{

		SceneManager.LoadScene(1);
	}
}
