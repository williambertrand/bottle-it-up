using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public int humansEaten;
    public static GameController Instance;
    public int MAX_HUMANS_EATEN = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHumanEaten()
    {
        humansEaten += 1;

        if (humansEaten >= MAX_HUMANS_EATEN)
        {
            GameOver();
        }

    }

    void GameOver()
    {
        Debug.Log("GAME OVER");
    }
}
