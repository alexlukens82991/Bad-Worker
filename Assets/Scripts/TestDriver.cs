using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestDriver : MonoBehaviour
{
    //public NavMeshAgent navAgent;
    //public Camera cam;

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

    //        if (Physics.Raycast(ray, out RaycastHit hit))
    //        {
    //            MakeMove(hit.point);
    //        }
    //    }
    //}

    //private void MakeMove(Vector3 newPosition)
    //{
    //    navAgent.SetDestination(newPosition);
    //}

    public NpcController TestNpcController;
    public WaterCooler WaterCooler;

    private void Start()
    {
        TestNpcController.FindAndInteract(WaterCooler);
    }
}
