using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    [Space]
    public Slider food;
    public Slider energy;
    public Slider knowledge;
    public Slider fun;
    [Space]
    public GameObject warningLog;
    public GameObject scoreText;

    [Space]
    public GameObject gameOverScreen;
    public GameObject gameOverReason;


    private int score = 0;
    

    public static GameManager singleton;

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
        InvokeRepeating("SubstractFood", 2.0f, 1f);
        InvokeRepeating("SubstractEnergy", 2.0f, 10f);
        InvokeRepeating("SubstractKnowledge", 2.0f, 30f);
        InvokeRepeating("SubstractFun", 2.0f, 3f);

        InvokeRepeating("AddScore", 0.0f, 5f);
    }

    // Update is called once per frame
    private void Update()
    {

        if (food.value <= 20)
        {
            if (food.value <= 0)
            {
                GameOver("Food");
            }

            ChangeWarning("Food");
        }
        else if (energy.value <= 20)
        {
            if (energy.value <= 0)
            {
                GameOver("Energy");
            }

            ChangeWarning("Energy");
           
        }
        else if (knowledge.value <= 20)
        {
            if (knowledge.value <= 0)
            {
                GameOver("Knowledge");
            }

            ChangeWarning("Knowledge");
        }
        else if (fun.value <= 20)
        {
            if (fun.value <= 0)
            {
                GameOver("Fun");
            }
            ChangeWarning("Fun");
        }
        else
        {
            ChangeWarning("Good");
        }

    }


    void SubstractFood()
    {
        food.value--;
    }

    void SubstractEnergy()
    {
        energy.value--;
    }

    void SubstractKnowledge()
    {
        knowledge.value--;
    }

    void SubstractFun()
    {
        fun.value--;
    }

    void AddKnowledge()
    {
        knowledge.value++;
    }

    void GameOver(string reason)
    {
        CancelInvoke("SubstractFood");
        CancelInvoke("SubstractEnergy");
        CancelInvoke("SubstractKnowledge");
        CancelInvoke("SubstractFun");

        CancelInvoke("AddScore");

        player.GetComponentInChildren<CameraSelector>().enabled = false;
        player.GetComponentInChildren<CameraMovement>().enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<PlayerMovement2>().enabled = false;
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameOverScreen.SetActive(true);

        string reasonText = "test";
            switch (reason)
        {
            case "Energy":
                reasonText = "Umarles z wycieńczenia nadmiarem materiału do naczenia.";
                break;

            case "Food":
                reasonText = "Umarles z glodu.";
                break;
            case "Knowledge":
                reasonText = "Zostales wyrzucony z uczelni.";
                break;
            case "Fun":
                reasonText = "Twoje zycie nie mialo sensu. Popelniles samobojstwo.";
                break;

            
        }
        string scoret = System.Environment.NewLine + "Score: " + score;


        reasonText = reasonText + scoret;

        gameOverReason.GetComponent<TextMeshProUGUI>().text = reasonText;
    }

    void ChangeWarning(string warning)
    {
        string warningText = "test";
        switch (warning)
        {
            case "Energy":
                warningText = "Uczysz się już od paru godzin. Masz dość... Odpocznij";
                break;

            case "Food":
                warningText = "Zaraz umrzesz z głodu...";
                break;
            case "Knowledge":
                warningText = "Twoja wiedza nie jest w najlepszym stanie, a sesja tuż tuż... Poszukaj książki i poucz się!";
                break;
            case "Fun":
                warningText = "Twoje życie jest nudne i masz go coraz bardziej dość... Idź się zabaw!";
                break;
            default:
                warningText = "Świetnie Ci idzie!Studia to wspaniały okres życia!";
                break;
        }

        warningLog.GetComponent<TextMeshProUGUI>().text = warningText;

    }

    void AddScore()
    {
        score += 10;
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }


   
}
