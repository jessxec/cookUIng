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
    public Button steamer;
    public Button heat;
    public Button collectable;
    public Button restart;
    public Button oilBtn;
    public Button stopCooking;
    public Button ketchup;
    public Button soy;
    public Button greenOnions;
    public Button waterBtn;
    public Button flipBtn;
    public Scrollbar stirBtn;

    public GameObject collection;
    public GameObject heatDial;
    public GameObject panel;

    public Image timer;

    private bool collectionOn = false;
    private bool gotEgg;
    private bool selectedPan;
    private bool selectedPot;
    private bool selectedSteamer;
    private bool heatOn;
    private bool temp;
    private bool water;
    private bool oil;
    private bool flip;
    private bool stirBefore;
    private bool stirAfter;
    private bool selectedSoy;
    private bool doneCooking;
    private bool selectedKetchup;
    private bool selectedGreens;


    private float multiplier;
    private float cookingTime;
    private float startTime = 50f;
    private int stirSpeed = 0;


    public Sprite uncookedEgg;
    public Sprite[] boiledEgg;
    public Sprite[] friedEgg;
    public Sprite[] scrambledEgg;
    public Sprite[] omlette;
    public Sprite bastedEgg;
    public Sprite[] steamedEgg;

    public TMP_Text message;
    public TMP_Text header;
    public TMP_Text result;
 

    public Image eggImage;

    // Start is called before the first frame update
    void Start()
    {
        
        gotEgg = false;
        selectedPan = false;
        selectedPot = false;
        selectedSteamer = false;
        selectedGreens = false;
        selectedKetchup = false;
        selectedSoy = false;
        heatOn = false;
        cookingTime = startTime;
        temp = false;
        panel.SetActive(false);
        message.text = "make an egg please!";
        flip = false;
        flipBtn.interactable = false;
        stirBefore = false;
        stirAfter = false;
        oil = false;
        water = false;
        ketchup.interactable = false;
        soy.interactable = false;
        greenOnions.interactable = false;
       

        collection = GameObject.Find("/CollectionPanel/Scroll View");
        Debug.Log("Start Found Collection:" + collection.GetInstanceID());
        collection.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (heatOn)
        {

            Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = (mouseScreenPosition - (Vector2)heatDial.transform.position).normalized;
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
        if (temp && gotEgg && (selectedPan || selectedPot || selectedSteamer) && cookingTime >= 0)
        {
            flipBtn.interactable = true;
            cookingTime -= Time.deltaTime * multiplier;
            TimerOn();
        }
        stirBtn.onValueChanged.AddListener((value) =>
        {
            int currentStep = Mathf.RoundToInt(value / (1f / (float)stirBtn.numberOfSteps));
        });


    }

    public void StirBtn()
    {
        if ((selectedPan || selectedPot || selectedSteamer) && temp && gotEgg)
        {
            stirAfter = true;
            Debug.Log("after: " + stirAfter);
        }
        else { stirBefore = true; Debug.Log("before:" + stirBefore); }

        if (stirBtn.value < 0.33 && stirBtn.value >= 0)
        {
            stirSpeed = 0;
        }
        else if (stirBtn.value < 0.67 && stirBtn.value >= 0.33)
        {
            stirSpeed = 1;
            message.text = "stiring...";
        }
        else
        {
            stirSpeed = 2;
            message.text = "stiring faster!";
        }
    }

    public void FlipSwitch()
    {
        flip = true;
        flipBtn.interactable = false;
        message.text = "egg flip!";
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
        steamer.interactable = false;
        pot.interactable = false;
        message.text = "u have a pan...";
    }

    public void PotButton()
    {
        selectedPot = true;
        pot.enabled = false;
        steamer.interactable = false;
        pan.interactable = false;
        message.text = "u have a pot...";
    }

    public void SteamerButton()
    {
        selectedSteamer = true;
        pot.interactable = false;
        pan.interactable = false;
        steamer.enabled = false;
        message.text = "u have a steamer...";
    }

    public void HeatButton()
    {
        heatOn = !heatOn;
    }

    public void OilButton()
    {
        oil = true;
        message.text = "u have some oil...";
        oilBtn.interactable = false;
    }

    public void WaterButton()
    {
        water = true;
        message.text = "u got some water...";
        waterBtn.interactable = false;
    }

    public void OnionButton()
    {
        selectedGreens = true;
        message.text = "u added some green onions...";
        greenOnions.interactable = false;
    }

    public void SoyButton()
    {
        selectedSoy = true;
        message.text = "u added some soy sauce...";
        soy.interactable = false;
    }

    public void KetchupButton()
    {
        selectedKetchup = true;
        message.text = "u added some ketchup...";
        ketchup.interactable = false;
    }

    public void Plate()
    {
        if (selectedPot && stirBefore)
        {
            soy.interactable = true;
            greenOnions.interactable = true;
        }

        if (flip && stirBefore)
        {
            ketchup.interactable = true;
        }
        
        heat.interactable = false;
        stirBtn.interactable = false;
        flipBtn.interactable = false;
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

        // if no oil and water, burn

        
        //bool fried = (selectedPan || selectedPot) && oil;
        //bool boiled = selectedPot && water;
        //bool omlette = (selectedPot || selectedPan) && oil && stir;
        //bool basted = selectedSteamer && water;
        //bool steamed = selectedSteamer && water && stir;

        // fried
        if ((selectedPan || selectedPot) && oil && !stirBefore)
        {
            Debug.Log("fried");
            FriedOptions();
        }
        // boiled
        else if (selectedPot && water)
        {
            Debug.Log("boiled");
            BoiledOptions();
        // steamed
        } else if (selectedSteamer && water)
        {
            Debug.Log("steam");
            SteamerOptions();
        } else if ((selectedPot || selectedPan) && oil && stirBefore)
        {
            OmletteOptions();
        } else if ((selectedPan || selectedPot) && !oil && !water)
        {
            GameManager.S.UpdateCollection("burnt");
            eggImage.sprite = friedEgg[3];
            result.text = ("your egg is burnt!! :(, please add oil next time");
        } else {
            GameManager.S.UpdateCollection("egg");
            eggImage.sprite = uncookedEgg;
            result.text = ("you have an egg! ... uncooked :0");
        }
        
        // pop out panel for finished egg dish
        panel.SetActive(true);
        header.text = "eggcellent!";

    }

    private void OmletteOptions()
    {
        Debug.Log("omlette");
        if (!flip && !stirAfter)
        {
            GameManager.S.UpdateCollection("unscrambled");
            eggImage.sprite = omlette[0];
            result.text = ("not a omlette, not scrambled eggs, its...");
        } else if (flip && cookingTime <= 0)
        {
            GameManager.S.UpdateCollection("burntomlette");
            eggImage.sprite = omlette[3];
            result.text = ("crispy scrambled eggs :0");
        } else if (flip && cookingTime < 50 && cookingTime > 0)
        {
            GameManager.S.UpdateCollection("omlette");
            eggImage.sprite = omlette[1];
            result.text = ("its a omlette <3");
        }

        if (flip && selectedKetchup)
        {
            GameManager.S.UpdateCollection("ketchupom");
            eggImage.sprite = omlette[1];
            result.text = ("its a <3 omlette :D");
        }

        else if (cookingTime <= 15 && stirAfter)
        // burnt
        {
            GameManager.S.UpdateCollection("burntscrambled");
            eggImage.sprite = scrambledEgg[2];
            result.text = ("crispy scrambled eggs :0");

        } // uncooked
        else if (cookingTime == startTime)
        {
            GameManager.S.UpdateCollection("egg");
            eggImage.sprite = uncookedEgg;
            result.text = ("you have an egg! ... uncooked :(");
        } // scrambled
        else if (cookingTime < 50 && cookingTime > 15 && stirAfter && stirSpeed == 1)
        {
            GameManager.S.UpdateCollection("scrambled");
            eggImage.sprite = scrambledEgg[0];
            result.text = ("scrambled eggs <3");
        } // hard
        else if (cookingTime < 50 && cookingTime > 15 && stirAfter && stirSpeed == 2)
        {
            GameManager.S.UpdateCollection("overscrambled");
            eggImage.sprite = scrambledEgg[1];
            result.text = ("overscrambled eggs <3");
        }

    }

    private void BoiledOptions()
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

    private void FriedOptions()
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
        if (!stirBefore)
        {
            GameManager.S.UpdateCollection("basted");
            eggImage.sprite = bastedEgg;
            result.text = ("basted egg <3");
        }
        else if (selectedSoy && !selectedGreens)
        {
            GameManager.S.UpdateCollection("steamedsoy");
            eggImage.sprite = steamedEgg[2];
            result.text = ("steamed egg with soy sauce <3");
        }
        else if (!selectedSoy && selectedGreens)
        {
            GameManager.S.UpdateCollection("steamedgreen");
            eggImage.sprite = steamedEgg[1];
            result.text = ("steamed egg with green onions <3");
        } else if (selectedSoy && selectedGreens)
        {
            GameManager.S.UpdateCollection("steamedall");
            eggImage.sprite = steamedEgg[3];
            result.text = ("steamed egg with the works <3");
        } else {
            GameManager.S.UpdateCollection("steamed");
            eggImage.sprite = steamedEgg[0];
            result.text = ("steamed egg <3");
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
