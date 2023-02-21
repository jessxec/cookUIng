using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtnManager : MonoBehaviour
{
    public void btn_Start()
    {
        SceneManager.LoadScene("cookUIng");
    }
}
