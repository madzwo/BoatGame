using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    private float clickRange;
    private int multiplierIncrease;

    public GameObject clickBox;
    public GameObject clickMultiplier;
    public GameObject clickAutoClick;

    public int points;
    public int multiplier;
    public int autoClickLevel;

    public int multiplierPrice;
    public int autoClickLevelPrice;

    public TMP_Text pointsText;
    public TMP_Text multiplierText;
    public TMP_Text AutoClickText;

    public TMP_Text multiplierPriceText;
    public TMP_Text AutoClickPriceText;

    public float timeTillClick;
    public float clickRate;


    void Start()
    {
        clickRange = .5f;

        points = 0;
        multiplier = 1;
        autoClickLevel = 0;

        autoClickLevelPrice = 10;
        multiplierPrice = 25;

        multiplierIncrease = 1;

        clickRate = 2f;
        timeTillClick = clickRate;
    }

    void Update()
    {      
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float clickDistance = Vector2.Distance(mousePosition, clickBox.transform.position);
            if (clickDistance <= clickRange)
            {
                points += multiplier;            
            }
            pointsText.text = "Points: " + points;

            float multiplierDistance = Vector2.Distance(mousePosition, clickMultiplier.transform.position);
            if (multiplierDistance <= clickRange && points >= multiplierPrice)
            {
                multiplier += multiplierIncrease;
                multiplierIncrease *= 5;           
                points -= multiplierPrice;
                multiplierPrice *= 10;
                multiplierPriceText.text = "Price: " + multiplierPrice;
                pointsText.text = "Points: " + points;
            }
            multiplierText.text = "x" + multiplier;

            float autoClickDistance = Vector2.Distance(mousePosition, clickAutoClick.transform.position);
            if (autoClickDistance <= clickRange && points >= autoClickLevelPrice)
            {
                autoClickLevel += 1;           
                points -= autoClickLevelPrice;
                autoClickLevelPrice *= 5;
                AutoClickPriceText.text = "Price: " + autoClickLevelPrice;
                pointsText.text = "Points: " + points;
            }
            AutoClickText.text = "" + autoClickLevel;
        }

        if (autoClickLevel != 0)
        {
            if (timeTillClick <= 0)
            {
                points += multiplier;     
                clickRate = 5f / (float) autoClickLevel;
                timeTillClick = clickRate;      
                pointsText.text = "Points: " + points;
            }
            timeTillClick -= Time.deltaTime;
        }
    }

    public void SpendPoints(int amt)
    {
        points -= amt;
    }
}
