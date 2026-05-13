using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public float speed = 3f;

    int i;

    private void Start()
    {
        
        foreach (Transform p in points)
        {
            p.SetParent(null);
        }
    }

    private void Update()
    {
        
        float distance = Vector2.Distance(transform.position, points[i].position);

       
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);

        
        if (distance < 0.1f)
        {
            i++;
            if (i >= points.Length)
            {
                i = 0;
            }
        }
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
          

            
            collision.transform.SetParent(transform);

            
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}