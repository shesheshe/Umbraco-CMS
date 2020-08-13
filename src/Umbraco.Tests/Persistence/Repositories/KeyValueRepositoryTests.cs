﻿using System;
using NUnit.Framework;
using Umbraco.Core.Models;
using Umbraco.Core.Persistence.Repositories;
using Umbraco.Core.Persistence.Repositories.Implement;
using Umbraco.Core.Scoping;
using Umbraco.Tests.TestHelpers;
using Umbraco.Tests.Testing;

namespace Umbraco.Tests.Persistence.Repositories
{
    [TestFixture]
    [UmbracoTest(Database = UmbracoTestOptions.Database.NewSchemaPerTest)]
    public class KeyValueRepositoryTests : TestWithDatabaseBase
    {
        [Test]
        public void CanSetAndGet()
        {
            var provider = TestObjects.GetScopeProvider(Logger);

            // Insert new key/value
            using (var scope = provider.CreateScope())
            {
                var keyValue = new KeyValue
                {
                    Identifier = "foo",
                    Value = "bar",
                    UpdateDate = DateTime.Now,
                };
                var repo = CreateRepository(provider);
                repo.Save(keyValue);
                scope.Complete();
            }

            // Retrieve key/value
            using (var scope = provider.CreateScope())
            {
                var repo = CreateRepository(provider);
                var keyValue = repo.Get("foo");
                scope.Complete();

                Assert.AreEqual("bar", keyValue.Value);
            }

            // Update value
            using (var scope = provider.CreateScope())
            {
                var repo = CreateRepository(provider);
                var keyValue = repo.Get("foo");
                keyValue.Value = "buzz";
                keyValue.UpdateDate = DateTime.Now;
                repo.Save(keyValue);
                scope.Complete();
            }

            // Retrieve key/value again
            using (var scope = provider.CreateScope())
            {
                var repo = CreateRepository(provider);
                var keyValue = repo.Get("foo");
                scope.Complete();

                Assert.AreEqual("buzz", keyValue.Value);
            }
        }

        private IKeyValueRepository CreateRepository(IScopeProvider provider)
        {
            return new KeyValueRepository((IScopeAccessor) provider, Logger);
        }        
    }
}