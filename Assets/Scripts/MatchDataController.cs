using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MatchDataController : MonoBehaviour
{
    [SerializeField]
    private string DataFileName;
    [SerializeField]
    private GameObject ActorPrefab;
    [SerializeField]
    private GameObject ActorParent;
    [SerializeField]
    private GameObject BallPrefab;
    [SerializeField]
    private GameObject BallParent;    

    private StreamReader StreamReader;
    string[] DataStreamAsArray;
    private int dataIndex;
    private Ball BallComponent;

    private void Awake()
    {
        //Initialize stream reader and related variables for the loop        
        string filePath = string.Concat(Application.dataPath,@"\", DataFileName);
        StreamReader = new StreamReader(filePath);        
        
        string dataStream = StreamReader.ReadToEnd();
        DataStreamAsArray = SplitDataIntoArray(dataStream);
        dataIndex = 0;

        //Instantiate Ball
        GameObject ball = Instantiate(BallPrefab);
        ball.transform.SetParent(BallParent.transform);

        //Get ball components
        BallComponent = ball.GetComponent<Ball>();
        BallComponent.Movement = ball.GetComponent<Movement>();        
    }

    void Update()
    {
        if (dataIndex < DataStreamAsArray.Length)
        {
            string[] parsedObjects = DataStreamAsArray[dataIndex].Split(':');

            var frameId = parsedObjects[0];
            var actorsRawData = parsedObjects[1];
            var ballRawData = parsedObjects[2];

            //Process Ball data
            string[] ballSplittedData = GetObjectChunks(ballRawData);
            BallComponent.XPosition = float.Parse(ballSplittedData[0]);
            BallComponent.ZPosition = float.Parse(ballSplittedData[1]);
            BallComponent.YPosition = float.Parse(ballSplittedData[2]);
            BallComponent.Speed = float.Parse(ballSplittedData[3]);
            //TODO: ask about ClickerFlags
            //string[] ballClickerFlags = ballSplittedData[4].Split(',');

            BallComponent.Movement.Move(new Vector3(BallComponent.XPosition, 0, BallComponent.ZPosition));

            Debug.Log(string.Concat("Processing frame #", frameId));

            //Process Actors data
            string[] actorsRawDataAsArray = GetArrayOfActorsData(actorsRawData);

            foreach (var actorData in actorsRawDataAsArray)
            {
                string[] actorSplittedData = GetObjectChunks(actorData);
                if (actorSplittedData.Length < 6) continue;

                int actorTeam = int.Parse(actorSplittedData[0]);
                int actorTrackingId = int.Parse(actorSplittedData[1]);
                int actorNumber = int.Parse(actorSplittedData[2]);
                float actorXPosition = float.Parse(actorSplittedData[3]);
                float actorZPosition = float.Parse(actorSplittedData[4]);
                float actorSpeed = float.Parse(actorSplittedData[5]);

                //Re utilize existing instances            
                GameObject existingActor = GameObject.Find(string.Concat("Actor", actorTrackingId));

                //Actor Processing
                if (existingActor == null)
                {
                    GameObject actor = Instantiate(ActorPrefab, ActorParent.transform);
                    Actor actorComponent = actor.GetComponent<Actor>();
                    Movement actorMovement = actor.GetComponent<Movement>();

                    //Assign data
                    actorComponent.Team = actorTeam;
                    actorComponent.TrackingId = actorTrackingId;
                    actorComponent.Number = actorNumber;
                    actorComponent.XPosition = actorXPosition;
                    actorComponent.ZPosition = actorZPosition;
                    actorComponent.Speed = actorSpeed;

                    actorComponent.AssignRole(actorComponent.Team);
                    actorComponent.AssignNumber(actorComponent.Number.ToString());
                    actor.name = string.Concat("Actor", actorComponent.TrackingId.ToString());

                    var actorMovementVector = new Vector3(actorComponent.XPosition, 0, actorComponent.ZPosition);
                    actorMovement.Move(actorMovementVector);

                    Debug.Log(actor.name + "POSITION: " + actor.transform.position);
                }
                else
                {
                    //for some reason this returns null
                    //Actor actorComponent = existingActor.GetComponent<Actor>();

                    //but creating a new instance of existingActor and then going 
                    //and finding the exact same object once more, works...                

                    GameObject anotherInstanceOfExistingActor = GameObject.Find(string.Concat("Actor", actorTrackingId));
                    var anotherInstanceOfActorComponent = anotherInstanceOfExistingActor.GetComponent<Actor>();
                    Movement anotherInstanceOfActorMovement = anotherInstanceOfExistingActor.GetComponent<Movement>();

                    anotherInstanceOfActorMovement.Move(new Vector3(actorXPosition, 0, actorZPosition));

                    Debug.Log(anotherInstanceOfActorComponent.name + "POSITION: " + anotherInstanceOfActorComponent.transform.position);
                }
            }
        }
        dataIndex++;
    }    

    private string[] SplitDataIntoArray(string dataStream)
    {        
        return dataStream.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
    }

    private string[] GetObjectChunks(string parsedLine)
    {
        return parsedLine.Split(',');
    }

    private string[] GetArrayOfActorsData(string parsedLine)
    {
        return parsedLine.Split(';');
    }
}
