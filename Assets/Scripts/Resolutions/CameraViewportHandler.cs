using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraViewportHandler : MonoBehaviour
{
    public static CameraViewportHandler Instance = null;
    public struct BottomScreen
    {
        public Vector3 Left
        {
            get;
            set;
        }

        public Vector3 Center
        {
            get;
            set;
        }

        public Vector3 Right
        {
            get;
            set;
        }
    }

   public struct MiddleScreen
    {
        public Vector3 Left;
        public Vector3 Center;
        public Vector3 Right;
    }

   public struct TopScreen
    {
        public Vector3 Left;
        public Vector3 Center;
        public Vector3 Right;
    }
    
    public enum Constraint
    {
        LandScape,
        Portarit
    }
    
    public Color wireColor = Color.white;

    public float unitsSize = 1.0f;

    public Constraint constraint = Constraint.Portarit;



    public new Camera camera;

    public bool executeInUpdate;

    public float Width
    {
        get;
        set;
    }

    public float Height
    {
        get;
        set;
    }

    public BottomScreen Bottom
    {
        get => mBottom;
        set
        {
            mBottom = value;
        }
    }

    private BottomScreen mBottom;

    public MiddleScreen Middle
    {
        get
        {
            return    mMiddle;
        }
        set
        {
            mMiddle = value;
        }
        
    }

    private MiddleScreen mMiddle;
    
    public TopScreen Top => mTop;
    private TopScreen mTop;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        Instance = this;
        mBottom = new BottomScreen();
        mMiddle = new MiddleScreen();
        mTop = new TopScreen();
        ComputeResolution();
    }

    private void ComputeResolution()
    {
        float leftX;
        float rightX;
        float topY;
        float bottomY;

        if (constraint == Constraint.LandScape)
        {
            camera.orthographicSize = 1.0f / camera.aspect * unitsSize / 2.0f;
        }
        else
        {
            camera.orthographicSize = unitsSize / 2.0f;
        }

        Height = 2.0f * camera.orthographicSize;
        Width = Height * camera.aspect;

        float cameraX;
        float cameraY;

        cameraX = camera.transform.position.x;
        cameraY = camera.transform.position.y;

        leftX = cameraX - Width / 2;
        rightX = cameraX + Width / 2;
        topY = cameraY + Height / 2;
        bottomY = cameraY - Height / 2;

        mBottom.Left = new Vector3(leftX, bottomY, 0);
        mBottom.Center = new Vector3(cameraX, bottomY, 0);
        mBottom.Right = new Vector3(rightX, bottomY, 0);

        mMiddle.Left = new Vector3(leftX, cameraY, 0);
        mMiddle.Center = new Vector3(cameraX, cameraY, 0);
        mMiddle.Right = new Vector3(rightX, cameraY, 0);

        mTop.Left = new Vector3(leftX, topY, 0);
        mTop.Center = new Vector3(cameraX, topY, 0);
        mTop.Right = new Vector3(rightX, topY, 0);

    }

    // Update is called once per frame
    void Update()
    {
      #if UNITY_EDITOR
        if (executeInUpdate)
        {
            ComputeResolution();
        }
        #endif
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = wireColor;

        Matrix4x4 temp = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position,transform.rotation,Vector3.one);

        if (camera.orthographic)
        {
            float spread = camera.farClipPlane - camera.nearClipPlane;
            float center = (camera.farClipPlane + camera.nearClipPlane) * 0.5f;
            
            Gizmos.DrawWireCube(new Vector3(0,0,center),new Vector3(camera.orthographicSize*2*camera.aspect,camera.orthographicSize*2,spread));
        }
        else
        {
            Gizmos.DrawFrustum(Vector3.zero, camera.fieldOfView, camera.farClipPlane, camera.nearClipPlane, camera.aspect);
        }

        Gizmos.matrix = temp;
    }
}
