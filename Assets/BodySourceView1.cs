using UnityEngine;
using System.Collections.Generic;
using Joint = Windows.Kinect.Joint;
using Windows.Kinect;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BodySourceView1 : MonoBehaviour
{
    public BodySourceManager bodySourceManager;
    public GameObject JointObject;
    private Dictionary<ulong, GameObject> mBodies = new Dictionary<ulong, GameObject>();

    public bool  i = true;

    public GameObject[] obj;
    public Text win;

    public Image[] button_sel;
    public static bool yse;

    public GameObject btnnn;


    private void Start()
    {
        i = true;
        yse = false;
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
        InvokeRepeating("handMouse", 1,0.01f);
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
                if (GameObject.Find("HandRight").transform.position.y <= -3.23 && GameObject.Find("HandRight").transform.position.y >= -4.26)
                {


                    if (GameObject.Find("HandRight").transform.position.x >= 3.72 && GameObject.Find("HandRight").transform.position.x <= 7.51)
                    {

                    btnnn.SetActive(false);
                        Invoke("wait", 1.5f);

                    }
                }
            }
            catch { }
        
    }


    void close()
    {
        obj[1].SetActive(false);
        obj[2].SetActive(false);
        win.text = "沒關係，再一次";
    }

    void wait()
    {
        SceneManager.LoadScene("Home");
    }

}

