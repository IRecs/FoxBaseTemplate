using Engine.DI;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Camera
{
    public class ControllerVirtualCameras : MonoBehaviour, IDependency
    {
        [SerializeField] private UnityEngine.Camera m_InitCamera;

        [SerializeField] private string m_DefaultSwitch = "Default";
        [SerializeField] private List<CameraView> m_Views;

        private IVirtualCamerasManager m_VirtualCamerasManager;

        public void Inject()
        {
            DIContainer.RegisterAsSingle(m_InitCamera);
        }

        protected void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {

            m_VirtualCamerasManager = DIContainer.GetAsSingle<IVirtualCamerasManager>();

            m_VirtualCamerasManager.AddCameraView(m_Views.ToArray());
            m_VirtualCamerasManager.SwitchTo(m_DefaultSwitch);
        }

        protected void OnDestroy()
        {
            m_VirtualCamerasManager.Deinitialize();
        }
    }
}
