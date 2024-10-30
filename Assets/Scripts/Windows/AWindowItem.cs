using UnityEngine;

namespace Windows
{
    public abstract class AWindowItem<T> : MonoBehaviour where T : AWindowItemData
    {
        public abstract void Init(T data);
    }
}
