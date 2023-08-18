using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;    // Mirar cuando tengamos musica si nos interesa que se baje un poco, se pare o lo que sea cuando esta variable este a 'true'. Mirar el chat de referencias del canal Programacion.

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                ResumeFunction();
            }
            else
            {
                PauseFunction();
            }
        }
        
    }


    void ResumeFunction()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void PauseFunction()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void SaveGameFunction()
    {
        Debug.Log("Guardando partida...");
    }

    public void LoadGameFunction()
    {
        Debug.Log("¿Qué partida quieres cargar...?");
    }

    public void QuitGameFunction()
    {
        Debug.Log("Cerrando el juego...");
        Application.Quit();
    }

}
