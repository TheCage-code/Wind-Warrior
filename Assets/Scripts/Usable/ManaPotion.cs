using UnityEngine;

public class ManaPotion : MonoBehaviour
{
    [SerializeField] float giveMana = 20f;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().currentMana += giveMana;
            Destroy(gameObject);
            collision.GetComponent<PlayerHealth>().UpdateUI();
        }



    }
}
