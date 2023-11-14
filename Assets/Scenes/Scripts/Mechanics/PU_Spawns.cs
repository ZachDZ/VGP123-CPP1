using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PU_Spawns : MonoBehaviour
{

    public int spawnNum;

    public Transform powerUp1;
    public Transform powerUp2;
    public Transform powerUp3;


    // Start is called before the first frame update
    void Start()
    {

        spawnNum = Random.Range(0, 4);

        switch (spawnNum)
        {
            case 1:
                GameObject.Instantiate(powerUp1, gameObject.transform.position, Quaternion.identity);
                break;
            case 2:
                GameObject.Instantiate(powerUp2, gameObject.transform.position, Quaternion.identity);
                break;
            case 3:
                GameObject.Instantiate(powerUp3, gameObject.transform.position, Quaternion.identity);
                break;
            default:
                break;
        }

    }
}
