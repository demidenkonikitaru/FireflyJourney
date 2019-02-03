using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsOnBlueMountains : MonoBehaviour
{
    public BlueMountainsSky skyManager;

    [Range(0.05f, 0.5f)]
    public float speed;

    private Vector2 cloudsPosition2D;

    public enum CloudsRow
    {
        Row1 = 0,
        Row2 = 2,
        Row3 = 3
    }

    public CloudsRow row;


    //__________________________________________________________________


    #region Default methods.

    void Start () {
        cloudsPosition2D = transform.position;
	}
	
	
	void Update () {
        MoveCloud();
        cloudsPosition2D = transform.position;
    }

    #endregion


    //__________________________________________________________________


    #region Custom methods.

    private void MoveCloud()
    {
        if (row == CloudsRow.Row1)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(skyManager.finishPointRow1.position.x, transform.position.y),
                speed * Time.deltaTime);

            if (cloudsPosition2D.x == skyManager.finishPointRow1.position.x)
            {
                transform.position = new Vector2(skyManager.startPointRow1.position.x, transform.position.y);
            }
        }

        if (row == CloudsRow.Row2)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(skyManager.finishPointRow2.position.x, transform.position.y),
                speed * Time.deltaTime);

            if (cloudsPosition2D.x == skyManager.finishPointRow2.position.x)
            {
                transform.position = new Vector2(skyManager.startPointRow2.position.x, transform.position.y);
            }
        }

        if (row == CloudsRow.Row3)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(skyManager.finishPointRow3.position.x, transform.position.y),
                speed * Time.deltaTime);

            if (cloudsPosition2D.x == skyManager.finishPointRow3.position.x)
            {
                transform.position = new Vector2(skyManager.startPointRow3.position.x, transform.position.y);
            }
        }

    }

    #endregion
}
