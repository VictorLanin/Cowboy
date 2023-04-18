using System.Collections.Generic;
using UnityEngine;

namespace LaninCode
{
    //We use this class for collision checks! 
    public class CollisionChecker
    {
        private List<string> _tags = new List<string>(10);
        private List<ContactPoint2D> _point2Ds=new List<ContactPoint2D>(20);
        private CollisionChecker(List<string> tags)
        {
            _tags.AddRange(tags);
        }

        public static CollisionChecker CreateInstance(List<string> tags)
        {
            return new CollisionChecker(tags);
        }

        public bool CheckForAppropriateTag(string tag)
        {
            return _tags.Contains(tag);
        }

        public bool IsObjectOutOfCollider(Collider2D col)
        {
            return col.GetContacts(_point2Ds) == 0;
        }
        
    }
}