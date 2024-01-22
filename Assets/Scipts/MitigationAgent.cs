using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.VisualScripting;
using Unity.MLAgents.SideChannels;

public class TestAgent : Agent
{
    
    private  string[] tagsToFind = {"collaborative","Obscure","Distraction","Avatar","AvatarMalicious"} ;
    private StatsRecorder statsRecorder;
    private bool episodeStarted = false;

    
     private int maxState = 148;
    private Dictionary<string, int>  objectsCount = new Dictionary<string, int> ();
    
    private float padValue = 9999f; 
    private int currentStateSize = 0;
    private int currentActionsSize = 0;
    
    private List<CollabObject> collabList = new List<CollabObject>();
    private List<ObscObject> obscList = new List<ObscObject>();
    private List<DistrcObject> distrcList = new List<DistrcObject>();
    private List<Avatar> avatarList = new List<Avatar>();
    private List<MaliciuosAvatar> malavatarList = new List<MaliciuosAvatar>();
    private float alphaThreshold = 0.2f; 
    private float cf0 = 1/20 ;
    private float bf0 = 1/20;
    private float du = 7f;
    private List<float> rewardWeights = new List<float> {0.007f, 0.0003f, 0.04761f, 0.00384f, 0.00384f, 0.1052f}; 
    
    private SendHyperParameters sendHyperParameters;

    private List<float> hyperParameters = new List<float>() ;
    
    private List<float> rewads = new List<float>() ;
    
    public GameObject EnvManager ;

    private void Awake() {
        
        sendHyperParameters = new SendHyperParameters();
        SideChannelManager.RegisterSideChannel(sendHyperParameters);
        hyperParameters = sendHyperParameters.GetReceivedHyperParameters();
        statsRecorder = Academy.Instance.StatsRecorder;
        if (hyperParameters.Count == 0)
        {
            hyperParameters  = new List<float> {10f, 0.2f, 0.5f, 0.5f, 2.5f, 0.007f, 0.005f, 0.12f, 0.0013f, 0.0013f, 0.25f} ;
        }
    }
    /*public override void Initialize()
    {
        sendHyperParameters = new SendHyperParameters();
        SideChannelManager.RegisterSideChannel(sendHyperParameters);
        hyperParameters = sendHyperParameters.GetReceivedHyperParameters();
        statsRecorder = Academy.Instance.StatsRecorder;
        Debug.Log(hyperParameters.Count);
        if (hyperParameters.Count == 0)
        {
            hyperParameters  = new List<float> {10f, 0.1f, 0.05f, 0.05f, 7.5f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f} ;
        }
    }*/
    public override void OnEpisodeBegin()
    {
    // Reset the environment at the beginning of each episode
        EnvironmentManager environmentManager = EnvManager.GetComponent<EnvironmentManager>();
        environmentManager.ResetEnvironment();
        episodeStarted = true;
        currentStateSize = 0;
        currentActionsSize=0;
        collabList.Clear();
        obscList.Clear();
        distrcList.Clear();
        avatarList.Clear();
        malavatarList.Clear();
    }
    private void Start() {
        
        int numHyperParameters = (int) hyperParameters[0];
        alphaThreshold = hyperParameters[1];
        cf0 = hyperParameters[2];
        bf0 = hyperParameters[3];
        du = hyperParameters[4];
        int j = 0;
        while (j< rewardWeights.Count)
        {
            rewardWeights[j] = hyperParameters[j+5];
            j+=1; 
        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        if (episodeStarted){
            
        
        currentStateSize = 0;
        currentActionsSize=0;
        collabList.Clear();
        obscList.Clear();
        distrcList.Clear();
        avatarList.Clear();
        malavatarList.Clear();
        foreach (string tag in tagsToFind)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
            //Debug.Log(tag +"is" + objectsWithTag.Length);
            objectsCount[tag] =  objectsWithTag.Length;
            //Debug.Log("Nbr of "+ tag +" :"+objectsWithTag.Length);
            
            if (tag == "collaborative")
            {
                
                foreach (GameObject gameObject in objectsWithTag)
                {
                    CollabObject collab = new CollabObject(gameObject);
                    collabList.Add(collab);
                    sensor.AddObservation(collab.GetPosition());
                    //Dictionary<string, Vector3> box = utils.GetBoundingBox(gameObject);
                    //sensor.AddObservation(box["bounds"]);
                    sensor.AddObservation(collab.GetBB()["bounds"]);
                    currentStateSize += 6;
                }
                
            }
            if (tag == "Obscure")
            {
                foreach (GameObject gameObject in objectsWithTag)
                {
                    ObscObject obsc = new ObscObject(gameObject);
                    obscList.Add(obsc);
                    //sensor.AddObservation(gameObject.transform.localPosition);
                    sensor.AddObservation(obsc.GetPosition());
                    //Dictionary<string, Vector3> box = utils.GetBoundingBox(gameObject);
                    //sensor.AddObservation(box["bounds"]);
                    sensor.AddObservation(obsc.GetBB()["bounds"]);
                    //sensor.AddObservation(utils.GetAlpha(gameObject));
                    sensor.AddObservation(obsc.GetAlpha());
                    currentStateSize += 7;
                    currentActionsSize+=7;

                    
                }
            }
            if (tag == "Distraction" )
            {
                foreach (GameObject gameObject in objectsWithTag)
                {
                    DistrcObject distrc = new DistrcObject(gameObject);
                    distrcList.Add(distrc);
                    //sensor.AddObservation(gameObject.transform.localPosition);
                    //sensor.AddObservation(distrc.GetPosition());
                    //Dictionary<string, Vector3> box = utils.GetBoundingBox(gameObject);
                    //sensor.AddObservation(box["bounds"]);
                    sensor.AddObservation(distrc.GetBB());
                    //sensor.AddObservation(utils.GetFrequencies(gameObject));
                    sensor.AddObservation(distrc.Getfreq());
                    currentStateSize += 5;
                    currentActionsSize += 5;
                    
                }
            }

            if (tag == "Avatar")
            {
               foreach (GameObject  gameObject in objectsWithTag)
               {
                  Avatar avatar = new Avatar(gameObject);
                  avatarList.Add(avatar);
                  //sensor.AddObservation(gameObject.transform.localPosition);
                  sensor.AddObservation(avatar.GetPosition());
                  currentStateSize += 3;
               }
               
                
            }
            if (tag == "AvatarMalicious")
            {
                foreach (GameObject  gameObject in objectsWithTag)
               {
                  MaliciuosAvatar malavatar = new MaliciuosAvatar(gameObject);
                  malavatarList.Add(malavatar);
                  //sensor.AddObservation(gameObject.transform.localPosition);
                  sensor.AddObservation(malavatar.GetPosition());
                  currentStateSize += 3;
                  currentActionsSize+=3;
               }
            }
            
                    
        }
        while(currentStateSize< maxState)
        {
            sensor.AddObservation(padValue);
            currentStateSize+=1;
        }
        }

        statsRecorder.Add("Nbr Obsc", obscList.Count);
        statsRecorder.Add("Nbr distr", distrcList.Count);
        statsRecorder.Add("Nbr malavatar", malavatarList.Count);
    }
    

    public override void OnActionReceived(ActionBuffers actions)
    {   
        if (episodeStarted){
        rewads.Clear();
        Vector3 positionShift ; 
        Vector3 bbShift ;
        float alphaShift;
        Vector2 freqshift ;
        // Compute Ixy and Iyz for View blocking elements
        Dictionary<CollabObject, List<float>>  Ixyz = new Dictionary<CollabObject, List<float>>() ;
        List<Vector3> initialPosition = new  List<Vector3>() ;
        float totalReward = 0 ;
        
        
        foreach (CollabObject collab in collabList)
        {
            List<float> I = new List<float>() ;
            foreach (ObscObject obsc in obscList)
            {
                Vector2 ixyz= utils.Ixyz(collab.gameObject,obsc.gameObject);
                I.Add(ixyz.x);
                I.Add(ixyz.y);
            }
            Ixyz.Add(collab,I);
        }

        float Reward2 = 0f;
        // Act on Obscure view
        int actionsIndex = 0;
        foreach (ObscObject obsc in obscList)
        {
            positionShift = new Vector3(actions.ContinuousActions[actionsIndex],actions.ContinuousActions[++actionsIndex],actions.ContinuousActions[++actionsIndex]);
            obsc.ShiftPosition(positionShift); 
            Reward2 -= Vector3.Distance(positionShift, new Vector3(0f,0f,0f));
            ++actionsIndex;
            bbShift = new Vector3(actions.ContinuousActions[actionsIndex],actions.ContinuousActions[++actionsIndex],actions.ContinuousActions[++actionsIndex]);
            obsc.ShiftBB(bbShift); 
            ++actionsIndex;
            alphaShift = actions.ContinuousActions[actionsIndex];
            obsc.ShiftAlpha(alphaShift);
            ++actionsIndex;
        }
        

        // Compute the rewards for View Blocking attack
        float Reward1=0f ;
        foreach (CollabObject collab in collabList)
        {
            int i = 0;
            foreach (ObscObject obsc in obscList)
            {
                Vector3 w = collab.GetPosition();
                Vector3 o = obsc.GetPosition();
                Vector3 bb = obsc.GetBB()["bounds"];
                if (Ixyz[collab][i]==1)
                {
                    Vector2 axyz= utils.Axyz(collab.gameObject,obsc.gameObject);
                    float axy = axyz.x;
                    float dxy = Vector2.Distance(new Vector2(w.x,w.y),new Vector2(o.x,o.y));
                    Reward1 += 0.1f*dxy - axy;
                    //Debug.Log("Distance: " + dxy);
                    //Debug.Log("Surface:" + axy);
                    //Debug.Log("Reward1xy: " + Reward1);


                }
                ++i;
                if (Ixyz[collab][i]==1)
                {
                    Vector2 axyz= utils.Axyz(collab.gameObject,obsc.gameObject);
                    float ayz = axyz.y;
                    float dyz = Vector2.Distance(new Vector2(w.y,w.z),new Vector2(o.y,o.z));
                    Reward1 += 0.1f*dyz - ayz;
                    //Debug.Log("Distance: " + dyz);
                    //Debug.Log("Surface:" + ayz);
                    //Debug.Log("Reward1yz: " + Reward1);
                }
                ++i;
            }
            
        }
        //Debug.Log("Reward1: " + Reward1);
        //Debug.Log("Reward2: " + Reward2);
        AddReward(rewardWeights[0]*Reward1);
        rewads.Add(Reward1);
        totalReward += rewardWeights[0]*Reward1; 
        
        AddReward(rewardWeights[1]*Reward2);
        rewads.Add(Reward2);
        totalReward += rewardWeights[1]*Reward2; 


        float Reward3 = 0f ;
        
        foreach (CollabObject collab in collabList)
        {
            int i=0;
            foreach (ObscObject obsc in obscList)
            {
                Reward3 -= (Ixyz[collab][i] + Ixyz[collab][i+1]) *utils.ReLU(obsc.GetAlpha() - alphaThreshold);
                i+=2;
            }
        }
        //Debug.Log("Reward3: " + Reward3 );
        AddReward(rewardWeights[2]*Reward3);
        rewads.Add(Reward3);
        totalReward += rewardWeights[2]*Reward3;

        
        // Act in distraction objects 
        foreach (DistrcObject distrc in distrcList)
        {
            bbShift = new Vector3(actions.ContinuousActions[actionsIndex],actions.ContinuousActions[++actionsIndex],actions.ContinuousActions[++actionsIndex]);
            distrc.ShiftBB(bbShift); 
            ++actionsIndex;
            freqshift = new Vector2(actions.ContinuousActions[actionsIndex],actions.ContinuousActions[++actionsIndex]);
            distrc.ShiftFreq(freqshift);
            ++actionsIndex;
        }

        // Compute Reward4
        float Reward4 = 0f;
        float Reward5 = 0f;
        foreach (DistrcObject distrc in distrcList)
        {
            Vector2 freqs =  distrc.Getfreq();
            float volume = distrc.GetVolume();
            //Debug.Log("Freq" + freqs);
            //Debug.Log("Volume" + volume);
            Reward4 -= utils.ReLU(freqs.x - cf0)*volume;
            Reward5 -= utils.ReLU(freqs.y - bf0)*volume;
            //Debug.Log("Reward4: " + Reward4);
            //Debug.Log("Reward5: "+ Reward5);
            //Debug.Log("End---------------");
            
        }
        //Debug.Log("Reward4: " + Reward4);
        AddReward(rewardWeights[3]*Reward4);
        rewads.Add(Reward4);
        totalReward += rewardWeights[3]*Reward4;
        //Debug.Log("Reward5: "+ Reward5);
        AddReward(rewardWeights[4]*Reward5);
        rewads.Add(Reward5);
        totalReward += rewardWeights[4]*Reward5;

        foreach (MaliciuosAvatar malavatar in malavatarList)
        {
            positionShift = new Vector3(actions.ContinuousActions[actionsIndex],actions.ContinuousActions[++actionsIndex],actions.ContinuousActions[++actionsIndex]);
            //positionShift = new Vector3(0.1f,0.1f,0.1f);
            malavatar.ShiftPosition(positionShift); 
            ++actionsIndex;
        }

        float Reward6 = 0f;
        foreach (Avatar avatar in avatarList)
        {
            if (malavatarList.Count != 0)
            {
                Reward6 -= utils.ReLU(du - Vector3.Distance(avatar.GetPosition(),malavatarList[0].GetPosition()));
                //Debug.Log(malavatarList[0].GetPosition());
                
            }
            
        }
        //Debug.Log("Reward6: " + Reward6);
        AddReward(rewardWeights[5]*Reward6);
        rewads.Add(Reward6);
        totalReward += rewardWeights[5]*Reward6;
        totalReward = totalReward / 6f ; 
        //Debug.Log("Total reward: " + totalReward);
        statsRecorder.Add("Reward1", Reward1);
        statsRecorder.Add("Reward2", Reward2);
        statsRecorder.Add("Reward3", Reward3);
        statsRecorder.Add("Reward4", Reward4);
        statsRecorder.Add("Reward5", Reward5);
        statsRecorder.Add("Reward6", Reward6);
        statsRecorder.Add("Total Reward",totalReward);
        //Debug.Log(rewads.Count);
        //Debug.Log("Total reward is:" + (Reward1 + Reward2 + Reward3 + Reward4 + Reward5 + Reward6));
        //SetReward((Reward1 + Reward2 + Reward3 + Reward4 + Reward5 + Reward6));
        SetReward(totalReward);
        
        }
    } 

}
