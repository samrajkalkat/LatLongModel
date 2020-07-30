/// <summary>
/// This is the linear Geo-Location algorithm for objects in Unity with accuracy errors in larger areas.
/// See "LatLon2UnityMercator" script for precise Geo-Location algorithm.
/// </summary>

using UnityEngine;

[ExecuteInEditMode]
public class LatLon2Unity : MonoBehaviour
{
    public double areaTop;
    public double areaBottom;
    public double areaLeft;
    public double areaRight;

    public double areaWidth;
    public double areaLength;

    public bool forceMoveToLatLon = true;
    public double destinationLat;
    public double destinationLon;

    public bool lockToCenter = false;

    public double scaleFactor = 1;

    private double currentLatitude;
    private double currentLongitude;
    private double worldSizeX;
    private double worldSizeY;

    private double LAT;
    private double LON;




    void Update ()
    {
        double originLatitude = (areaTop + areaBottom) / 2d;
        double originLongitude = (areaLeft + areaRight) / 2d;
        worldSizeX = areaWidth * scaleFactor;
        worldSizeY = areaLength * scaleFactor;

        if (lockToCenter)
        {
            LAT = originLatitude;
            LON = originLongitude;
        }
        else
        {
            LAT = destinationLat;
            LON = destinationLon;
        }

        if (transform.position.z >= 0)
            currentLatitude = originLatitude + ((areaTop - areaBottom) * (Mathf.InverseLerp(0, (float)worldSizeY, transform.position.z)));
        else
            currentLatitude = originLatitude - ((areaTop - areaBottom) * (Mathf.InverseLerp(0, (float)worldSizeY, -transform.position.z)));

        if(transform.position.x >= 0)
            currentLongitude = originLongitude + ((areaRight - areaLeft) * (Mathf.InverseLerp(0, (float)worldSizeX, transform.position.x)));
        else
            currentLongitude = originLongitude - ((areaRight - areaLeft) * (Mathf.InverseLerp(0, (float)worldSizeX, -transform.position.x)));

        if(forceMoveToLatLon)
            PerformGeoLocation();
        else
        {
            if (Application.isEditor && !Application.isPlaying)
                print("Current Lat: " + currentLatitude + "   Lon: " + currentLongitude);
        }
	}
    
    private void PerformGeoLocation ()
    {
        transform.position = new Vector3
            (
                (float)(((LON - areaLeft) / (areaRight - areaLeft)) * (worldSizeX)) - (float)(worldSizeX / 2f),
                20,
                (float)(((LAT - areaBottom) / (areaTop - areaBottom)) * (worldSizeY)) - (float)(worldSizeY / 2f)
            );
    }
}



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//[ExecuteInEditMode]
//public class LatLon2Unity : MonoBehaviour
//{
//	public double originLatitude;
//	public double originLongitude;
//	public double areaHeight;
//	public double areaWidth;
//
//	public double areaTop;
//	public double areaBottom;
//	public double areaLeft;
//	public double areaRight;
//
//	public bool forceMoveToLatLon = false;
//	public double destinationLat;
//	public double destinationLon;
//
//	private double currentLatitude;
//	private double currentLongitude;
//
//	void Update ()
//	{
//		if(transform.position.z >= 0)
//			currentLatitude = originLatitude + ((areaTop - areaBottom) * (Mathf.InverseLerp(0, (float)areaHeight, transform.position.z)));
//		else
//			currentLatitude = originLatitude - ((areaTop - areaBottom) * (Mathf.InverseLerp(0, (float)areaHeight, -transform.position.z)));
//
//		if(transform.position.x >= 0)
//			currentLongitude = originLongitude + ((areaRight - areaLeft) * (Mathf.InverseLerp(0, (float)areaWidth, transform.position.x)));
//		else
//			currentLongitude = originLongitude - ((areaRight - areaLeft) * (Mathf.InverseLerp(0, (float)areaWidth, -transform.position.x)));
//
//		print("Current Lat: " + currentLatitude + "   Lon: " + currentLongitude);
//
//		if(forceMoveToLatLon)
//			MoveToLatLon();
//	}
//
//	private void MoveToLatLon ()
//	{
//		transform.position = new Vector3
//			(
//				(float)(((destinationLon - areaLeft) / (areaRight - areaLeft)) * (areaWidth)) - (float)(areaWidth / 2f),
//				0,
//				(float)(((destinationLat - areaBottom) / (areaTop - areaBottom)) * (areaHeight)) - (float)(areaHeight / 2f)
//			);
//	}
//}
//
