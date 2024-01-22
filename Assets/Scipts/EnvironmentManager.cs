using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnvironmentManager : MonoBehaviour
{
    

    public GameObject AttackSharedPresentation;
    public GameObject AttackWhiteBoard;
    public GameObject AttackDistraction ;
    public GameObject UserHarassement;

    // Function to reset the entire environment
    public void ResetEnvironment()
    {
        MaliciousObjectGeneratorXY viewBlockingAttackXY = AttackWhiteBoard.GetComponent<MaliciousObjectGeneratorXY >();
        MaliciousObjectGeneratorYZ viewBlockingAttackYZ = AttackSharedPresentation.GetComponent<MaliciousObjectGeneratorYZ>();
        InstantiatePrefab distractionAttack = AttackDistraction.GetComponent<InstantiatePrefab>();
        UserHarassmentSim avatarInjection = UserHarassement.GetComponent<UserHarassmentSim>();
        if (AttackWhiteBoard != null && viewBlockingAttackXY !=null)
        {
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