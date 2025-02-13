using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShowrromCar : ParentCarTrigger
{
    private CarController carController;
   

    protected override void Start()
    {
        base.Start(); // Call the parent script's Start() method

        carController = GetComponent<CarController>();
       
    }
    protected override void EnterCar()
    {

        carController.enabled = true;

        base.EnterCar(); // Call the parent class EnterCar() logic
    }
    protected override void ExitCar()
    {
        carController.OnDisable();

        carController.enabled = false;

        base.ExitCar(); // Call the parent class ExitCar() logic
    }

}
