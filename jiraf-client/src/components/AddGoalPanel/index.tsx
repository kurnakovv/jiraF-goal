import axios from "axios";
import { IAddGoalRequestDto } from "./IAddGoalRequestDto";

const AddGoalPanel: React.FC<any> = (props) => {
    return (
        <>
            <div className="Container">
                <form onSubmit={props.handleAdd}>
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