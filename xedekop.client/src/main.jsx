import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { PrimeReactProvider } from 'primereact/api';
import { AuthProvider } from './auth/AuthContext';
import PrivateRoute from './auth/PrivateRoute';
import Home from './pages/Home';
import { Login } from './pages/Login';
import { Register } from './pages/Register';
import Shop from './pages/Shop';
import Basket from './pages/Basket';
import 'primereact/resources/themes/lara-light-blue/theme.css';
import 'primereact/resources/primereact.min.css';
import 'primeicons/primeicons.css';
import 'primeflex/primeflex.css';

createRoot(document.getElementById('root')).render(
    <StrictMode>
        <PrimeReactProvider>
            <AuthProvider>
                <BrowserRouter>
                    <Routes>
                        <Route path="/" element={<Home />} />
                        <Route path="/login" element={<Login />} />
                        <Route path="/register" element={<Register />} />

                        <Route
                            path="/shop"
                            element={
                                <PrivateRoute>
                                    <Shop />
                                </PrivateRoute>
                            }
                        />

                        <Route
                            path="/basket"
                            element={
                                <PrivateRoute>
                                    <Basket />
                                </PrivateRoute>
                            }
                        />

                        <Route path="*" element={<Navigate to="/" />} />
                    </Routes>
                </BrowserRouter>
            </AuthProvider>
        </PrimeReactProvider>
    </StrictMode>,
);