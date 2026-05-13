using UnityEngine;
using UnityEngine.UI;



public class BossStartGround : MonoBehaviour
{
    bool activated = false;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !activated)
        {
            Boss.Instance.healthSlider.gameObject.SetActive(true);
            
            SoundManager.instance.ChangeBackgroundMusic(SoundManager.instance.bossMusic);
            
            activated = true;
        }
    }
}
