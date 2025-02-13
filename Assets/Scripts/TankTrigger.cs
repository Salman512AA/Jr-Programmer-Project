using UnityEngine;

public class TankTrigger : ParentCarTrigger
{
    private TankMovement tankMovement;

    protected override void Start()
    {
        base.Start();
        tankMovement = GetComponent<TankMovement>();
    }

    protected override void EnterCar()
    {
        base.EnterCar();

        if (tankMovement != null)
            tankMovement.enabled = true; // Enable Tank Movement
    }

    protected override void ExitCar()
    {
        base.ExitCar();

        if (tankMovement != null)
            tankMovement.enabled = false; // Disable Tank Movement
    }
}


