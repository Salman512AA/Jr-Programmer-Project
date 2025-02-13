using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUi : MonoBehaviour
{
    [SerializeField]
    TMP_InputField playerName;

    void Start()
    {
        // Set the placeholder text initially

        // Add listener for the onEndEdit event
        playerName.onEndEdit.AddListener(SaveName);
    }
    void SaveName(string name)
    {

        // Assuming you're saving to a Singleton Manager
        if (MainManager.instance != null)
        {
            MainManager.instance.UserName = name;
        }
    }
        public void StartButton()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
  Application.Quit();
#endif
    }

}
