using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneManager = UnityEngine.SceneManagement.SceneManager;


public class ScriptInfrastructure : MonoBehaviour
{
    
    public static ScriptInfrastructure instance;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Streets2");
        }
    }
}