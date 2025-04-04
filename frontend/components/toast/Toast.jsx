"use client";
import { useState, useEffect } from "react";

export default function Toast({ message }) {
  const [visible, setVisible] = useState(false);

  useEffect(() => {
    if (message) {
      setVisible(true);
      setTimeout(() => setVisible(false), 2800);
    }
  }, [message]);

  return (
    <div className={`toaster-wrap ${visible ? "toast-show" : "toast-hide"}`}>
      <div className="toast">
        <div className="toast__border-top"></div>
        <div className="toast__top"></div>
        <div className="toast__bottom"></div>
        <div className="toast__border-bottom"></div>
        <div className="message">
          <div className="message__text">{message}</div>
        </div>

      </div>
    </div>
  );
}
