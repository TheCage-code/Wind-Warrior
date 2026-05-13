using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private PlayerHealth player;



    private void Awake()
    {
        if(instance == null)
        {
            instance = this; 
            DontDestroyOnLoad(gameObject);
            

        }
        else
        {
            Destroy(gameObject);
        }
    }
   


    public void SetPlayer(PlayerHealth playerScript)
    {
        player = playerScript;

        //LoadPlayerData();
    }
    //public void SavePlayerData()
    //{
    //    if (player != null)
    //    {
    //        PlayerPrefs.SetFloat("SavedHealth", player.currentHealth);
    //        PlayerPrefs.SetFloat("SavedMana", player.currentMana);
    //        PlayerPrefs.Save();
            
            
    //    }
    //}
    //public void LoadPlayerData()
    //{
    //    if (player != null)
    //    {
    //        player.currentHealth = PlayerPrefs.GetFloat("SavedHealth", player.currentHealth);
    //        player.currentMana = PlayerPrefs.GetFloat("SavedMana", player.currentMana);
    //        player.UpdateUI();
    //    }
       
    //}

    public void RestartLevel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}
