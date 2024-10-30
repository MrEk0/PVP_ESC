using Common;
using UnityEngine;

namespace Windows
{
    public abstract class AWindow : MonoBehaviour
    {
        public abstract void Init(ServiceLocator serviceLocator);
    }
}