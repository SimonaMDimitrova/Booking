namespace Booking.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Booking.Data.Models;
    using Booking.Web.ViewModels.PropertiesViewModels.Add;
    using Booking.Web.ViewModels.PropertiesViewModels.Edit;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class RulesServiceTests : BaseServiceTests
    {
        private IRulesService Service => this.ServiceProvider.GetRequiredService<IRulesService>();

        [Fact]
        public void CheckGetAllMethod()
        {
            var rules = this.GetAddedToDbRules();
            var expectedResult = new List<RuleInputModel>();
            foreach (var rule in rules)
            {
                expectedResult.Add(new RuleInputModel
                {
                    Id = rule.Id,
                    Name = rule.Name,
                });
            }

            var actualResult = this.Service.GetAll().ToList();

            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetAllByPropertyId()
        {
            var rules = this.GetAddedToDbRules().ToList();

            var property = new Property
            {
                Address = "some address",
                ApplicationUserId = Guid.NewGuid().ToString(),
                TownId = 1,
                Stars = 5,
                Floors = 2,
                Name = "some name",
                PropertyCategoryId = 1,
            };

            this.DbContext.Properties.Add(property);
            this.DbContext.SaveChanges();

            var index = 0;
            var expectedResult = new List<EditRuleInputModel>();
            foreach (var rule in rules)
            {
                var inputRule = new EditRuleInputModel();
                if (index % 2 == 0)
                {
                    property.PropertyRules.Add(new PropertyRule { RuleId = rule.Id, IsAllowed = true });
                    inputRule.Id = rule.Id;
                    inputRule.IsAllowed = true;
                    inputRule.Name = rule.Name;
                }
                else
                {
                    property.PropertyRules.Add(new PropertyRule { RuleId = rule.Id, IsAllowed = false });
                    inputRule.Id = rule.Id;
                    inputRule.IsAllowed = false;
                    inputRule.Name = rule.Name;
                }

                expectedResult.Add(inputRule);
                index++;
            }

            var actualResult = this.Service.GetAllByPropertyId(property.Id).ToList();

            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Equal(expectedResult, actualResult);
        }

        private IEnumerable<Rule> GetAddedToDbRules()
        {
            var rules = new List<Rule>();
            for (int i = 0; i < 4; i++)
            {
                var rule = new Rule
                {
                    Name = $"Rule{i}",
                };
            }

            this.DbContext.Rules.AddRange(rules);
            this.DbContext.SaveChanges();

            return rules;
        }
    }
}
