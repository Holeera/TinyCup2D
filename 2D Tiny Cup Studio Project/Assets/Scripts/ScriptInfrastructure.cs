using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
