using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static Transform player;
    public static Transform hand;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hand = player.Find("HandPos");
    }
}
