using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinBank : MonoBehaviour
{
    public static CoinBank instance;
    public float coinBank;
    public TextMeshProUGUI coinTxt;


    private void Awake()
    {
        if(instance == null) { instance = this; }
    }

    public void Update()
    {
        if (coinTxt != null)
        {
            coinTxt.SetText(coinBank.ToString());
        }
    }
}
