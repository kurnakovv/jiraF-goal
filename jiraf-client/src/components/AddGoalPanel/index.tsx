import axios from "axios";
import { IAddGoalRequestDto } from "./IAddGoalRequestDto";

const AddGoalPanel: React.FC<any> = (props) => {
    const handleAdd = (e: any): void => {
        e.preventDefault();
        
        const goalDto: IAddGoalRequestDto = {
            title: e.target.elements.title.value,
            description: e.target.elements.description.value,
            reporterId: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
            assigneeId: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
            labelTitle: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
        }

        try{
            axios.post("https://jiraf-goal.herokuapp.com/goal", goalDto, {
              headers: {
                "GoalApiKey": `${process.env.REACT_APP_GOAL_API_KEY}`,
              }
            });
            e.target.reset();
            props.setIsAddedGoal(true);
        } catch (error) {
            console.log(error);
        }
    }

    return (
        <>
            <div className="Container">
                <form onSubmit={handleAdd}>
                    <div className="addSection">
                        <p className="subTitle">Title</p>
                        <input name="title" placeholder="enter your title" />
                        <p className="subTitle">Description</p>
                        <input name="description" placeholder="enter your description" />
                        <br />
                        <button type="submit">Add</button>
                    </div>
                </form>
            </div>
        </>
    );
}

export default AddGoalPanel;