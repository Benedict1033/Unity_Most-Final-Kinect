﻿using UnityEngine;
using System.Collections.Generic;
using Joint = Windows.Kinect.Joint;
using Windows.Kinect;
using UnityEngine.SceneManagement;

public class BodySourceViewH : MonoBehaviour
{
    public BodySourceManager bodySourceManager;
    public GameObject JointObject;
    private Dictionary<ulong, GameObject> mBodies = new Dictionary<ulong, GameObject>();
    public Transform[] food;

    public GameObject btnn;

    public static int i=5;

    private List<JointType> joints = new List<JointType> {
    JointType.HandLeft,
    JointType.HandRight,
    };

    private void Start()
    {
        i = 0;
    }

    private void Update()
    {
        Body[] data = bodySourceManager.GetData();
        if (data == null)
            return;

        List<ulong> trackedIds = new List<ulong>();

        foreach (var body in data)
        {
            if (body == null)
                continue;

            if (body.IsTracked)
            {
                trackedIds.Add(body.TrackingId);
            }
        }

        List<ulong> knownIds = new List<ulong>(mBodies.Keys);

        foreach (ulong trackingId in knownIds)
        {
            if (!trackedIds.Contains(trackingId))
            {
                Destroy(mBodies[trackingId]);
                mBodies.Remove(trackingId);
            }
        }

        foreach (var body in data)
        {
            if (body == null)
                continue;

            if (body.IsTracked)
            {
                if (!mBodies.ContainsKey(body.TrackingId))
                {
                    mBodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }

                UpdateBodyObject(body, mBodies[body.TrackingId]);
            }
        }
    }

    private GameObject CreateBodyObject(ulong id)
    {
        Invoke("handMouse", 2);
        GameObject body = new GameObject("Body:" + id);
        body.transform.localScale = new Vector3(1, 1f, 1);
        body.transform.localPosition = new Vector3(0, 0, 0);

        foreach (JointType joint in joints)
        {
            GameObject newJoint = Instantiate(JointObject);
            newJoint.name = joint.ToString();
            newJoint.transform.parent = body.transform;
        }

        if (body.transform.GetChild(0).gameObject.name == "HandLeft") { body.transform.GetChild(0).gameObject.SetActive(false); }
        if (body.transform.GetChild(1).gameObject.name == "HandLeft") { body.transform.GetChild(1).gameObject.SetActive(false); }

        return body;
    }

    private void UpdateBodyObject(Body body, GameObject bodyObject)
    {
        foreach (JointType joint in joints)
        {
            Joint sourceJoint = body.Joints[joint];
            Vector3 targetPosition = GetVector3FromJoint(sourceJoint);

            Transform jointObject = bodyObject.transform.Find(joint.ToString());
            jointObject.position = targetPosition;
        }
    }

    private Vector3 GetVector3FromJoint(Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, 0);
    }

    void handMouse()
    {

        Invoke("handMouse", 0.2f);


        try
        {
            if (GameObject.Find("HandRight").transform.position.y <= 0.51 && GameObject.Find("HandRight").transform.position.y >= -0.6)
            {
                if (GameObject.Find("HandRight").transform.position.x >= -7.33 && GameObject.Find("HandRight").transform.position.x <= -6.27)
                {
                    //1
                    i += 1;
                    food[0].gameObject.SetActive(true);
                    food[6].gameObject.SetActive(true);
                }

                if (GameObject.Find("HandRight").transform.position.x >= -3.95 && GameObject.Find("HandRight").transform.position.x <= -3.3)
                {
                    //2
                    i += 1;
                    food[1].gameObject.SetActive(true);
                    food[6].gameObject.SetActive(true);


                }
                if (GameObject.Find("HandRight").transform.position.x >= -1.35&& GameObject.Find("HandRight").transform.position.x <= -0.14)
                {
                    //3
                    i += 1;
                    food[2].gameObject.SetActive(true);
                    food[6].gameObject.SetActive(true);

                }

                if (GameObject.Find("HandRight").transform.position.x >= 1.75 && GameObject.Find("HandRight").transform.position.x <= 2.98)
                {
                    //1
                    i += 1;
                    food[4].gameObject.SetActive(true);
                    food[6].gameObject.SetActive(true);

                }

                if (GameObject.Find("HandRight").transform.position.x >= 5 && GameObject.Find("HandRight").transform.position.x <= 7.56)
                {
                    //2
             

                    i += 1;
                    food[3].gameObject.SetActive(true);
                    food[6].gameObject.SetActive(true);


                }
            }
           
      
                //if (GameObject.Find("HandRight").transform.position.x >= 5.76 && GameObject.Find("HandRight").transform.position.x <= 7.77)
                //{
                //    //3
                //    i +=1;
                //    food[5].gameObject.SetActive(true);
                //    food[6].gameObject.SetActive(true);

                //}

            
            else if (GameObject.Find("HandRight").transform.position.y <=-3.44&&i>=1) {

                if (GameObject.Find("HandRight").transform.position.x >= -1.5 && GameObject.Find("HandRight").transform.position.x <= 1.81)
                {
                    btnn.SetActive(false);
                    Invoke("wait", 1.5f);
                }
               
            }


            }
        catch { }
    }
    void wait()
    {
        SceneManager.LoadScene("6.Easy");
    }


}

