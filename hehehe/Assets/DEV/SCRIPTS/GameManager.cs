using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float GameSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = GameSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
