using UnityEngine;

public class UpgradePaper : MonoBehaviour
{
    public GameObject upgradePanel;
    public GameObject upgradePaper;
    public Transform uiCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            upgradePaper.SetActive(false);
            GameObject newPanel = Instantiate(upgradePanel, uiCanvas);
            upgradePanel.SetActive(true);
            newPanel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            Time.timeScale = 0f;

        }
    }

    public void GiveAp()
    {

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            PlayerCombat player = playerObj.GetComponent<PlayerCombat>();
            player.damage += 10f;
            upgradePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void GiveHp()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            PlayerHealth playerHealth = playerObj.GetComponent<PlayerHealth>();
            playerHealth.currentHealth += 30;
            upgradePanel.SetActive(false);
            Time.timeScale = 1f;
            playerHealth.UpdateUI();
        }
    }
}
