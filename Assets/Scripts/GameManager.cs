using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Taxis")]
    public Transform[] TaxiSpawns;
    public GameObject Taxi;
    public Transform[] Targets;

    [Header("Busses")]
    public PathCreator[] Paths;
    public GameObject Bus;

    [Header("Other")]
    public KeyCode ShowTargetsKey;
    public GameObject menu;

    private void Start()
    {

        for (int i = 0; i < TaxiSpawns.Length; i++)
        {
            GameObject newTaxi = Instantiate(Taxi, TaxiSpawns[i].position, new Quaternion(0, TaxiSpawns[i].rotation.y, 0, 0));
            newTaxi.GetComponent<Taxi>().target = Targets[i];
        }
        for (int i = 0; i < Paths.Length; i++)
        {
            GameObject newBus = Instantiate(Bus);
            newBus.GetComponent<Bus>().path = Paths[i];
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(ShowTargetsKey))
            foreach (Transform target in Targets)
            {
                target.GetComponent<SpriteRenderer>().enabled= !target.GetComponent<SpriteRenderer>().enabled;
            }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            menu.SetActive(!menu.active);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Switchscene(int index) {
    {
        SceneManager.LoadScene(index);
    }
    }


}
