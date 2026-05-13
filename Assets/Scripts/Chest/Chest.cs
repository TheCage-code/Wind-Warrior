using NUnit.Framework.Internal;
using UnityEngine;

public class Chest : MonoBehaviour
{

    [SerializeField] GameObject[] items;
    [SerializeField] Transform coinSpawnPoint;
    Animator anim;
    GameObject itemToSpawn;


    private bool isOpened = false;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }



   public void SelectRandomItem()
    {
       int randomItemIndex = Random.Range(0, items.Length);
       itemToSpawn = items[randomItemIndex];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && !isOpened)
        {
            SelectRandomItem();
            isOpened = true;

            anim.SetTrigger("Open");
            Instantiate(itemToSpawn, coinSpawnPoint.position, Quaternion.identity);
        }
        
    }
}