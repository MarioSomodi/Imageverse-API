﻿using Imageverse.Domain.Models;

namespace Imageverse.Domain.UserActionAggregate.ValueObjects
{
    public sealed class UserActionId : ValueObject
    {
        public Guid Value { get; }

        private UserActionId(Guid value)
        {
            Value = value;
        }

        public static UserActionId Create(Guid value)
        {
            return new(value);
        }

        public static UserActionId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
