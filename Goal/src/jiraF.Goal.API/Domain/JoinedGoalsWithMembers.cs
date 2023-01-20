using jiraF.Goal.API.Domain.Dtos;

namespace jiraF.Goal.API.Domain
{
    public class JoinedGoalsWithMembers
    {
        public IEnumerable<GoalModel> Goals { get; private set; }
        private readonly IEnumerable<Member> _members;

        public JoinedGoalsWithMembers(
            IEnumerable<GoalModel> goals,
            IEnumerable<Member> members)
        {
            Goals = goals;
            _members = members;
        }

        public void Join()
        {
            Goals = from g in Goals
                    join m in _members on g.Reporter.Number equals m.Number into reporters
                    join m in _members on g.Assignee.Number equals m.Number into assignees
                    from r in reporters.DefaultIfEmpty()
                    from a in assignees.DefaultIfEmpty()
                    select new GoalModel(
                        g.Number,
                        g.Title,
                        g.Description,
                        r == null ? new Member() : new Member(r.Number, r.Name, r.Img),
                        a == null ? new Member() : new Member(a.Number, a.Name, a.Img),
                        g.DateOfCreate,
                        g.DateOfUpdate,
                        g.Label
                        );
        }
    }
}
