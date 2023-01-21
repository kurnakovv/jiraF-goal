using jiraF.Goal.API.Domain;
using jiraF.Goal.API.Domain.Dtos;
using jiraF.Goal.API.ValueObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace jiraF.Goal.UnitTests.Domain
{
    public class JoinedGoalsWithMembersTests
    {
        private readonly IEnumerable<GoalModel> _goals = new List<GoalModel>();
        private readonly IEnumerable<Member> _members = new List<Member>();

        public JoinedGoalsWithMembersTests()
        {
            Guid firstMemberNumber = new Guid("b8fa47d8-e41a-486f-aafd-3f6c578a31fa");
            Guid secondMemberNumber = new Guid("b8fa47d8-e41a-486f-aafd-3f6c578a31fb");
            _members = new List<Member>()
            {
                new Member(
                    firstMemberNumber,
                    "Member1",
                    "img"),
                new Member(
                    secondMemberNumber,
                    "Member2",
                    "img")
            };
            _goals = new List<GoalModel>()
            {
                new GoalModel(
                    new Title("Test value1"),
                    new Description("Test value1"),
                    firstMemberNumber,
                    firstMemberNumber,
                    "Test value1"),
                new GoalModel(
                    new Title("Test value2"),
                    new Description("Test value2"),
                    secondMemberNumber,
                    secondMemberNumber,
                    "Test value2"),
            };
        }

        [Fact]
        public void Join_CanJoinGoalsWithMembers_JoinedGoalsWithMembers()
        {
            JoinedGoalsWithMembers joinedGoalsWithMembers = new(_goals, _members);
            joinedGoalsWithMembers.Join();
            Assert.Equal(2, joinedGoalsWithMembers.Goals
                .Where(x => 
                    (
                     x.Assignee.Name == "Member1" && 
                     x.Assignee.Img == "img" &&
                     x.Reporter.Name == "Member1" &&
                     x.Reporter.Img == "img"
                    ) ||
                    (
                     x.Assignee.Name == "Member2" &&
                     x.Assignee.Img == "img" &&
                     x.Reporter.Name == "Member2" &&
                     x.Reporter.Img == "img"
                    )).Count());
        }
    }
}
