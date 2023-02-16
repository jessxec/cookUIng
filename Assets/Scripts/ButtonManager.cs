using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public Button egg;
    public Button pan;
    public Button pot;
    public Button heat;
    public Button collectable;
    public Button restart;
    public GameObject collection;

    public GameObject heatDial;
    public GameObject panel;

    public Image timer;

    private bool collectionOn = false;
    private bool gotEgg;
    private bool selectedPan;
    private bool selectedPot;
    private bool heatOn;
    private bool temp;
    private float multiplier;
    private float cookingTime;
    private float startTime = 50f;

    public string[] eggStates = {"uncooked", "low", "med", "hard", "burnt"};
    public bool isBoiled = false;

    public Sprite uncookedEgg;
    public Sprite[] boiledEgg;
    public Sprite[] friedEgg;
    public Sprite[] scrambledEgg;
    public Sprite[] omlette;
    public Sprite bastedEgg;
    public Sprite steamedEgg;
  
    public string eggIs;

    public Image eggImage;

    // Start is called before the first frame update
    void Start()
    {
        
        gotEgg = false;
        selectedPan = false;
        selectedPot = false;
        heatOn = false;
        cookingTime = startTime;
        temp = false;
        eggIs = eggStates[0];
        panel.SetActive(false);
        collection.SetActive(false);
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
                // off
                if ((direction.x >= -0.5 || direction.x <= 0.5) && (direction.y >= 0.5))
                {
                    temp = false;
                    multiplier = 0f;
                    heatDial.transform.up = new Vector2(0, 1);
                } // med
                else if ((direction.x >= -0.5 || direction.x <= 0.5) && (direction.y <= -0.5))
                {
                    temp = true;
                    multiplier = 2f;
                    heatDial.transform.up = new Vector2(0, -1);
                } // high
                else if ((direction.y >= -0.5 || direction.y <= 0.5) && (direction.x <= -0.5))
                {
                    temp = true;
                    multiplier = 4f;
                    heatDial.transform.up = new Vector2(-1, 0);
                } // low
                else if ((direction.y >= -0.5 || direction.y <= 0.5) && (direction.x >= 0.5))
                {
                    temp = true;
                    multiplier = 1f;
                    heatDial.transform.up = new Vector2(1, 0);
                }
            }

        }
        if (temp && gotEgg && (selectedPan || selectedPot) && cookingTime >= 0)
        {
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

    public void GetResults()
    {
        temp = false;
        multiplier = 0;
        pan.interactable = false;
        pot.interactable = false;
        heat.interactable = false;

        // burnt
        if (cookingTime <= 0)
        {
            eggIs = eggStates[4];
            //eggImage.sprite = boiledEgg[4];
            Debug.Log("your egg is burnt!! :(");
        } // uncooked
        if (cookingTime == startTime)
        {
            eggImage.sprite = uncookedEgg;
            eggIs = eggStates[0];
            //eggImage.sprite = boiledEgg[0];
            Debug.Log("you have an egg! ... uncooked :0");
        } // undercooked
        if (cookingTime < 50 && cookingTime >= 20)
        {
            eggIs = eggStates[1];
            eggImage.sprite = boiledEgg[2];
            Debug.Log("soft egg");
        } // med
        if (cookingTime < 20 && cookingTime >= 12)
        {
            eggIs = eggStates[2];
            eggImage.sprite = boiledEgg[1];
            Debug.Log("med egg");
        } // hard
        if (cookingTime < 12 && cookingTime > 0)
        {
            eggIs = eggStates[3];
            eggImage.sprite = boiledEgg[0];
            Debug.Log("hard egg");
        }

        // pop out panel for finished egg dish
        panel.SetActive(true);

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CollectionBtn()
    {
        collection.SetActive(!collectionOn);
        collectionOn = !collectionOn;
    }




}
