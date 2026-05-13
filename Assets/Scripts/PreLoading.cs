using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreLoading : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(NextScene());
    }

    
    


    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("MainMenu");

    }

    
}
