"use client"
import { register } from "@/scripts/scripts";
import { useState } from "react";
import { IoIosClose } from "react-icons/io";

export default function RegisterMode({ onClose, setIsLoginMode }) {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [username, setUsername] = useState("");


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
            console.log("successfully registered!");
        } catch (e) {
            console.log(e.message)
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
                <form className="form-main">
                    <div className="form-inputs">
                        <div>
                            <label htmlFor="email">Email</label>
                            <input onChange={(e) => setEmail(e.target.value)} type="email" id="email" placeholder="Enter your email" />
                        </div>
                        <div>
                            <label htmlFor="username">Username</label>
                            <input onChange={(e) => setUsername(e.target.value)} type="text" id="username" placeholder="Enter your username" />
                        </div>
                        <div>
                            <label htmlFor="password">Password</label>
                            <input onChange={(e) => setPassword(e.target.value)} type="password" id="password" placeholder="Enter your password" />
                        </div>
                    </div>
                    <div className="buttons">
                        <button type="submit" className="submit-btn" onClick={(e) => handleRegister(e)}>Submit</button>
                    </div>
                </form>
            </div>
        </div>
    )
}