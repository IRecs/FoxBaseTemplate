using Engine.DI;
using System.Collections.Generic;
using UnityEngine;

namespace Main.DI
{
    public class MonoInjector : MonoBehaviour
    {
        [SerializeField] private Object[] m_Dependencies;

        private void Awake()
        {
            InjectorDependencies injector = new InjectorDependencies(m_Dependencies);
            injector.InjectAll();
        }

#if UNITY_EDITOR
        [NaughtyAttributes.Button("Reload Dependencies")]
        public void ReloadDependencies()
        {
            List<Object> allObjects = new List<Object>();

            allObjects.AddRange(editor.AssetUtility.FindScribtableObjectsOfType<ScriptableObject>());
            allObjects.AddRange(FindObjectsOfType(typeof(Object)));

            List<Object> objects = new List<Object>();
            foreach (Object obj in allObjects)
            {
                if (obj as IDependency != null) objects.Add(obj);
            }

            m_Dependencies = objects.ToArray();
        }
#endif
    }
}