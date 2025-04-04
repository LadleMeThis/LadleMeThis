"use client"
import Toast from "@/components/toast/Toast";
import { createContext, useContext, useState } from "react";

const ToastContext = createContext();

export function ToastProvider({ children }) {
  const [toast, setToast] = useState(null);

  const showToast = (message) => {
    setToast(message);
    setTimeout(() => setToast(null), 3000);
  };

  return (
    <ToastContext.Provider value={{ showToast }}>
      {children}
      <Toast message={toast} />
    </ToastContext.Provider>
  );
}

export function useToast() {
  return useContext(ToastContext);
}
