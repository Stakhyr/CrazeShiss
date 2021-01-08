 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    private int maxHealth = 10;

    public Texture2D fullHeart, halfHeart, emptyHeart;
    private Texture2D heart1, heart2, heart3, heart4, heart5;
    public Texture2D[] hearts;
    public Vector2 healthPos;

    public static bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        heart1 = fullHeart;
        heart2 = fullHeart;
        heart3 = fullHeart;
        heart4 = fullHeart;
        heart5 = fullHeart;

        hearts = new Texture2D[5];

        for(int i = 0; i < hearts.Length; i++) 
        {
            hearts[i] = fullHeart;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HealthMonitor();
    }

    void HealthMonitor() 
    {
        if (health == maxHealth) 
        {
            heart1 = fullHeart;
            heart2 = fullHeart;
            heart3 = fullHeart;
            heart4 = fullHeart;
            heart5 = fullHeart;
        }

        if (health == maxHealth -1)
        {
            heart1 = fullHeart;
            heart2 = fullHeart;
            heart3 = fullHeart;
            heart4 = fullHeart;
            heart5 = halfHeart;
        }

        if (health == maxHealth - 2)
        {
            heart1 = fullHeart;
            heart2 = fullHeart;
            heart3 = fullHeart;
            heart4 = fullHeart;
            heart5 = emptyHeart;
        }

        if (health == maxHealth - 3)
        {
            heart1 = fullHeart;
            heart2 = fullHeart;
            heart3 = fullHeart;
            heart4 = halfHeart;
            heart5 = emptyHeart;
        }

        if (health == maxHealth - 4)
        {
            heart1 = fullHeart;
            heart2 = fullHeart;
            heart3 = fullHeart;
            heart4 = emptyHeart;
            heart5 = emptyHeart;
        }

        if (health == maxHealth - 5)
        {
            heart1 = fullHeart;
            heart2 = fullHeart;
            heart3 = halfHeart;
            heart4 = emptyHeart;
            heart5 = emptyHeart;
        }

        if (health == maxHealth - 6)
        {
            heart1 = fullHeart;
            heart2 = fullHeart;
            heart3 = emptyHeart;
            heart4 = emptyHeart;
            heart5 = emptyHeart;
        }

        if (health == maxHealth - 7)
        {
            heart1 = fullHeart;
            heart2 = halfHeart;
            heart3 = emptyHeart;
            heart4 = emptyHeart;
            heart5 = emptyHeart;
        }

        if (health == maxHealth - 8)
        {
            heart1 = fullHeart;
            heart2 = emptyHeart;
            heart3 = emptyHeart;
            heart4 = emptyHeart;
            heart5 = emptyHeart;
        }

        if (health == maxHealth - 9)
        {
            heart1 = halfHeart;
            heart2 = emptyHeart;
            heart3 = emptyHeart;
            heart4 = emptyHeart;
            heart5 = emptyHeart;
        }

        if (health <= 0)
        {
            heart1 = emptyHeart;
            heart2 = emptyHeart;
            heart3 = emptyHeart;
            heart4 = emptyHeart;
            heart5 = emptyHeart;

            EndGame();
        }
    }

    void Damage() 
    {
        health -= 1;
    }
    void EndGame() 
    {
        isDead = true;
    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(healthPos.x, healthPos.y, 250, 50));
        GUI.DrawTexture(new Rect(0, 0, 50, 50), heart1, ScaleMode.ScaleToFit);
        GUI.DrawTexture(new Rect(50, 0, 50, 50), heart2, ScaleMode.ScaleToFit);
        GUI.DrawTexture(new Rect(100, 0, 50, 50), heart3, ScaleMode.ScaleToFit);
        GUI.DrawTexture(new Rect(150, 0, 50, 50), heart4, ScaleMode.ScaleToFit);
        GUI.DrawTexture(new Rect(200, 0, 50, 50), heart5, ScaleMode.ScaleToFit);

        GUI.EndGroup();
    }
}
