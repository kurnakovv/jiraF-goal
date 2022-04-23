﻿using jiraF.Goal.API.Domain;

namespace jiraF.Goal.API.Contracts;

public interface IGoalRepository
{
    Task<IEnumerable<GoalModel>> GetAsync();
}
