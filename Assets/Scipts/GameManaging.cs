using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManaging : MonoBehaviour
{
    [SerializeField] private float restartDelay = 2f;
    bool gameHasEnded = false;
   public void endGame() 
    {
        if (gameHasEnded == false) 
        { 
            gameHasEnded = true;
            Debug.Log("End game");
            Invoke("Restart", restartDelay);
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
