using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public int points;
    public int autoClickLevel;

    public TMP_Text pointsText;

    public float timeTillClick;
    public float clickRate;
    public float clickRateIncrease;


    void Start()
    {
        points = 0;
        autoClickLevel = 1;

        clickRate = 1f;
        timeTillClick = clickRate;
        clickRateIncrease = .0001f;
    }

    void Update()
    {    
        if (timeTillClick <= 0)
        {
            points += 1;     
            clickRate -= clickRateIncrease;
            timeTillClick = clickRate;      
            pointsText.text = "Points: " + points;
        }
        timeTillClick -= Time.deltaTime;
    }

    public void SpendPoints(int amt)
    {
        points -= amt;
    }
}
