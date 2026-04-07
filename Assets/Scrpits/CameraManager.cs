using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;

    public GameObject target; //카메라 포착 대상
    public float movespeed;
    private Vector3 targetPosition;// 대상의 위치
    public bool CameraMove = true;
 
    public BoxCollider2D bound;

    private Vector3 minBound;
    private Vector3 maxBound;

    private float halfWidth;
    private float halfHeight;
    
    private Camera theCamera;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    
    void Start()
    {
        theCamera = GetComponent<Camera>();
        
        //minBound = bound.bounds.min;
        //maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "FlyingGame")
        {
            this.transform.position = new Vector3(0f, 0f, this.transform.position.z);
        }

        if (target.gameObject != null && SceneManager.GetActiveScene().name != "FlyingGame" && CameraMove)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, movespeed * Time.deltaTime);

            /*
            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
            */
        }
    }

    /*public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
    */
}
