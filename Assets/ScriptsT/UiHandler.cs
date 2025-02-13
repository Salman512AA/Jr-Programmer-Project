using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class UiHandler : MonoBehaviour
{
    [SerializeField] Slider playerSlidebar;
    [SerializeField] Slider EnemySlidebar;
    [SerializeField] TextMeshProUGUI Text;

    private void Awake()
    {
        EventManager.AddEnemyEventListenr(enemyHealth);
        EventManager.AddPlayerEventListenr(playerHealth);
        
    }
    private void Start()
    {
        if (MainManager.instance != null)
        {
            string userName = MainManager.instance.UserName;  // Assuming this gets the player’s name from the MainManager instance
            string tankName = "AI_Tank";// You can modify this as needed

            
            Text.text = "<color=#00FF00>" + userName + "</color> <color=#B0B0B0>Vs</color> <color=#FF0000>" + tankName + "</color>";
        }
    }
    public void playerHealth(int damage)
    {
        playerSlidebar.value = damage;
        Debug.Log($"Updating player health slider to: {damage}");

    }
    public void enemyHealth(int damage) { 
       EnemySlidebar.value = damage;
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(1);
    }
}
