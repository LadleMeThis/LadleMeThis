"use client"
import { login, register } from "@/scripts/auth";
import { useState } from "react";
import { IoIosClose } from "react-icons/io";
import { useToast } from "@/context/ToastContext";

export default function RegisterMode({ onClose, setIsLoginMode }) {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [username, setUsername] = useState("");
    const { showToast } = useToast()


    function changeModalMode() {
        setIsLoginMode(prev => !prev);
    }


    function handleClose(e) {
        if (e.target === e.currentTarget) {
            onClose();
        }
    };


    async function handleRegister(e) {
        try {
            e.preventDefault();
            await register({ Email: email, Username: username, Password: password })
            await login({ EmailOrUsername: email, Password: password })
            showToast("Register and login successful")
            onClose();
        } catch (e) {
            showToast("Something went wrong, try again!")
        }
    }


    return (
        <div className="modal-overlay" onClick={handleClose}>
            <div className="modal-content">
                <button className="close-btn" onClick={onClose}><IoIosClose />
                </button>
                <div className="modal-header">
                    <button onClick={changeModalMode} className="register-btn">Login</button><button className="active-btn">Register</button>
                </div>
                <form onSubmit={handleRegister} className="form-main">
                    <div className="form-inputs">
                        <div>
                            <label htmlFor="email">Email</label>
                            <input required onChange={(e) => setEmail(e.target.value)} type="email" id="email" placeholder="Enter your email" />
                        </div>
                        <div>
                            <label htmlFor="username">Username</label>
                            <input required onChange={(e) => setUsername(e.target.value)} type="text" id="username" placeholder="Enter your username" />
                        </div>
                        <div>
                            <label required htmlFor="password">Password</label>
                            <input required onChange={(e) => setPassword(e.target.value)} type="password" id="password" placeholder="Enter your password" />
                        </div>
                    </div>
                    <div className="buttons">
                        <button type="submit" className="submit-btn" >Submit</button>
                    </div>
                </form>
            </div>
        </div>
    )
}