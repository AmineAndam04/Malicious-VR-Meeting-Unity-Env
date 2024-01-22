using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.SideChannels;
using System;
using System.Collections.Generic;

public class SendHyperParameters : SideChannel
{
    List<float> hyperParameters = new List<float>() ; // = new List<float> {10f, 0.1f, 0.05f, 0.05f, 7.5f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f} ;
    public SendHyperParameters()
    {
        
        ChannelId = new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"); // the Guid
    }

    protected override void OnMessageReceived(IncomingMessage msg)
    {
        hyperParameters = new List<float>();
        hyperParameters.Clear();
        int numHyperParameters = msg.ReadInt32(); 
        for (int i = 0; i < numHyperParameters; i++)
        {
            float hyperParameter = msg.ReadFloat32();
            hyperParameters.Add(hyperParameter);
        }
        
    }
    /*public void SendIndividualRewards(List<float> floatList)
    {
        
        int index = 0;
        if (floatList.Count > 0 && floatList.Count <7){
        using (var msgOut = new OutgoingMessage())
        {
            
            foreach (var floatValue in floatList)
            {
                msgOut.WriteFloat32(floatValue);
                index +=1;
                if (index > 5)
                {
                    break;
                }
            }
            QueueMessageToSend(msgOut);
        }}
    }*/

    public List<float> GetReceivedHyperParameters()
    {
        return hyperParameters;
    }
}
