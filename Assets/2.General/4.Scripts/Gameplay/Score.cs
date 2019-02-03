using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoSingleton<Score>
{

    /* Summary: This script track score which player acquires during the gameplay. */

    [HideInInspector]
    public float score;

    private float distanceScore;

    public float scoreMultiplier;

    public TextMeshProUGUI scoreText;

    public Transform player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        UpdateScore();

    }

    private void UpdateScore()
    {
        if (player.position.y > distanceScore)
        {
            distanceScore = player.position.y;
        }




        score = distanceScore * scoreMultiplier;


        scoreText.text = score.ToString("0");
    }
}
