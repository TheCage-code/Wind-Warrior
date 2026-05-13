using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] private GameObject rBtnPrefab; 
    [SerializeField] private GameObject btnPoint;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private AbilityData abilityToUpgrade;

    private GameObject currentButton;



    private bool isEnter = false;
    private bool isOpen = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isEnter)
        {
            if (!isOpen)
                OpenShop();
            else
                CloseShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && currentButton == null)
        {
            isEnter = true;
            currentButton = Instantiate(rBtnPrefab, btnPoint.transform.position, Quaternion.identity, transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
            if (currentButton != null)
            {
                Destroy(currentButton);
                currentButton = null; 
            }
            isEnter = false;
            CloseShop();
        }
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        isOpen = true;
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        isOpen = false;
    }

    public void BuyTornadoUpgradeButton()
    {
        if (abilityToUpgrade != null)
        {
            
           
            
            if(CoinBank.instance.coinBank < 10)
            {
                return;
            }
            CoinBank.instance.coinBank -= 10;
            abilityToUpgrade.damage += 20f;
            
        }
    }

}