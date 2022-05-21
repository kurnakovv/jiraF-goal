import React from 'react';
import logo from './logo.svg';
import './App.css';
import Goal from './components/Goal';
import axios from 'axios';
import { IGoal } from './components/Goal/IGoal';

function App() {
  const [goals, setGoals] = React.useState<IGoal[]>([]);

  React.useEffect(() => {
    axios.get("https://jiraf-goal.herokuapp.com/goal", {
      headers: {
        "GoalApiKey": `${process.env.REACT_APP_GOAL_API_KEY}`,
      }
    }).then(({ data }) => {
      setGoals(data.goals);
    });
  }, [])

  return (
    <div className="App">
      {
        goals && goals.map((goal: IGoal) => {
          return (
            <Goal
              key={goal.id}
              id={goal.id}
              title={goal.title}
              description={goal.description}
            />
          )
        })
      }
    </div>
  );
}

export default App;
