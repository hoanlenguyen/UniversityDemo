using System;

namespace UniversityDemo.Data.BaseEntities
{
    public interface IEntity<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}