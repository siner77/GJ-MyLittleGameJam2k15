﻿using UnityEngine;
using System.Collections;

public class Device : MonoBehaviour {

    private bool isWorking;

    private GameObject player;

    public bool IsWorking
    {
        get
        {
            return isWorking;
        }
        set
        {
            isWorking = value;
        }
    }

	// Use this for initialization
	void Start () {
        isWorking = false;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && !isWorking)
        {
            MinigameManager.LaunchMinigame();
            MinigameManager.OnMinigameWin += Win;
            MinigameManager.OnMinigameLost += Lose;
            player = col.gameObject;
            player.GetComponent<PlayerController>().enabled = false;
        }
    }

    void Win()
    {
        isWorking = true;
        GameController.Instance.FixedItems += 1;
        GameController.Instance.SomethingFixed();
        MinigameManager.OnMinigameWin -= Win;
        MinigameManager.OnMinigameLost -= Lose;
        player.GetComponent<PlayerController>().enabled = true;
        player = null;
    }

    void Lose()
    {
        isWorking = false;
        GameController.Instance.SomethingNotFixed();
        MinigameManager.OnMinigameWin -= Win;
        MinigameManager.OnMinigameLost -= Lose;
        player.GetComponent<PlayerController>().enabled = true;
        player = null;
    }
}
