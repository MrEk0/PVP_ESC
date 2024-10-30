using UnityEngine;
using UnityEngine.UI;

namespace Characters
{
    public class CharacterHpView : MonoBehaviour
    {
        [SerializeField] private Image _hpBarImage;

        private Character _owner;
        
        public void Init(Character owner)
        {
            _owner = owner;
            
            _hpBarImage.type = Image.Type.Filled;
            _hpBarImage.fillMethod = Image.FillMethod.Horizontal;
            _hpBarImage.fillAmount = 1f;

            _owner.Hp.UpdateEvent += OnUpdateReceived;
        }

        private void OnDestroy()
        {
            _owner.Hp.UpdateEvent -= OnUpdateReceived;
        }

        private void OnUpdateReceived()
        {
            _hpBarImage.fillAmount = _owner.Hp.GetHpRate;
        }
    }
}
