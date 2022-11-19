using UnityEngine;
using System.Collections.Generic;
using Joint = Windows.Kinect.Joint;
using Windows.Kinect;
using UnityEngine.SceneManagement;

public class BodySourceViewHLast : MonoBehaviour
{
    public BodySourceManager bodySourceManager;
    public GameObject JointObject;
    private Dictionary<ulong, GameObject> mBodies = new Dictionary<ulong, GameObject>();
    public GameObject wash;

    public cardHard[] card;
    public cardHardPick[] card1;



    private List<JointType> joints = new List<JointType> {
    JointType.HandLeft,
    JointType.HandRight,
    };


    public GameObject[] obj;

    public static bool a;
    public static bool b;
    public static bool c;
    public static bool d;
    public static bool e;
    public static bool f;

    bool yes;
    bool yes1;
    bool yes2;


    public static bool one;

    public static int state;

    private void Start()
    {
        a = false;
        b = false;
        c = false;
        d = false;
        e = false;
        f = false;
        one = false;

        yes = false;
        yes1 = false;
        yes2 = false;
    }


    private void Update()
    {
        InvokeRepeating("handMouse", 7f, 1.3f);



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


            if (GameObject.Find("HandRight").transform.position.y >= -2.8 && GameObject.Find("HandRight").transform.position.y <= 0.09)
            {
                if (GameObject.Find("HandRight").transform.position.x >= -8.01 && GameObject.Find("HandRight").transform.position.x <= -5.71)
                {
                    card[0].OnMouseDown();
                    a = true;
                }
                else if (GameObject.Find("HandRight").transform.position.x >= -5.34 && GameObject.Find("HandRight").transform.position.x <= -3.09)
                {

                    card[1].OnMouseDown();
                    b = true;

                }
                else if (GameObject.Find("HandRight").transform.position.x >= -2.71 && GameObject.Find("HandRight").transform.position.x <= -0.57)
                {

                    card[2].OnMouseDown();
                    c = true;

                }



                else if (GameObject.Find("HandRight").transform.position.x >= 0.3 && GameObject.Find("HandRight").transform.position.x <= 1.97)
                {

                    card[3].OnMouseDown();
                    d = true;


                }
                else if (GameObject.Find("HandRight").transform.position.x >= 2.83 && GameObject.Find("HandRight").transform.position.x <= 4.52)
                {
                    card[4].OnMouseDown();

                    e = true;



                }
                else if (GameObject.Find("HandRight").transform.position.x >= 5.28 && GameObject.Find("HandRight").transform.position.x <= 7.35)
                {
                    card[5].OnMouseDown();
                    f = true;


                }


                if (a) { card[0].OnMouseDown(); }
                if (b) { card[1].OnMouseDown(); }
                if (c) { card[2].OnMouseDown(); }
                if (d) { card[3].OnMouseDown(); }
                if (e) { card[4].OnMouseDown(); }
                if (f) { card[5].OnMouseDown(); }


                if (GameObject.Find("兔子正確1") != null && GameObject.Find("兔子正確2") != null)
                {

                    a = false;
                    b = false;
                    yes = true;
                }

                if (GameObject.Find("正確1") != null && GameObject.Find("正確2") != null)
                {

                    c = false;
                    e = false;
                    yes1 = true;

                }

                if (GameObject.Find("正確a") != null && GameObject.Find("正確b") != null)
                {

                    d = false;
                    f = false;
                    yes2 = true;

                }




            }

            if (yes && yes1 && yes2)
            {
                Invoke("wait", 1.5f);

            }

            //if (cardHard.pairsFound == 3 )
            //{


            //    Invoke("done", 4);
            //}

            if (cardHardPick.pairsFound == 1)
            {

                Invoke("wait", 4);

            }
            else { }

            if (cardHardPick.pairsFound == 1)
                return;

            if (GameObject.Find("HandRight").transform.position.y >= 0.6 && GameObject.Find("HandRight").transform.position.y <= 2.82)
            {
                if (GameObject.Find("HandRight").transform.position.x <= 0.3 && GameObject.Find("HandRight").transform.position.x >= -1.37)
                {
                    card1[0].OnMouseDown();
                }
                if (GameObject.Find("HandRight").transform.position.x >= 2.26 && GameObject.Find("HandRight").transform.position.x <= 3.95)
                {

                    card1[1].OnMouseDown();
                }
                if (GameObject.Find("HandRight").transform.position.x >= 5.85 && GameObject.Find("HandRight").transform.position.x <= 7.55)
                {

                    card1[2].OnMouseDown();
                }
            }

            else if (GameObject.Find("HandRight").transform.position.y <= -0.51 && GameObject.Find("HandRight").transform.position.y >= -2.82)
            {
                if (GameObject.Find("HandRight").transform.position.x <= 0.3 && GameObject.Find("HandRight").transform.position.x >= -1.37)
                {

                    card1[3].OnMouseDown();

                }
                if (GameObject.Find("HandRight").transform.position.x >= 2.26 && GameObject.Find("HandRight").transform.position.x <= 3.95)
                {
                    card1[4].OnMouseDown();




                }
                if (GameObject.Find("HandRight").transform.position.x >= 5.85 && GameObject.Find("HandRight").transform.position.x <= 7.55)
                {
                    card1[5].OnMouseDown();


                }
            }


            //if (cardNormal.pairsFound == 3&&GameObject.Find("HandRight").transform.position.y >= -1.03 && GameObject.Find("HandRight").transform.position.y <= 0.7)
            //{
            //    if (GameObject.Find("HandRight").transform.position.x >= -1.42 && GameObject.Find("HandRight").transform.position.x <= -0.3)
            //    {
            //        state = 2;
            //        card1[0].pick();
            //    }
            //    if (GameObject.Find("HandRight").transform.position.x >= 1.92 && GameObject.Find("HandRight").transform.position.x <= 3.05)
            //    {
            //        state = 1;
            //        card1[1].pick();
            //    }
            //    if (GameObject.Find("HandRight").transform.position.x >= 5.2 && GameObject.Find("HandRight").transform.position.x <= 6.3)
            //    {
            //        state = 2;
            //        card1[2].pick();
            //    }
            //}

        
        }
        catch { }


    }

    void done()
    {
        obj[0].gameObject.SetActive(true);
        obj[1].gameObject.SetActive(false);
    }


    void wait()
    {
        SceneManager.LoadScene("2.Easy");
    }

}

