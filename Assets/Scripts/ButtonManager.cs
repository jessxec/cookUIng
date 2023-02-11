using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button egg;
    public Button pan;
    public Button pot;
    public Button heat;
    public GameObject heatDial;

    public Image timer;

    private bool gotEgg;
    private bool selectedPan;
    private bool selectedPot;
    private bool heatOn;
    private string temp = "off";
    private float multiplier;
    private float cookingTime;
    private float startTime = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
        gotEgg = false;
        selectedPan = false;
        selectedPot = false;
        heatOn = false;
        cookingTime = startTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (heatOn)
        {

            Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = (mouseScreenPosition - (Vector2)heatDial.transform.position).normalized;
            Debug.Log(direction);
            if ((direction.x >= 0.15 || direction.x <= -0.15) && (direction.y >= 0.15 || direction.y <= -0.15))
            {
                if ((direction.x >= -0.5 || direction.x <= 0.5) && (direction.y >= 0.5))
                {
                    temp = "off";
                    multiplier = 0f;
                    heatDial.transform.up = new Vector2(0, 1);
                }
                else if ((direction.x >= -0.5 || direction.x <= 0.5) && (direction.y <= -0.5))
                {
                    temp = "med";
                    multiplier = 2f;
                    heatDial.transform.up = new Vector2(0, -1);
                }
                else if ((direction.y >= -0.5 || direction.y <= 0.5) && (direction.x <= -0.5))
                {
                    temp = "high";
                    multiplier = 4f;
                    heatDial.transform.up = new Vector2(-1, 0);
                }
                else if ((direction.y >= -0.5 || direction.y <= 0.5) && (direction.x >= 0.5))
                {
                    temp = "low";
                    multiplier = 1f;
                    heatDial.transform.up = new Vector2(1, 0);
                }
            }

            cookingTime -= Time.deltaTime * multiplier;
            TimerOn();
        }
    }

    public void EggButton()
    {
        gotEgg = true;
        egg.interactable = false;
    }

    public void PanButton()
    {
        selectedPan = true;
        pan.enabled = false;
        pot.interactable = false;
    }

    public void PotButton()
    {
        selectedPot = true;
        pot.enabled = false;
        pan.interactable = false;
    }

    public void HeatButton()
    {
        heatOn = !heatOn;
    }

    private void TimerOn()
    {
        timer.fillAmount = 1- (1- (cookingTime / startTime)) ;
    }


}
