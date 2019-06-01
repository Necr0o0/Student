using UnityEngine;
using System;

public class Interactive : MonoBehaviour
{

    [SerializeField]
	Action action;

    public String interactionType;
    [TextArea]
    public String moreInfo;

  

	public void SetAction(String actionName)
	{
        switch (actionName)
        {
            case "Desk":
                action = Desk;
                break;

            case "Bed":
                action = Bed;
                break;
            case "Fridge":
                action = Fridge;
                break;
            case "Math Book":
                action = MathBook;
                break;
            case "Beer":
                action = Beer;
                break;
        }
      

       
	}
    public void Interaction()
	{
        Renderer rend = this.GetComponent<Renderer>();
        // rend.material = new Material(Shader.Find("Outline"));

        SetAction(interactionType);
		action();
        FadeManager.singleton.Fade(true, 1.1f);

    }

    void Desk()
    {
        DisplayTime.singleton.hours += 1;
        GameManager.singleton.knowledge.value+=5;
        GameManager.singleton.energy.value-=10;

    }

    void Bed()
    {
        DisplayTime.singleton.hours += 1;
        GameManager.singleton.energy.value += 20;
    }
    void Fridge()
    {
        DisplayTime.singleton.hours += 1;
        GameManager.singleton.food.value += 5;
        GameManager.singleton.energy.value -=1;
    }
    void MathBook()
    {
        DisplayTime.singleton.hours += 1;
        GameManager.singleton.knowledge.value += 25;
        GameManager.singleton.energy.value -= 5;
        GameManager.singleton.fun.value -= 10;

    }

    void Beer()
    {
        DisplayTime.singleton.hours += 1;
        GameManager.singleton.knowledge.value -= 15;
        GameManager.singleton.energy.value -= 3;
        GameManager.singleton.fun.value += 20;
    }
}
