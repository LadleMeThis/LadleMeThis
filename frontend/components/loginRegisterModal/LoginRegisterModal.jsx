"use client";
import { useState } from "react";
import LoginMode from "./LoginMode";
import RegisterMode from "./RegisterMode";


export default function LoginRegisterModal({ isOpen, onClose }) {
    const [isLoginMode, setIsLoginMode] = useState(true);
    if (!isOpen) return null;

    return (
        <>
            {isLoginMode ? <LoginMode onClose={onClose} setIsLoginMode={setIsLoginMode} /> : <RegisterMode setIsLoginMode={setIsLoginMode} onClose={onClose} />}
        </>
    );
}
