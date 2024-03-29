﻿using jiraF.Goal.API.Dtos.Label;

namespace jiraF.Goal.API.Dtos.Goal;

public class GoalDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public MemberDto Reporter { get; set; }
    public MemberDto Assigee { get; set; }
    public DateTime DateOfCreate { get; set; }
    public DateTime DateOfUpdate { get; set; }
    public LabelDto Label { get; set; }
}
