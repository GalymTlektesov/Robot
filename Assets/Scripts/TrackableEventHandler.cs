using Lean.Touch;
using UnityEngine;
using Vuforia;

public class TrackableEventHandler : DefaultTrackableEventHandler
{
    public static bool foundIs = false;
    private LeanScale Controller;

    protected override void Start()
    {
        Controller = GetComponentInChildren<LeanScale>();
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected override void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    //Объект появляется в поле зрения камеры
    protected override void OnTrackingFound()
    {
        if (mTrackableBehaviour)
        {
            Controller.gameObject.SetActive(true);

            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

            foreach (var component in rendererComponents)
                component.enabled = true;

            foreach (var component in colliderComponents)
                component.enabled = true;

            foreach (var component in canvasComponents)
                component.enabled = true;
        }
    }

    //Объект теряется с поле зрения камеры
    protected override void OnTrackingLost()
    {
        if (mTrackableBehaviour)
        {
            TouchObject.numberAnim = 0;
            Controller.gameObject.SetActive(false);

            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

            foreach (var component in rendererComponents)
                component.enabled = false;

            foreach (var component in colliderComponents)
                component.enabled = false;

            foreach (var component in canvasComponents)
                component.enabled = false;
        }
    }
}
