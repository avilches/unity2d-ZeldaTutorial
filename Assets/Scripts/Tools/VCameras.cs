using UnityEngine;

/**
 * Stores all the Virtual Cinemachine cameras.
 * MUST BE TAGGED WITH THE "cinemachine" TAG
 *
 * Enable one of them with EnableVCam() method, which will disabled the others. 
 */
public class VCameras {
    private GameObject[] _cameraList;
    public static string TAG = "cinemachine";

    public void Init() {
        _cameraList = GameObject.FindGameObjectsWithTag(TAG);
/*
        var cameraListLength = _cameraList.Length;
        for (int i = 0; i < cameraListLength; i++) {
            Debug.Log(_cameraList[i].transform.name);
        }
*/
    }

    public void EnableVCam(GameObject vcam) {
        var cameraListLength = _cameraList.Length;
        for (int i = 0; i < cameraListLength; i++) {
            _cameraList[i].SetActive(_cameraList[i].gameObject == vcam);
        }
    }
    
}
