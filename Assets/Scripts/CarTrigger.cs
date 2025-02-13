using UnityEngine;
using UnityEngine.AI;

public class CarTrigger : ParentCarTrigger
{
    private CarController carController;
    private AiCarMovement aiController;
    private NavMeshAgent navMeshAgent;

    protected override void Start()
    {
        base.Start(); // Call the parent script's Start() method

        carController = GetComponent<CarController>();
        aiController = GetComponent<AiCarMovement>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected override void EnterCar()
    {
        if (navMeshAgent != null) navMeshAgent.enabled = false;

        carController.enabled = true;
        aiController.enabled = false;

        base.EnterCar(); // Call the parent class EnterCar() logic
    }

    protected override void ExitCar()
    {
        if (navMeshAgent != null) navMeshAgent.enabled = true;
        carController.OnDisable();
        carController.enabled = false;
        aiController.enabled = true;

        base.ExitCar(); // Call the parent class ExitCar() logic
    }
}
