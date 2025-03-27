"use client"
import { login } from "@/scripts/scripts";
import { useState } from "react";
import { IoIosClose } from "react-icons/io";

export default function LoginMode({ setIsLoginMode, onClose }) {
    const [emailOrUsername, setEmailOrUsername] = useState("");
    const [password, setPassword] = useState("");


    function changeModalMode() {
        setIsLoginMode(prev => !prev);
    }

    function handleClose(e) {
        if (e.target === e.currentTarget) {
            onClose();
        }
    };


    async function handleLogin(e) {
        try {
            e.preventDefault();
            await login({ EmailOrUsername: emailOrUsername, Password: password })
            console.log("successfully logged in!");
        } catch (e) {
            console.log(e.message);
        }
    }

    // admin@example.com",
    //  Password = "Admin@123"

    return (
        <div className="modal-overlay" onClick={handleClose}>
            <div className="modal-content">
                <button className="close-btn" onClick={onClose}><IoIosClose />
                </button>
                <div className="modal-header">
                    <button className="active-btn" >Login</button><button onClick={changeModalMode} >Register</button>
                </div>
                <form className="form-main">
                    <div className="form-inputs">
                        <div>
                            <label htmlFor="email">Email or Username</label>
                            <input onChange={(e) => setEmailOrUsername(e.target.value)} type="email" id="email" placeholder="Enter your email or username" />
                        </div>
                        <div>
                            <label htmlFor="password">Password</label>
                            <input onChange={(e) => setPassword(e.target.value)} type="password" id="password" placeholder="Enter your password" />
                        </div>
                    </div>
                    <div className="buttons">
                        <button type="submit" className="submit-btn" onClick={(e) => handleLogin(e)}>Login</button>
                    </div>
                </form>
            </div>
        </div>
    )
}