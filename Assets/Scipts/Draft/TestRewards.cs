using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRewards : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject back;
    [SerializeField] GameObject malicious;
   

    // Update is called once per frame
    void Update()
    {
        Vector2 Ixyz= utils.Ixyz(back,malicious);
        Vector2 Axyz= utils.Axyz(back,malicious);
        Debug.Log("Ixy is: "+ Ixyz.x);
        Debug.Log("Iyz is: "+ Ixyz.y);
        Debug.Log("Axy is: "+ Axyz.x);
        Debug.Log("Ayz is: "+ Axyz.y);

        
    }
}
