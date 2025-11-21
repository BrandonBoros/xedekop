import React, { useState, useEffect } from "react";
import api from "../api/api";
import { InputText } from "primereact/inputtext";
import { Password } from "primereact/password";
import { Button } from "primereact/button";
import { useAuth } from "../auth/AuthContext";

export default function RegisterFields({ navigate }) {
    const { login } = useAuth();
    const [email, setEmail] = useState("");
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    const [isDisabled, setDisabled] = useState(true);

    useEffect(() => {
        setDisabled(!(email && username && password));
    }, [email, username, password]);

    const handleRegister = async () => {
        setLoading(true);
        setError("");

        try {
            const response = await api.post("Auth/register", {
                email,
                username,
                password,
            });

            login(response.data.token);
            navigate("/shop");
        } catch (err) {
            const data = err.response?.data;

            if (Array.isArray(data)) {
                // Identity errors come as an array of objects
                const messages = data.map(e => e.description).join("\n");
                setError(messages);
            } else {
                setError(data || "Registration failed");
            }
        }

        setLoading(false);
    };

    return (
        <>
            {error && <p style={{ color: "red" }}>{error}</p>}

            <div className="p-inputgroup flex-1">
                <span className="p-inputgroup-addon">@</span>
                <InputText value={email} onChange={(e) => setEmail(e.target.value)} placeholder="Email" />
            </div>

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
                label="Register"
                icon="pi pi-user-plus"
                raised
                loading={loading}
                disabled={isDisabled}
                onClick={handleRegister}
            />
        </>
    );
}