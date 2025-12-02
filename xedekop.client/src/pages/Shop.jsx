import { useAuth } from "../auth/AuthContext";
import { Button } from "primereact/button";
import { useNavigate } from "react-router-dom";
import ShopItems from "../components/ShopItems";
import "../styles/shop.css";

export default function Shop() {
    const { logout } = useAuth();
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate("/");
    };

    return (
        <div className="flex justify-content-center align-items-center min-h-screen"
            style={{ background: "linear-gradient(to bottom, #F9FAFB, #f0f0f0)" }}>

            <div className="pokemon-card-shop p-4 w-full" style={{ maxWidth: "900px" }}>
                <div className="flex align-items-center justify-content-between mb-3 gap-3">
                    <h1 className="pokemon-title">Pokemon Shop</h1>
                    <Button
                        label="Logout"
                        icon="pi pi-sign-out"
                        severity="danger"
                        className="mt-3"
                        onClick={handleLogout}
                    />
                </div>

                <ShopItems />
            </div>
        </div>
    );
}
