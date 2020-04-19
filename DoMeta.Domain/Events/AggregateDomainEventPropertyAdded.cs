﻿using System;
using DoMeta.Domain.ValueObjects;

namespace DoMeta.Domain.Events
{
    public class AggregateDomainEventPropertyAdded : Kledex.Domain.DomainEvent
    {
        public string DomainEventName { get; set; }
        public Property Property { get; set; }
    }
}
