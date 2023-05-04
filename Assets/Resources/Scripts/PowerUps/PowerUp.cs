using System;
using UnityEngine;

namespace LaninCode
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Destructible))]
    public abstract class PowerUp:MonoBehaviour,IOnDamage
    {
        private Animator _anim; 
        public Destructible Destruct { get; private set; }
        public Player PlayerToGetPowerUp { get; private set; }
        private Rigidbody2D _rbody2d;
        private const float JumpSpeed=2f;
        
        public virtual void Awake()
        {
            Destruct = GetComponent<Destructible>();
            Destruct.Awake();
            _anim = GetComponent<Animator>();
            _rbody2d = GetComponent<Rigidbody2D>();
        }

        public void SetHealth(float health)
        {
            _anim.SetFloat("Health",health);
        }

        public abstract void AddPowerUp();
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!Destruct.Checker.CheckForAppropriateTag(other.tag)) return;
            SetPlayerByName(other.name);
        }

        public void SetPlayerByName(string nameOfCol)
        {
            PlayerToGetPowerUp=SetPlayer(nameOfCol);
        }
        private Player SetPlayer(string nameOfCol)
        {
            var indexOfDelim = nameOfCol.IndexOf(' ');
            var nameOfPlayer =nameOfCol[..indexOfDelim];
            return Player.GetPlayer(nameOfPlayer);
        }
        
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
    }
}