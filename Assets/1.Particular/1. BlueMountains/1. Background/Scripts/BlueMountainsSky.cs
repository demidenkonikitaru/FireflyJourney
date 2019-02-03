using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/* Summary: This script provides behaviour for sky elements.*/
public class BlueMountainsSky : BaseBackground
{
    
    [Header("Main objects and parameters.")]

    [Tooltip("Object to parent sky elements.")]
    [SerializeField]
    private GameObject objectForParenting;


    //private parameters for soon and moon

    private float daytimeCounter; //counter to control moon and sun movement
    private bool turnSwitch; //controls if it is moon movement or sun

    /* Sun object field and parameters that control it's movement. */

    #region Moon and Sun objects and parameters.

    [Header("1. Sun objects and parameters.")]

    [Tooltip("Sun object.")]
    [SerializeField]
    private GameObject sunObject;

    [Tooltip("Sun prefab.")]
    [SerializeField]
    private GameObject sunPrefab;

    [Tooltip("Start moving point for sun.")]
    [SerializeField]
    private Vector2 sunStartMovingPoint;

    [Tooltip("Finish moving point for sun.")]
    [SerializeField]
    private Vector2 sunFinishMovingPoint;

    [Tooltip("Multiplier for bezier curve height point.")]
    [SerializeField]
    private float bezierCurveMultiplierForSun; //multiplier to control height of bezier curve

    private Vector2 heightBezierCurvePointForSun1; //first height point for bezier curve along which sun moves

    private Vector2 heightBezierCurvePointForSun2; //second height point for bezier curve along which sun moves

    [Tooltip("Controls sprite speed movement.")]
    [SerializeField]
    [Range(0.001f, 0.1f)]
    private float speedValueForSun;


    /* Moon object field and parameters that control it's movement. */

    [Header("2. Moon objects and parameters.")]

    [Tooltip("Sun object.")]
    [SerializeField]
    private GameObject moonObject;

    [Tooltip("Sun object.")]
    [SerializeField]
    private GameObject moonPrefab;

    [Tooltip("Start moving point for moon.")]
    [SerializeField]
    private Vector2 moonStartMovingPoint;

    [Tooltip("Finish moving point for moon.")]
    [SerializeField]
    private Vector2 moonFinishMovingPoint;

    [Tooltip("Multiplier for bezier curve height point.")]
    [SerializeField]
    private float bezierCurveMultiplierForMoon; //multiplier to control height of bezier curve

    private Vector2 heightBezierCurvePointForMoon1; //first height point for bezier curve along which moon moves

    private Vector2 heightBezierCurvePointForMoon2; //first height point for bezier curve along which moon moves

    [Tooltip("Controls sprite speed movement.")]
    [SerializeField]
    [Range(0.001f, 0.1f)]
    private float speedValueForMoon;

    #endregion

    /* Clouds objects field and parameters that control it's movement. */

    [Header("3. Clouds objects and parameters.")]

    [Tooltip("Coordinates on X axis where clouds object should change position to go for a next loop.")]
    [SerializeField]
    public Transform finishPointRow1;
    public Transform finishPointRow2;
    public Transform finishPointRow3;

    [Tooltip("Coordinates on X axis where small clouds should start next loop.")]
    [SerializeField]
    public Transform startPointRow1;
    public Transform startPointRow2;
    public Transform startPointRow3;

    [Range(0.05f, 0.5f)]
    public float speed;

    public GameObject clouds1;
    public GameObject clouds2;
    public GameObject clouds3;
    public GameObject clouds4;
    public GameObject clouds5;
    public GameObject clouds6;


    #region 4. Stars Objects and parameters.

    [Header("4. Stars object and parameters.")]

    [Tooltip("Stars object.")]
    [SerializeField]
    private GameObject starsObject;

    [Tooltip("Stars rotation speed.")]
    [SerializeField]
    [Range(0.01f, 10f)]
    private float starsRotationSpeed;

    #endregion



    [Header("Animators.")]

    //animators of objects related to this background system
    [SerializeField]
    private Animator mountainsAndTrees1;
    [SerializeField]
    private Animator mountainsAndTrees2;
    [SerializeField]
    private Animator mountainsAndTrees3;
    [SerializeField]
    private Animator mountainsAndTrees4;
    [SerializeField]
    private Animator tower;
    [SerializeField]
    private Animator towerLight;
    [SerializeField]
    private Animator mountain1;
    [SerializeField]
    private Animator mountain2;
    [SerializeField]
    private Animator stars;
    [SerializeField]
    private Animator backgroundSky;
    [SerializeField]
    private Animator clouds1Anim;
    [SerializeField]
    private Animator clouds2Anim;
    [SerializeField]
    private Animator clouds3Anim;
    [SerializeField]
    private Animator clouds4Anim;
    [SerializeField]
    private Animator clouds5Anim;
    [SerializeField]
    private Animator clouds6Anim;

    #region Default methods.

    void Start() {

        AnimationsControl(false);

        daytimeCounter = 0f;

        //initializing bezier curve height point for sun
        InitializeCubicBezierCurve(sunStartMovingPoint, ref heightBezierCurvePointForSun1, ref heightBezierCurvePointForSun2,
            sunFinishMovingPoint, bezierCurveMultiplierForSun);
        //initializing bezier curve height point for moon
        InitializeCubicBezierCurve(moonStartMovingPoint, ref heightBezierCurvePointForMoon1, ref heightBezierCurvePointForMoon2,
            moonFinishMovingPoint, bezierCurveMultiplierForMoon);

        

    }

    void Update() {
        //moves sun and moon
        MoveMoonAndSunAlongBezierCurve(ref sunObject, sunStartMovingPoint, heightBezierCurvePointForSun1, heightBezierCurvePointForSun2, sunFinishMovingPoint,
           ref moonObject, moonStartMovingPoint, heightBezierCurvePointForMoon1, heightBezierCurvePointForMoon2, moonFinishMovingPoint);

        //rotates stars
        rotateStars();
    }

    #endregion

    #region Custom methods.

    #region Moon and Sun methods.

    //initialize bezier curve height point for moon and sun sprite movements
    private void InitializeCubicBezierCurve(Vector2 startPoint, ref Vector2 heightPoint1, ref Vector2 heightPoint2,
        Vector2 finishPoint, float heightMultiplier)
    {
        heightPoint1 = startPoint + (finishPoint - startPoint) / 3 + Vector2.up * heightMultiplier; //defines first height point for bezier curve
        heightPoint2 = startPoint + (finishPoint - startPoint) * 0.66f + Vector2.up * heightMultiplier; //defines second height point for bezier curve
    }

    private void MoveMoonAndSunAlongBezierCurve
       (ref GameObject sunSprite, Vector2 sunStartPoint, Vector2 sunCurveHeightPoint1, Vector2 sunCurveHeightPoint2, Vector2 sunFinishPoint,
        ref GameObject moonSprite, Vector2 moonStartPoint, Vector2 moonCurveHeightPoint1, Vector2 moonCurveHeightPoint2, Vector2 moonFinishPoint)
    {
        if (turnSwitch == false) //sun turn
        {
            if (daytimeCounter < 1.0f) //check if time is up
            {
                daytimeCounter += speedValueForSun * Time.deltaTime; //defines time for sun to pass the way

                //movement from start to first height point
                Vector2 m1 = Vector2.Lerp(sunStartPoint, sunCurveHeightPoint1, daytimeCounter);
                //movement from first to second height point
                Vector2 m2 = Vector2.Lerp(sunCurveHeightPoint1, sunCurveHeightPoint2, daytimeCounter);
                //movement from second height point to finish point
                Vector2 m3 = Vector2.Lerp(sunCurveHeightPoint2, sunFinishPoint, daytimeCounter); 

                //movement between m1 and m2
                Vector2 m4 = Vector2.Lerp(m1, m2, daytimeCounter);
                // movement between m2 and m3
                Vector2 m5 = Vector2.Lerp(m2, m3, daytimeCounter); 

                //apply movement to sprite
                sunSprite.GetComponent<Transform>().localPosition = Vector2.Lerp(m4, m5, daytimeCounter); 

                if (daytimeCounter >= 1.0f)
                {
                    daytimeCounter = 0f;
                    turnSwitch = !turnSwitch;
                    AnimationsControl(turnSwitch);
                }
            }
        }
        else if (turnSwitch == true) //moon turn
        {
            if (daytimeCounter < 1.0f) //check if time is up
            {
                //defines time for moon to pass the way
                daytimeCounter += speedValueForMoon * Time.deltaTime;

                //movement from start to first height point
                Vector2 m1 = Vector2.Lerp(moonStartPoint, moonCurveHeightPoint1, daytimeCounter);
                //movement from first to second height point
                Vector2 m2 = Vector2.Lerp(moonCurveHeightPoint1, moonCurveHeightPoint2, daytimeCounter);
                //movement from second height point to finish point
                Vector2 m3 = Vector2.Lerp(moonCurveHeightPoint2, moonFinishPoint, daytimeCounter);

                //movement between m1 and m2
                Vector2 m4 = Vector2.Lerp(m1, m2, daytimeCounter);
                // movement between m2 and m3
                Vector2 m5 = Vector2.Lerp(m2, m3, daytimeCounter);

                //apply movement to sprite
                moonSprite.GetComponent<Transform>().localPosition = Vector2.Lerp(m4, m5, daytimeCounter); 

                if (daytimeCounter >= 1.0f)
                {
                    daytimeCounter = 0f;
                    turnSwitch = !turnSwitch;
                    AnimationsControl(turnSwitch);
                }
            }
        }
    }

    #endregion

    #region Stars methods.

    //this method implements stars image rotation to imitate real sky behaviour
    private void rotateStars()
    {
        //sets rotating vector. Not 360 cause 360 considered as 0
        Vector3 targetRotation = new Vector3(0, 0, 359);

        //rotates stars object
        starsObject.transform.Rotate(-Vector3.forward, starsRotationSpeed * Time.deltaTime, Space.World);
    }

    #endregion

    #region Clouds methods.



    #endregion

    public void AnimationsControl(bool isNight)
    {
        if (isNight == false)
        {
            mountainsAndTrees1.Play("MountainsAndTrees1OnDawn", -1, 0f);
            mountainsAndTrees2.Play("MountainsAndTrees2OnDawn", -1, 0f);
            mountainsAndTrees3.Play("MountainsAndTrees3OnDawn", -1, 0f);
            mountainsAndTrees4.Play("MountainsAndTrees4OnDawn", -1, 0f);

            tower.Play("MountainsAndTrees2OnDawn", -1, 0f);
            towerLight.Play("TowerLightOff", -1, 0f);

            mountain1.Play("Mountain1OnDawn", -1, 0f);
            mountain2.Play("Mountain2OnDawn", -1, 0f);

            stars.Play("StarsFade", -1, 0f);
            backgroundSky.Play("SkyDawn", -1, 0f);

            clouds1Anim.Play("CloudsOnDawn", -1, 0f);
            clouds2Anim.Play("CloudsOnDawn", -1, 0f);
            clouds3Anim.Play("CloudsOnDawn", -1, 0f);

            clouds4Anim.Play("CloudsOnDawn", -1, 0f);
            clouds5Anim.Play("CloudsOnDawn", -1, 0f);
            clouds6Anim.Play("CloudsOnDawn", -1, 0f);
        }
        else
        {
            mountainsAndTrees1.Play("MountainsAndTrees1OnSunset", -1, 0f);
            mountainsAndTrees2.Play("MountainsAndTrees2OnSunset", -1, 0f);
            mountainsAndTrees3.Play("MountainsAndTrees3OnSunset", -1, 0f);
            mountainsAndTrees4.Play("MountainsAndTrees4OnSunset", -1, 0f);

            tower.Play("MountainsAndTrees2OnSunset", -1, 0f);
            towerLight.Play("TowerLightOn", -1, 0f);

            mountain1.Play("Mountain1OnSunset", -1, 0f);
            mountain2.Play("Mountain2OnSunset", -1, 0f);

            stars.Play("StarsReveal", -1, 0f);
            backgroundSky.Play("SkySunset", -1, 0f);

            clouds1Anim.Play("CloudsOnSunset", -1, 0f);
            clouds2Anim.Play("CloudsOnSunset", -1, 0f);
            clouds3Anim.Play("CloudsOnSunset", -1, 0f);

            clouds4Anim.Play("CloudsOnSunset", -1, 0f);
            clouds5Anim.Play("CloudsOnSunset", -1, 0f);
            clouds6Anim.Play("CloudsOnSunset", -1, 0f);
        }
    }

    #endregion
}


