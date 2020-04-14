﻿using System;
using System.Collections.Generic;
using System.Linq;
using DoMeta.Domain;
using DoMeta.Domain.ValueObjects;
using NUnit.Framework;

namespace DoMeta.Test.Domain
{
    [TestFixture]
    public class EntityTests
    {
        [Test]
        public void CanRegisterEntity()
        {
            var boundedContextId = Guid.NewGuid();

            var entity = new Entity(boundedContextId, "Touchpoint");

            Assert.AreNotEqual(Guid.Empty, entity.Id);
            Assert.AreEqual(boundedContextId, entity.BoundedContextId);
            Assert.AreEqual("Id", entity.Identity.Name);
            Assert.AreEqual(new SystemType(typeof(Guid)), entity.Identity.Type);
            Assert.IsEmpty(entity.DomainEvents);
            Assert.IsEmpty(entity.Properties);
            Assert.False(entity.IsAggregateRoot);
        }

        [Test]
        public void CanAddDomainEventsToEntity()
        {
            var entity = new Entity(Guid.NewGuid(), "Touchpoint");

            entity.AddDomainEvent("TouchpointCreated");
            entity.AddDomainEvent("TouchpointSettingsChanged");

            Assert.AreEqual(2, entity.DomainEvents.Count);
            Assert.True(entity.IsAggregateRoot);
        }

        [Test]
        public void CanNotAddDomainEventsWithSameName()
        {
            var entity = new Entity(Guid.NewGuid(), "Touchpoint");

            entity.AddDomainEvent("TouchpointCreated");

            Assert.Throws<ArgumentException>(() => entity.AddDomainEvent("TouchpointCreated"));
        }

        [Test]
        public void CanAddPropertiesToEntity()
        {
            var entity = new Entity(Guid.NewGuid(), "Touchpoint");

            entity.AddProperty(new Property("Name", new SystemType(typeof(string))));
            entity.AddProperty(new Property("Description", new SystemType(typeof(string))));

            Assert.AreEqual(2, entity.Properties.Count);
        }

        [Test]
        public void CanAddPropertiesToDomainEvents()
        {
            var entity = new Entity(Guid.NewGuid(), "Touchpoint");

            entity.AddDomainEvent("TouchpointCreated");
            entity.AddPropertyToDomainEvent(entity.GetDomainEvent("TouchpointCreated").Id, new Property("Name", new SystemType(typeof(string))));

            Assert.IsNotEmpty(entity.DomainEvents);
            Assert.IsNotEmpty(entity.DomainEvents.First().Properties);
        }
    }
}