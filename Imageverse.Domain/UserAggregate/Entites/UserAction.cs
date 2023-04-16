﻿using Imageverse.Domain.Models;
using Imageverse.Domain.UserAggregate.ValueObjects;

namespace Imageverse.Domain.UserAggregate.Entites
{
    public sealed class UserAction : Entity<UserActionId>
    {
        public string Name { get; }
        public string Description { get; }

        private UserAction(
            UserActionId userActionId,
            string name,
            string description)
            : base(userActionId)
        {
            Name = name;
            Description = description;
        }

        public static UserAction Create(string name, string description)
        {
            return new(
                UserActionId.CreateUnique(),
                name,
                description);
        }
    }
}