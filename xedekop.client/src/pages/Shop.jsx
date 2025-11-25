import { useAuth } from "../auth/AuthContext";
import { Button } from "primereact/button";
import { useNavigate } from "react-router-dom";
import ShopItems from "../components/ShopItems"

export default function Shop() {
    const { logout } = useAuth();
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate("/");
    };

    return (
        <div className="flex justify-content-center align-items-center min-h-screen">
            <div className="card p-4">
                <h1>Welcome to the Shop!</h1>

                <Button
                    label="Logout"
                    icon="pi pi-sign-out"
                    severity="danger"
                    className="mt-3"
                    onClick={handleLogout}
                />

                <ShopItems />
            </div>
        </div>
    );
}