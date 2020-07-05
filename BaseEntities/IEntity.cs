using System;

namespace UniversityDemo.BaseEntities
{
    public interface IEntity<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}