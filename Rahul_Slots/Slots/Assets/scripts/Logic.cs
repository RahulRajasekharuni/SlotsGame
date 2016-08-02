using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour {
    //variables for the various sprites
    public GameObject Bell;
    public GameObject LuckySeven;
    public GameObject Grapes;
    public GameObject Gold;
    public GameObject Cherry;
    //audio variables
    public AudioClip Spin;
    private AudioSource source;
    //Vector2 and Gameobject 2D array are used to store the location and the sprites on the slot machine grid
    public Vector2[,] Grid = new Vector2[3,3];
    public GameObject[,] Sprites = new GameObject[3,3];
    public Text Score;
    public Text CoinText;
    private int TotalScore = 0;
    private int Coins = 0;

    void Start()
    {
        Coins = 10;
        //add vector2 position of each spot I want the sprites to appear
        Grid[0, 0] = new Vector2(-4, 3.5f);
        Grid[0, 1] = new Vector2(-4, 1);
        Grid[0, 2] = new Vector2(-4, -1.4f);
        Grid[1, 0] = new Vector2(0, 3.5f);
        Grid[1, 1] = new Vector2(0, 1);
        Grid[1, 2] = new Vector2(0, -1.4f);
        Grid[2, 0] = new Vector2(3.5f, 3.5f);
        Grid[2, 1] = new Vector2(3.5f, 1);
        Grid[2, 2] = new Vector2(3.5f, -1.4f);

        //These variables are used to generate a random number, which is used to generate one of the 5 sprites.
        int SpriteGenerator = 0;
        int SpriteChecker = 0;
        int SpriteChecker2 = 0;
        
        //nested for loop to initialise the postion of each sprite on the slot machine game grid.
        for (int CounterY = 0; CounterY < 3; CounterY++)
        {
            do
            {
                SpriteGenerator = UnityEngine.Random.Range(1, 6);
            } while (SpriteChecker == SpriteGenerator | SpriteChecker2 == SpriteGenerator);
            if (SpriteChecker == 0)
            {
                SpriteChecker = SpriteGenerator;
            }
            else
            {
                SpriteChecker2 = SpriteGenerator;
            }
            for (int CounterX = 0; CounterX < 3; CounterX++)
            {
                if (SpriteGenerator == 1)
                {
                    Sprites[CounterX, CounterY] = Instantiate(LuckySeven, Grid[CounterX, CounterY], Quaternion.identity) as GameObject;
                }
                else if (SpriteGenerator == 2)
                {
                    Sprites[CounterX, CounterY] = Instantiate(Bell, Grid[CounterX, CounterY], Quaternion.identity) as GameObject;
                }
                if (SpriteGenerator == 3)
                {
                    Sprites[CounterX, CounterY] = Instantiate(Grapes, Grid[CounterX, CounterY], Quaternion.identity) as GameObject;
                }
                if (SpriteGenerator == 4)
                {
                    Sprites[CounterX, CounterY] = Instantiate(Gold, Grid[CounterX, CounterY], Quaternion.identity) as GameObject;
                }
                if (SpriteGenerator == 5)
                {
                    Sprites[CounterX, CounterY] = Instantiate(Cherry, Grid[CounterX, CounterY], Quaternion.identity) as GameObject;
                 
                }
            }
        }
    }

	void Update () {
        CoinText.text = Coins.ToString();
        if(Coins < 1)
        {
            SceneManager.LoadScene("GameOver");
        }
        
    }

    public void Scroll()
    {
        source = GetComponent<AudioSource>();
        source.Play();

        int SpriteGenerator = 0;
        int[,] SpriteValue = new int[3,3];

        //similar nested loop to above, however no checker is used. This allows for a completely random game grid.
        for (int CounterY = 0; CounterY < 3; CounterY++)
        {
            for (int CounterX = 0; CounterX < 3; CounterX++)
            {
                SpriteGenerator = UnityEngine.Random.Range(1, 6);
                if (SpriteGenerator == 1)
                {
                    Destroy(Sprites[CounterX, CounterY]);
                    Sprites[CounterX, CounterY] = Instantiate(LuckySeven, Grid[CounterX, CounterY], Quaternion.identity) as GameObject;
                    SpriteValue[CounterX, CounterY] = 1;
                }
                else if (SpriteGenerator == 2)
                {
                    Destroy(Sprites[CounterX, CounterY]);
                    Sprites[CounterX, CounterY] = Instantiate(Bell, Grid[CounterX, CounterY], Quaternion.identity) as GameObject;
                    SpriteValue[CounterX, CounterY] = 2;
                }
                if (SpriteGenerator == 3)
                {
                    Destroy(Sprites[CounterX, CounterY]);
                    Sprites[CounterX, CounterY] = Instantiate(Grapes, Grid[CounterX, CounterY], Quaternion.identity) as GameObject;
                    SpriteValue[CounterX, CounterY] = 3;
                }
                if (SpriteGenerator == 4)
                {
                    Destroy(Sprites[CounterX, CounterY]);
                    Sprites[CounterX, CounterY] = Instantiate(Gold, Grid[CounterX, CounterY], Quaternion.identity) as GameObject;
                    SpriteValue[CounterX, CounterY] = 4;
                }
                if (SpriteGenerator == 5)
                {
                    Destroy(Sprites[CounterX, CounterY]);
                    Sprites[CounterX, CounterY] = Instantiate(Cherry, Grid[CounterX, CounterY], Quaternion.identity) as GameObject;
                    SpriteValue[CounterX, CounterY] = 5;

                }
                //ensures every spot on the grid has a randomized sprite
                if (CounterX == 2 && CounterY < 3)
                {
                    CounterX = 0;
                    break;
                }
            }
        }
        //Calculate score if three sprites in the middle row are the same then +500, otherwise if 2 sprites in the middle row are the same +20.
        if (SpriteValue[0,1] == SpriteValue[1,1] && SpriteValue[1, 1] == SpriteValue[2,1])
        {
            TotalScore += 500;
            Coins += 2;
        }
        if (SpriteValue[0, 1] == SpriteValue[1, 1] | SpriteValue[1, 1] == SpriteValue[2, 1] | SpriteValue[0, 1] == SpriteValue[2, 1])
        {
            TotalScore += 20;
        }
        Score.text = TotalScore.ToString();
        Coins -= 1;
    }
  }

