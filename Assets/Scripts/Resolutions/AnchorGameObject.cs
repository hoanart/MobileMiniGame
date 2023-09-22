using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class AnchorGameObject : MonoBehaviour
{
    public enum AnchorType
    {
        BottomLeft,
        BottomCenter,
        BottomRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        TopLeft,
        TopCenter,
        TopRight
    };

    public bool executeInUpdate;

    public AnchorType anchorType;
    public Vector3 anchorOffset;
    
    private IEnumerator updateAnchorRoutine;
    
    
    // Start is called before the first frame update
    void Start()
    {
        updateAnchorRoutine = UpdateAnchorSync();
        StartCoroutine(updateAnchorRoutine);
    }

    IEnumerator UpdateAnchorSync()
    {
        
        uint cameraWaitCycles = 0;
        while (CameraViewportHandler.Instance == null)
        {
            ++cameraWaitCycles;
            yield return new WaitForEndOfFrame();
        }

        if (cameraWaitCycles > 0)
        {
            print(string.Format("CameraAnchor found CameraFit instance after waiting {0} frame(s). " +
                                "You might want to check that CameraFit has an earlie execution order.", cameraWaitCycles));
        }

        UpdateAnchor();
        updateAnchorRoutine = null;
    }

    void UpdateAnchor()
    {
        switch (anchorType)
        {
            case AnchorType.BottomLeft:
                SetAnchor(CameraViewportHandler.Instance.Bottom.Left);
                break;
            case AnchorType.BottomCenter:
                SetAnchor(CameraViewportHandler.Instance.Bottom.Center);
                break;
            case AnchorType.BottomRight:
                SetAnchor(CameraViewportHandler.Instance.Bottom.Right);
                break;
            case AnchorType.MiddleLeft:
                SetAnchor(CameraViewportHandler.Instance.Middle.Left);
                break;
            case AnchorType.MiddleCenter:
                SetAnchor(CameraViewportHandler.Instance.Middle.Center);
                break;
            case AnchorType.MiddleRight:
                SetAnchor(CameraViewportHandler.Instance.Middle.Right);
                break;
            case AnchorType.TopLeft:
                SetAnchor(CameraViewportHandler.Instance.Top.Left);
                break;
            case AnchorType.TopCenter:
                SetAnchor(CameraViewportHandler.Instance.Top.Center);
                break;
            case AnchorType.TopRight:
                SetAnchor(CameraViewportHandler.Instance.Top.Right);
                break;
        }
    }

    void SetAnchor(Vector3 anchor)
    {
        Vector3 newPos = anchor + anchorOffset;
        
       // Debug.Log(anchor);
        if (!transform.position.Equals(newPos))
        {
            transform.position = newPos;
         //   Debug.Log(transform.position.ToString());
        }
    }
    #if UNITY_EDITOR
    // Update is called once per frame
    void Update()
    {
        if (updateAnchorRoutine == null && executeInUpdate)
        {
            updateAnchorRoutine = UpdateAnchorSync();
            StartCoroutine(updateAnchorRoutine);
        }
    }
    #endif
}
