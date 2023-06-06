using System;
using UnityEngine;
using UnityEngine.Pool;

namespace LaninCode
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Destructible))]
    public abstract class PowerUp:MonoBehaviour,IPoolable,IOnDamage
    {
        private Animator _anim; 
        public Destructible Destruct { get; private set; }
        public Player PlayerToGetPowerUp { get; private set; }
        private Rigidbody2D _rbody2d;
        private IObjectPool<IPoolable> _pool;
        private const float JumpSpeed=2f;
        
        public virtual void Awake()
        {
            Destruct = GetComponent<Destructible>();
            Destruct.Awake();
            _anim = GetComponent<Animator>();
            _rbody2d = GetComponent<Rigidbody2D>();
        }

        public void SetHealth(int health)
        {
            _anim.SetInteger("Health",health);
        }

        public int MaxHealth { get; }

        /// <summary>
        /// Invoke PowerUp
        /// </summary>
        public abstract void AddPowerUp();
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!Destruct.Checker.CheckForAppropriateTag(other.tag)) return;
            SetPlayerByName(other.name);
        }

        /// <summary>
        /// Get player by collided object name
        /// </summary>
        /// <param name="nameOfCol"> collider's name, first part of it is used to find player</param>
        public void SetPlayerByName(string nameOfCol)
        {
            var indexOfDelim = nameOfCol.IndexOf(' ');
            var nameOfPlayer =nameOfCol[..indexOfDelim];
            PlayerToGetPowerUp=Player.GetPlayer(nameOfPlayer);
        }
 
        //todo не забыть поменять ресурсы 
        public static Sprite LoadSprite(string endingOfFile)
        {
            var spr = UnityEngine.Resources.Load<Sprite>($"Sprites/PowerUps/{endingOfFile}");
            if (spr == null) throw new NullReferenceException($"Sprites/PowerUps/{endingOfFile}");
            return spr;
        }

        public void JumpPowerUp()
        {
            _rbody2d.AddForce(Vector2.up*JumpSpeed,ForceMode2D.Impulse);
        }

        public void BackToPool()
        {
            _anim.StopPlayback();
            Destruct.Activate(OnOff.Off);
        }

        public void Activate(OnOff onOff)
        {
            throw new NotImplementedException();
        }

        public string Name { get; }
        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public void SetPool(IObjectPool<IPoolable> pool)
        {
            _pool = pool;
        }
    }
}