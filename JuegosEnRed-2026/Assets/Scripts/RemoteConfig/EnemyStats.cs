using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public PatrullaNavMesh patrol;

    void OnEnable()
    {
        ExampleSample.OnEnemySpeedReceived += HandleNewConfig;
    }
    
    void OnDisable()
    {
        ExampleSample.OnEnemySpeedReceived -= HandleNewConfig;
    }

    private void HandleNewConfig(float speed)
    {
        patrol.velocidadDefault = speed;
    }

}
