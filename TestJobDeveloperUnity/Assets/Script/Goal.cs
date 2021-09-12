using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BollController boll = other.GetComponent<BollController>();

        if (!boll || GameManager.singleton.GameEnded)
            return;

        Debug.Log("Goal");

        GameManager.singleton.EndGame(true);
    }
}
