using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIController : MonoBehaviour
{

    public GameObject main;
    public GameObject option;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        main.SetActive(false);
        option.SetActive(false);
    }

    public void openMain()
    {
        main.SetActive(true);
        option.SetActive(false);
    }

    public void openOption()
    {
        main.SetActive(false);
        option.SetActive(true);
    }

    public void closeAll()
    {
        main.SetActive(false);
        option.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (main.active || option.active)
            {
                closeAll();
            }
            else
            {
                openMain();
            }

            
        }
    }
}
