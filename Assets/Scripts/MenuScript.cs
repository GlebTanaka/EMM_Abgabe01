using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject Menu;

    public static bool IsPaused;
    // Start is called before the first frame update
    void Start()
    {
        Menu.SetActive(false);
    }

    public void PauseGame()
    {
        Menu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;

    }

    public void ResumeGame()
    {
        Menu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
            
        }
        
    }
    
    
}
