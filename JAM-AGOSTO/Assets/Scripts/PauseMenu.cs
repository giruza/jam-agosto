using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;    // Mirar cuando tengamos musica si nos interesa que se baje un poco, se pare o lo que sea cuando esta variable este a 'true'. Mirar el chat de referencias del canal Programación.

    public GameObject pauseMenuUI;

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

    // Desactivamos la pantalla de menú, la velocidad del juego vuelve a la normal y ponemos el flag a false
    void ResumeFunction()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    // Activamos la pantalla de menú, el juego se congela (su velocidad pasa a 0) y ponemos el flag a true, que posteriormente comprobaremos en métodos como PruebaEnemy.cs
    void PauseFunction()
    {
        //InventoryManager inventoryManager = GetComponent<InventoryManager>();   // Creamos una instancia de InventoryManager para poder acceder desde este metodo, que es estático
        //inventoryManager.ListItems();    // Llamamos a la funcion de InventoryManager.cs
        InventoryManager.Instance.ListItems();    // Llamamos a la funcion de InventoryManager.cs

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    // Función de guardar la partida. De momento esta con un texto, pero hay que ver como formar un archivo de guardado y todo, aunque supongo que ya para el final del desarrollo. Asociada al botón "Save Game Button" del submenú "Game Menu".
    public void SaveGameFunction()
    {
        Debug.Log("Guardando partida...");
    }

    // Lo mismo que con la función SaveGameFunction(). Asociada al botón "Load Game Button" del submenú "Game Menu".
    public void LoadGameFunction()
    {
        Debug.Log("¿Qué partida quieres cargar...?");
    }

    // Es funcional, aunque desde solo desde el juego exportado, así que también imprime por consola un texto para demostrar que se ejecuta. Asociada al botón "Quit Game Button" del submenú "Game Menu".
    public void QuitGameFunction()
    {
        Debug.Log("Cerrando el juego...");
        Application.Quit();
    }

}
