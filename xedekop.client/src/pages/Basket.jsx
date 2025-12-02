import { Button } from "primereact/button";
import { useNavigate } from "react-router-dom";
import BasketItems from "../components/BasketItems"

export default function Basket() {
    const navigate = useNavigate();

    const handleExit = () => {
        navigate("/shop");
    };

    return (
        <div className="flex justify-content-center align-items-center min-h-screen"
            style={{ background: "linear-gradient(to bottom, #F9FAFB, #f0f0f0)" }}>

            <div className="pokemon-card-shop p-4 w-full" style={{ maxWidth: "900px" }}>
                <div className="flex align-items-center justify-content-between mb-3 gap-3">
                    <h1 className="pokemon-title">Your Items</h1>

                    <Button
                        label="Shop"
                        severity="warning"
                        className="mt-3"
                        onClick={handleExit}
                    />
                </div>

                <BasketItems />
            </div>
        </div>
    );
}