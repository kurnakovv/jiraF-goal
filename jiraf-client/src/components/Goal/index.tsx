import { IGoal } from "./IGoal"

const Goal: React.FC<IGoal> = ({ id, title, description }) => {
    return (
        <>
            <div className="Container">
                <h1>{title}</h1>
                <p>{description}</p>
            </div>
        </>
    );
}

export default Goal;