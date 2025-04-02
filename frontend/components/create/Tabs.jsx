import React from "react";

const Tabs = ({ tabs, activeTab, onTabChange }) => {
  return (
    <div className="tabs-header">
      {tabs.map((tab) => (
        <button
          type="button"
          key={tab.id}
          className={`tab-button ${activeTab === tab.id ? "active" : ""}`}
          onClick={() => onTabChange(tab.id)}
        >
          {`${tab.label}|${tab.count}`}
        </button>
      ))}
    </div>
  );
};

export default Tabs;
