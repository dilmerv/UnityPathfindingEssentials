using System.Collections;
using System.Collections.Generic;
using DilmerGames.Core.Singletons;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class Score : Singleton<Score>
{
    private TextMeshPro scoreText;

    private int miniDude = 0;

    private int bigDude = 0;

    private void Start() 
    {
        scoreText = GetComponent<TextMeshPro>();
    }

    public void updateScore(int miniDude, int bigDude)
    {
        this.miniDude += miniDude;
        this.bigDude += bigDude;
        scoreText.text = $"Mini Dudes: \t{this.miniDude}\nBig Dudes: \t{this.bigDude}";
    }
}
