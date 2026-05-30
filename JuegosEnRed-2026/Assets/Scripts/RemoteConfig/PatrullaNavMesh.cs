using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PatrullaNavMesh : MonoBehaviour
{
    [Header("Configuración de Rutas")]
    [Tooltip("Arrastra aquí los Transforms de los puntos que el objeto debe recorrer.")]
    public Transform[] waypoints;

    [Header("Configuración de Movimiento")]
    [Tooltip("Velocidad de movimiento del agente.")]
    public float velocidadDefault = 3.5f;
    [Tooltip("Distancia mínima al waypoint para considerarlo alcanzado y pasar al siguiente.")]
    public float distanciaDeParada = 0.5f;

    [Header("Estado Actual")]
    [Tooltip("Indica si el objeto está pausado. Puedes cambiarlo aquí o pulsando la barra espaciadora.")]
    public bool estaPausado = false;

    private NavMeshAgent agente;
    private int indiceWaypointActual = 0;

    void Start()
    {
        // Obtenemos el componente NavMeshAgent
        agente = GetComponent<NavMeshAgent>();

        // Configuramos los valores iniciales del agente
        agente.speed = velocidadDefault;
        agente.stoppingDistance = distanciaDeParada;

        // Desactivamos la rotación/frenado automático excesivo al llegar para mantener el flujo del bucle
        agente.autoBraking = false;

        // Iniciamos el recorrido si hay waypoints asignados
        IrSiguienteWaypoint();
    }

    void Update()
    {
        // 1. Detección del input (Barra espaciadora) para pausar/reanudar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            estaPausado = !estaPausado;
        }

        // 2. Aplicar el estado de pausa al agente
        agente.isStopped = estaPausado;
        
        agente.speed = velocidadDefault;

        // Si está pausado o no hay waypoints, no hacemos más cálculos
        if (estaPausado || waypoints.Length == 0) return;

        // 3. Comprobar si hemos llegado al destino
        // Verificamos si no está calculando un camino y si la distancia restante es menor o igual a nuestra distancia de parada
        if (!agente.pathPending && agente.remainingDistance <= agente.stoppingDistance)
        {
            IrSiguienteWaypoint();
        }
    }

    void IrSiguienteWaypoint()
    {
        // Verificamos que el arreglo no esté vacío
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No hay waypoints asignados en el Inspector.");
            return;
        }

        // Asignamos el destino actual al agente
        agente.destination = waypoints[indiceWaypointActual].position;

        // Actualizamos el índice para el siguiente waypoint, creando un bucle usando el operador módulo (%)
        indiceWaypointActual = (indiceWaypointActual + 1) % waypoints.Length;
    }
}