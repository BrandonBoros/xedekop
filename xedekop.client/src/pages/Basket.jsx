import { Button } from "primereact/button";
import { useNavigate } from "react-router-dom";
import BasketItems from "../components/BasketItems"

export default function Basket() {
    const navigate = useNavigate();

    const handleExit = () => {
        navigate("/shop");
    };

    return (
        <div className="flex justify-content-center align-items-center min-h-screen">
            <div className="card p-4">
                <h1>Your Items</h1>

                <Button
                    label="Shop"
                    severity="secondary"
                    className="mt-3"
                    onClick={handleExit}
                />

                <BasketItems />
            </div>
        </div>
    );
}