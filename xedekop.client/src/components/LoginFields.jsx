import React, { useState, useEffect } from "react";
import api from "../api/api";
import { InputText } from "primereact/inputtext";
import { Password } from "primereact/password";
import { Button } from "primereact/button";
import { useAuth } from "../auth/AuthContext";

export default function LoginFields({ navigate }) {
    const { login } = useAuth();
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    const [isDisabled, setDisabled] = useState(true);

    useEffect(() => {
        setDisabled(!(username && password));
    }, [username, password]);

    const handleLogin = async () => {
        setLoading(true);
        setError("");

        try {
            const response = await api.post("Auth/login", {
                username,
                password
            });

            login(response.data.token);
            navigate("/shop")
        } catch (err) {
            const data = err.response?.data;

            if (Array.isArray(data)) {
                setError(data.map(e => e.description).join("\n"));
            } else if (typeof data === "object") {
                setError(data.detail || data.title || JSON.stringify(data));
            } else {
                setError(data || "Login failed");
            }
        }

        setLoading(false);
    };

    return (
        <>
            {error && <p style={{ color: "red" }}>{error}</p>}

            <div className="p-inputgroup flex-1">
                <span className="p-inputgroup-addon">
                    <i className="pi pi-user"></i>
                </span>
                <InputText value={username} onChange={(e) => setUsername(e.target.value)} placeholder="Username" />
            </div>

            <div className="p-inputgroup flex-1">
                <span className="p-inputgroup-addon">#</span>
                <Password value={password} onChange={(e) => setPassword(e.target.value)} placeholder="Password" />
            </div>

            <Button
                label="Login"
                icon="pi pi-check"
                raised
                loading={loading}
                disabled={isDisabled}
                onClick={handleLogin}
            />
        </>
    );
}