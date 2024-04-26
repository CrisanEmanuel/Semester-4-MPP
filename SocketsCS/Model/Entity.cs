#nullable enable
using System;

namespace Model
{
    [Serializable]
    public class Entity<TId>
    {
        protected TId id;

        public TId Id
        {
            get => id;
            set => id = value;
        }

        public void SetId(TId tid)
        {
            id = tid;
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;
            return obj is Entity<TId> entity && Equals(Id, entity.Id);
        }

        public override int GetHashCode()
        {
            return Id!.GetHashCode();
        }

        public override string ToString()
        {
            return $"Entity{{id={id}}}";
        }
    }
}