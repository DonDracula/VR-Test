﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public bool isGameOver;
	public Text scoreTxt;
	public Text gameOverTxt;
	public Canvas gameOverCanvas;
    //VR
    public Canvas VRGameOverCanvas;
    public Text VRGameOverTxt;
    //VR text
    public Text VRScoreTxt;

    private int _currScore;
	private int _scoreToWin = 20;
	private bool _didIWin;

	/// <summary>
	/// Start a new game.
	/// </summary>
	void NewGame() {
		ResetGame();
	}
	

	/// <summary>
	/// Got an enemy! Increment the score and see if we win.
	/// </summary>
	public void GotOne() {
		_currScore++;
		scoreTxt.text =  "" + _currScore;
        VRScoreTxt.text = "" + _currScore;

        if (_currScore >= _scoreToWin) {
			GameOver(true);
		}
	}

    /// <summary>
    /// Game's over. 
    /// </summary>
    /// <param name="didIWin">Did the playeer win?</param>	
    /// VR
    public void GameOver(bool didIWin)
    {
        isGameOver = true;
        _didIWin = didIWin;
        string finalTxt = (_didIWin) ? "You won!" : "Too bad";
        if (GvrViewer.Instance.VRModeEnabled)
        {
            VRGameOverCanvas.enabled = true;
            VRGameOverTxt.text = finalTxt;
        }
        else
        {
            gameOverCanvas.enabled = true;
            gameOverTxt.text = finalTxt;
        }
    }


    /// <summary>
    /// Resets the interface, removes remaining game objects. Basically gets us to a point
    /// where we're ready to play again.
    /// </summary>
    public void ResetGame() {
        //VR
        VRGameOverCanvas.enabled = false;
        // Reset the interface
        gameOverCanvas.enabled = false;
		isGameOver = false;
		_currScore = 0;
		scoreTxt.text = "--";
        VRScoreTxt.text = "--";

        // Remove any remaining game objects
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
        	Destroy(enemy);
        }
        
        GameObject[] ninjaStars = GameObject.FindGameObjectsWithTag("NinjaStar");
        foreach (GameObject ninjaStar in ninjaStars) {
        	Destroy (ninjaStar);
        }
	}

    public void RefreshGameOver()
    {
        gameOverCanvas.enabled = false;
        VRGameOverCanvas.enabled = false;
        if (isGameOver)
        {
            GameOver(_didIWin);
        }
    }

    void Start () {
		NewGame();
	}


}
