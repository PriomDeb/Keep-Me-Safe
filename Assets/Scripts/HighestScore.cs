using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighestScore : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text highestScore;

    void Start()
    {
        highestScore.text = "Start The Match! My Death is Only The Beginning of a New Game!";
    }
}
