using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SliderUi : MonoBehaviour
{
    [SerializeField] Slider playerSlidebar;

    // Start is called before the first frame update
    void Start()
    {
        EventManagerGTA.AddPlayerEventListenr(playerHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playerHealth(int damage)
    {
        playerSlidebar.value = damage;
        Debug.Log($"Updating player health slider to: {damage}");

    }
    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
    }
}
