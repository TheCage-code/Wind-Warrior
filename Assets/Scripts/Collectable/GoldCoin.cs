using UnityEngine;

public class GoldCoin : MonoBehaviour
{

    public float coinAmount = 10f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {          
                CoinBank.instance.coinBank += coinAmount;
                Destroy(gameObject);
            
        }
    }
}
