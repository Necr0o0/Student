using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notifications : MonoBehaviour
{
  public IEnumerator TooHeavy(TextMeshProUGUI txt)
    {
        txt.text = "Too heavy";

        yield return new WaitForSeconds(5);

    }
}
