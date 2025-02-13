using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour {
	public void AboutUs()
	{
		SceneManager.LoadScene("Controls");
	}
	public void StartGame()
	{
		SceneManager.LoadScene("Game");
	}
	public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
	public void Back()
	{
		SceneManager.LoadScene("Menu 3D");
	}
}
