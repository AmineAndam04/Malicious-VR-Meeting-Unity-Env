using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnvironmentManager : MonoBehaviour
{
    /*public MaliciousObjectGeneratorXY viewBlockingAttackXY;
    public MaliciousObjectGeneratorYZ viewBlockingAttackYZ;
    public InstantiatePrefab distractionAttack;
    public UserHarassmentSim avatarInjection;*/

    public GameObject AttackSharedPresentation;
    public GameObject AttackWhiteBoard;
    public GameObject AttackDistraction ;
    public GameObject UserHarassement;

    // Call this method to reset the entire environment
    public void ResetEnvironment()
    {
        // Reset each malicious attack
        

        MaliciousObjectGeneratorXY viewBlockingAttackXY = AttackWhiteBoard.GetComponent<MaliciousObjectGeneratorXY >();
        MaliciousObjectGeneratorYZ viewBlockingAttackYZ = AttackSharedPresentation.GetComponent<MaliciousObjectGeneratorYZ>();
        InstantiatePrefab distractionAttack = AttackDistraction.GetComponent<InstantiatePrefab>();
        UserHarassmentSim avatarInjection = UserHarassement.GetComponent<UserHarassmentSim>();
        if (AttackWhiteBoard != null && viewBlockingAttackXY !=null)
        {
            //Debug.Log("Hi you are here");
            viewBlockingAttackXY.ResetAttack();
        }
        if (AttackSharedPresentation != null && viewBlockingAttackYZ !=null)
        {
            viewBlockingAttackYZ.ResetAttack();
        }
        if (AttackDistraction != null && distractionAttack !=null)
        {
            distractionAttack.ResetAttack();
        }
        if (UserHarassement != null && avatarInjection !=null)
        {
            
            avatarInjection.ResetAttack();
        }
        
        
        
        
    }
}