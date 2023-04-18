using UnityEngine;

namespace LaninCode
{
    public class WeaponCursor : MonoBehaviour,IOnAttackFinished
    {
        [SerializeField] private string _nameOfWeapon;
        public bool IsShooting { get; set; }
        //TODO изменим класс. чтобы он содержал не только имя 
        public string NameOfWeapon
        {
            get => _nameOfWeapon;
            set => _nameOfWeapon = value;
        }
        public IOnAttackFinished Attacker { get; set; }
        public void OnAttackFinished()
        {
            Attacker?.OnAttackFinished();
        }

        private void Awake()
        {
            GameManager.UsedCursors.Add(this);
        }
    }
}