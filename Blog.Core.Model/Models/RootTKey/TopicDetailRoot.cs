using System;

namespace Blog.Core.Model.Models
{
    public class TopicDetailRoot<TKey> : RootEntityTKey<TKey> where TKey : IEquatable<TKey>
    {
        public TKey TopicId { get; set; }
    }
}