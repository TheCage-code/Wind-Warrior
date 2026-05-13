using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{
   
    public AbilityData ability;
    public Slider cooldownSlider;
    public TextMeshProUGUI timerText;
   

    private float currentCooldown;
    private bool isCooldown = false;

    void Start()
    {
        
        if (ability != null) 
        {
            cooldownSlider.maxValue = ability.cooldown;
            cooldownSlider.value = 0;
            timerText.gameObject.SetActive(false);
        }
    }

    public void StartCooldownUI()
    {
        isCooldown = true;
        currentCooldown = ability.cooldown;
        timerText.gameObject.SetActive(true);
    }

    void Update()
    {
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;
            cooldownSlider.value = currentCooldown;
            timerText.text = Mathf.Ceil(currentCooldown).ToString();

            if (currentCooldown <= 0)
            {
                isCooldown = false;
                cooldownSlider.value = 0;
                timerText.gameObject.SetActive(false);
            }
        }
    }
}