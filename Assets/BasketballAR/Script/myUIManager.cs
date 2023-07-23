using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class myUIManager : MonoBehaviour
{
    [SerializeField] ARCameraManager m_CameraManager;

    public ARCameraManager cameraManager
    {
        get { return m_CameraManager; }
        set
        {
            if (m_CameraManager == value)
                return;

            if (m_CameraManager != null)
                m_CameraManager.frameReceived -= FrameChanged;

            m_CameraManager = value;

            if (m_CameraManager != null & enabled)
                m_CameraManager.frameReceived += FrameChanged;
        }
    }

    const string k_FadeOffAnim = "FadeOff";
    const string k_FadeOnAnim = "FadeOn";

    [SerializeField] ARPlaneManager m_PlaneManager;

    public ARPlaneManager planeManager
    {
        get { return m_PlaneManager; }
        set { m_PlaneManager = value; }
    }

    [SerializeField] Animator m_MoveDeviceAnimation;

    public Animator moveDeviceAnimation
    {
        get { return m_MoveDeviceAnimation; }
        set { m_MoveDeviceAnimation = value; }
    }

    [SerializeField] Animator m_TapToPlaceAnimation;

    public Animator tapToPlaceAnimation
    {
        get { return m_TapToPlaceAnimation; }
        set { m_TapToPlaceAnimation = value; }
    }

    static List<ARPlane> s_Planes = new List<ARPlane>();

    bool m_ShowingTapToPlace = false;

    bool m_ShowingMoveDevice = true;

    const float k_AnimationTime = 3.0f;
    const float k_FadeTime = 1.0f;

    void OnEnable()
    {
        if (m_CameraManager != null)
            m_CameraManager.frameReceived += FrameChanged;

        PlaceHoop.onPlacedObject += PlacedObject;
    }

    void OnDisable()
    {
        if (m_CameraManager != null)
            m_CameraManager.frameReceived -= FrameChanged;

        PlaceHoop.onPlacedObject -= PlacedObject;
    }

    void FrameChanged(ARCameraFrameEventArgs args)
    {
        if (PlanesFound() && m_ShowingMoveDevice)
        {
            StartCoroutine(PlayAnimations());
        }
    }

    IEnumerator PlayAnimations()
    {
        m_ShowingMoveDevice = false;
        if (moveDeviceAnimation)
        {
            moveDeviceAnimation.SetTrigger(k_FadeOffAnim);
            yield return new WaitForSeconds(k_AnimationTime);
            moveDeviceAnimation.SetBool(k_FadeOffAnim, false);
            yield return new WaitForSeconds(k_FadeTime);
        }

        m_ShowingTapToPlace = true;
        if (tapToPlaceAnimation)
        {
            tapToPlaceAnimation.SetTrigger(k_FadeOnAnim);
            yield return new WaitForSeconds(k_AnimationTime);
            tapToPlaceAnimation.SetBool(k_FadeOnAnim, false);
            yield return new WaitForSeconds(k_FadeTime);
        }

        m_ShowingTapToPlace = false;
    }

    bool PlanesFound()
    {
        if (planeManager == null)
            return false;

        return planeManager.trackables.count > 0;
    }

    void PlacedObject()
    {
        if (m_ShowingTapToPlace)
        {
            if (tapToPlaceAnimation)
                tapToPlaceAnimation.SetTrigger(k_FadeOffAnim);

            m_ShowingTapToPlace = false;
        }
    }
}
