using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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


    public Sprite uncookedEgg;
    public Sprite[] boiledEgg;
    public Sprite[] friedEgg;
    public Sprite[] scrambledEgg;
    public Sprite[] omlette;
    public Sprite bastedEgg;
    public Sprite steamedEgg;

    public TMP_Text message;
    public TMP_Text header;
    public TMP_Text result;
  
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
        panel.SetActive(false);
        message.text = "make an egg please!";

        //SceneManager.sceneLoaded -= OnSceneLoaded;
        //SceneManager.sceneLoaded += OnSceneLoaded;
        collection = GameObject.Find("/CollectionPanel/Scroll View");
        Debug.Log("Start Found Collection:" + collection.GetInstanceID());
        collection.SetActive(false);
    }

    /*void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        collection = GameObject.Find("/CollectionPanel/Scroll View");
    }*/


    // Update is called once per frame
    void FixedUpdate()
    {
        if (heatOn)
        {

            Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = (mouseScreenPosition - (Vector2)heatDial.transform.position).normalized;
            Debug.Log(direction);
            if ((direction.x >= 0.3 || direction.x <= -0.3) && (direction.y >= 0.3 || direction.y <= -0.3))
            {
                // off
                if ((direction.x >= -0.5 || direction.x <= 0.5) && (direction.y >= 0.5))
                {
                    temp = false;
                    multiplier = 0f;
                    heatDial.transform.up = new Vector2(0, 1);
                    message.text = "the stove is off";
                } // med
                else if ((direction.x >= -0.5 || direction.x <= 0.5) && (direction.y <= -0.5))
                {
                    temp = true;
                    multiplier = 2f;
                    heatDial.transform.up = new Vector2(0, -1);
                    message.text = "its getting warmer!";
                } // high
                else if ((direction.y >= -0.5 || direction.y <= 0.5) && (direction.x <= -0.5))
                {
                    temp = true;
                    multiplier = 4f;
                    heatDial.transform.up = new Vector2(-1, 0);
                    message.text = "woah! its hot!";
                } // low
                else if ((direction.y >= -0.5 || direction.y <= 0.5) && (direction.x >= 0.5))
                {
                    temp = true;
                    multiplier = 1f;
                    heatDial.transform.up = new Vector2(1, 0);
                    message.text = "its warming up...";
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
        message.text = "u have an egg...";
    }

    public void PanButton()
    {
        selectedPan = true;
        pan.enabled = false;
        pot.interactable = false;
        message.text = "u have a pan...";
    }

    public void PotButton()
    {
        selectedPot = true;
        pot.enabled = false;
        pan.interactable = false;
        message.text = "u have a pot...";
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

        if (selectedPan)
        {
            PanOptions();
        } else if (selectedPot)
        {
            PotOptions();
        } else
        {
            SteamerOptions();
        }
        
        // pop out panel for finished egg dish
        panel.SetActive(true);
        header.text = "eggcellent!";

    }

    private void PotOptions()
    {
        // burnt
        if (cookingTime <= 0)
        {
            GameManager.S.UpdateCollection("overboiled");
            eggImage.sprite = boiledEgg[3];
            result.text = ("your egg is overboiled!! :(");
            
        } // uncooked
        if (cookingTime == startTime)
        {
            GameManager.S.UpdateCollection("egg");
            eggImage.sprite = uncookedEgg;
            result.text = ("you have an egg! ... uncooked :0");
        } // undercooked
        if (cookingTime < 50 && cookingTime >= 20)
        {
            GameManager.S.UpdateCollection("softboiled");
            eggImage.sprite = boiledEgg[0];
            result.text = ("soft boiled egg <3");
        } // med
        if (cookingTime < 20 && cookingTime >= 12)
        {
            GameManager.S.UpdateCollection("medboiled");
            eggImage.sprite = boiledEgg[1];
            result.text = ("medium boiled egg <3");
        } // hard
        if (cookingTime < 12 && cookingTime > 0)
        {
            GameManager.S.UpdateCollection("hardboiled");
            eggImage.sprite = boiledEgg[2];
            result.text = ("hard boiled egg <3");
        }

    }

    private void PanOptions()
    {
        if (cookingTime <= 0)
        {
            GameManager.S.UpdateCollection("burnt");
            eggImage.sprite = friedEgg[3];
            result.text = ("your egg is burnt!! :(");
        } // uncooked
        if (cookingTime == startTime)
        {
            GameManager.S.UpdateCollection("egg");
            eggImage.sprite = uncookedEgg;
            result.text = ("you have an egg! ... uncooked :0");
        } // undercooked
        if (cookingTime < 50 && cookingTime >= 20)
        {
            GameManager.S.UpdateCollection("sunnyup");
            eggImage.sprite = friedEgg[0];
            result.text = ("sunny-side up egg <3");
        } // med
        if (cookingTime < 20 && cookingTime >= 12)
        {
            GameManager.S.UpdateCollection("overeasy");
            eggImage.sprite = friedEgg[1];
            result.text = ("over easy egg <3");
        } // hard
        if (cookingTime < 12 && cookingTime > 0)
        {
            GameManager.S.UpdateCollection("overhard");
            eggImage.sprite = friedEgg[2];
            result.text = ("over hard egg <3");
        }

    }

    private void SteamerOptions()
    {
        if (cookingTime == startTime)
        {
            GameManager.S.UpdateCollection("egg");
            eggImage.sprite = uncookedEgg;
            result.text = ("you have an egg! ... uncooked :0");
        }
    }

    public void Restart()
    {
        collection.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CollectionBtn()
    {
        collection.SetActive(!collectionOn);
        collectionOn = !collectionOn;
        header.text = "collection";
    }




}
