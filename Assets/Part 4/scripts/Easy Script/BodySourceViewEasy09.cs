using UnityEngine;
using System.Collections.Generic;
using Joint = Windows.Kinect.Joint;
using Windows.Kinect;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BodySourceViewEasy09 : MonoBehaviour
{
    public BodySourceManager bodySourceManager;
    public GameObject JointObject;
    private Dictionary<ulong, GameObject> mBodies = new Dictionary<ulong, GameObject>();

    public bool i = true;

    public GameObject[] obj;
    public Text win;

    public Together_Easy Together_Easy;

    public GameObject[] person;

    int done;
    int how;
    int how1;
    int how2;

    bool yes;
    bool yes2;




    private void Start()
    {
        i = true;
        done = 0;
        how = 0; how1 = 0; how2 = 0;
        yes = false;
        yes2 = false;

    }

    private List<JointType> joints = new List<JointType> {
    JointType.HandLeft,
    JointType.HandRight,
    };

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
        InvokeRepeating("handMouse", 1, 0.3f);
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

        try
        {
            if (GameObject.Find("HandRight").transform.position.y <= 1.45 && GameObject.Find("HandRight").transform.position.y >= -0.13)
            {

                if (GameObject.Find("HandRight").transform.position.x >= -6.42 && GameObject.Find("HandRight").transform.position.x <= -3.33)
                {


                    Together_Easy.click();
                    Together_Easy.click();
                    Together_Easy.click();

                }


            }
            if (Together_Easy.i==3)
            {

               

                    obj[0].SetActive(true);
                    done = 5;

               
            }

            if (GameObject.Find("HandRight").transform.position.y <= 0.37 && GameObject.Find("HandRight").transform.position.y >= -1.64 && done == 5)
            {

                if (GameObject.Find("HandRight").transform.position.x >= 0.85 && GameObject.Find("HandRight").transform.position.x <= 2.51)
                {

                    obj[1].SetActive(true);
                    how++;

                }
                else if (GameObject.Find("HandRight").transform.position.x >= 3.33 && GameObject.Find("HandRight").transform.position.x <= 4.95)
                {

                    obj[2].SetActive(true);
                    how1++;


                }
                else if (GameObject.Find("HandRight").transform.position.x >= 5.87 && GameObject.Find("HandRight").transform.position.x <= 7.34)
                {

                    how2++;

                    obj[3].SetActive(true);

                }

               

            }
            int a = 0;

            if (how >=1&&how1>=1&&how2>=1)
            {

                //obj[4].SetActive(true);
                //a = 1;

          

                Invoke("close", 3);
            }

            if (a==1 && GameObject.Find("HandRight").transform.position.y <= -2.02 && GameObject.Find("HandRight").transform.position.y >= -2.81)
            {

                if (GameObject.Find("HandRight").transform.position.x >= 5.3 && GameObject.Find("HandRight").transform.position.x <= 7.66)
                {
                   

                    obj[5].SetActive(true);
                    obj[6].SetActive(false);
                    obj[7].SetActive(false);

                    yes = true;
                }
            }

           if (yes&& GameObject.Find("HandRight").transform.position.y <= 1.5 && GameObject.Find("HandRight").transform.position.y >= -0.5)
            {
                print(2142412);
                if (GameObject.Find("HandRight").transform.position.x >= -6 && GameObject.Find("HandRight").transform.position.x <= -4.75)
                {
                    obj[8].SetActive(true);
                    print(1);

                    person[0].SetActive(true);
                    person[1].SetActive(false);
                    person[2].SetActive(false);
                    person[3].SetActive(false);

                    yes2 = true;
                }
                else if (GameObject.Find("HandRight").transform.position.x >= -2.93 && GameObject.Find("HandRight").transform.position.x <= -1.76)
                {

                    obj[8].SetActive(true);
                    print(2);

                    person[0].SetActive(false);
                    person[1].SetActive(true);
                    person[2].SetActive(false);
                    person[3].SetActive(false);

                    yes2 = true;


                }
                else if (GameObject.Find("HandRight").transform.position.x >=0.7 && GameObject.Find("HandRight").transform.position.x <= 1.85)
                {
                    obj[8].SetActive(true);
                    print(3);
                    yes2 = true;

                    person[0].SetActive(false);
                    person[1].SetActive(false);
                    person[2].SetActive(true);
                    person[3].SetActive(false);


                }
                else if (GameObject.Find("HandRight").transform.position.x >= 4.39 && GameObject.Find("HandRight").transform.position.x <= 5.52)
                {
                    obj[8].SetActive(true);
                    print(4);

                    person[0].SetActive(false);
                    person[1].SetActive(false);
                    person[2].SetActive(false);
                    person[3].SetActive(true);

                    yes2 = true;



                }
            }

            if (yes2 && GameObject.Find("HandRight").transform.position.y <= -2.37)
            {

                if (GameObject.Find("HandRight").transform.position.x >= -1.18&& GameObject.Find("HandRight").transform.position.x <= 1.1)
                {
                    obj[8].SetActive(false);
                    Invoke("wait", 1.5f);

                }

            }
        }
        catch { }

    }


    void close()
    {
        
                    //obj[5].SetActive(true);
                    //obj[6].SetActive(false);
                    //obj[7].SetActive(false);

                    //yes = true;

        obj[5].SetActive(true);
        obj[6].SetActive(false);
        obj[7].SetActive(false);

        yes = true;
    }

    void wait()
    {
        SceneManager.LoadScene("4.Normal");
    }

}

