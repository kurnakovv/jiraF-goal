import React from 'react';
import logo from './logo.svg';
import './App.css';
import Goal from './components/Goal';
import axios from 'axios';
import { IGoal } from './components/Goal/IGoal';
import AddGoalPanel from './components/AddGoalPanel';
import { IAddGoalRequestDto } from './components/AddGoalPanel/IAddGoalRequestDto';

function App() {
  const [goals, setGoals] = React.useState<IGoal[]>([]);
  const [isAddedGoal, setIsAddedGoal] = React.useState<boolean>(false);

  React.useEffect(() => {
    axios.get("https://jiraf-goal.herokuapp.com/goal", {
      headers: {
        "GoalApiKey": `${process.env.REACT_APP_GOAL_API_KEY}`,
      }
    }).then(({ data }) => {
      setGoals(data.goals);
    });
  }, [])

  const handleAdd = (e: any): void => {
    e.preventDefault();
    
    // TODO: Try to fix any -> IAddGoalRequestDto
    const goalDto: any = {
        title: e.target.elements.title.value,
        description: e.target.elements.description.value,
        reporterId: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
        assigneeId: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
        labelTitle: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
    }

    try{
      setGoals((prev) => [...prev, goalDto])
      axios.post("https://jiraf-goal.herokuapp.com/goal", goalDto, {
        headers: {
          "GoalApiKey": `${process.env.REACT_APP_GOAL_API_KEY}`,
        }
      }).then(({ data }) => {
        setGoals((prev) => 
        prev.map((goal: IGoal) => {
          if (goal.id === undefined) {
            return {
              ...goal,
              id: data.id,
            }
          }
          return goal;
      }));
      });
      e.target.reset();
      setIsAddedGoal(true);
    } catch (error) {
      console.log(error);
    }
}

  const handleDeleteGoal = (id: string): void => {
    setIsAddedGoal(false);
    try {
      axios.delete(`https://jiraf-goal.herokuapp.com/goal?id=${id}`, {
        headers: {
          "GoalApiKey": `${process.env.REACT_APP_GOAL_API_KEY}`,
        }
      })
      setGoals((prev) => prev.filter((goal) => goal.id != id))
    } catch (error) {
      console.error(error);
    }
  }

  return (
    <div className="App">
      {isAddedGoal 
        ? <><h1>Added new goal!</h1><button onClick={() => setIsAddedGoal(false)}>X</button></>
        : <></>
      }
      <AddGoalPanel
        handleAdd={handleAdd}
      />
      {
        goals && goals.map((goal: IGoal) => {
          return (
            <>
              <Goal
                key={goal.id}
                id={goal.id}
                title={goal.title}
                description={goal.description}
              />
              <button onClick={() => handleDeleteGoal(goal.id)}>X</button>
            </>
          )
        })
      }
    </div>
  );
}

export default App;
