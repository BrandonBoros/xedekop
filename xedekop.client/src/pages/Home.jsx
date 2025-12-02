import { Button } from "primereact/button";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../auth/AuthContext";

export default function Home() {
    const navigate = useNavigate();
    const { isAuthenticated } = useAuth();

    return (
        <div className="flex justify-content-center align-items-center min-h-screen">
            <div className="card flex flex-column gap-3 p-4" style={{ maxWidth: "400px" }}>
                <h1>Welcome to Home</h1>

                {!isAuthenticated && (
                    <>
                        <Button label="Login" onClick={() => navigate('/login')} />
                        <Button label="Sign Up" severity="secondary" onClick={() => navigate('/register')} />
                    </>
                )}

                {isAuthenticated && (
                    <Button label="Go to Shop" onClick={() => navigate('/shop')} />
                )}
            </div>
        </div>
    );
}