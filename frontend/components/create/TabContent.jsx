import React from "react";
import { getIdForActiveTab, getIconForActiveTab } from "@/scripts/scripts";
import { ClientPageRoot } from "next/dist/client/components/client-page";

const TabContent = ({ currentData, formData, activeTab, handleItemClick }) => {
  const currentSelectedItems = formData[activeTab];

  const id = getIdForActiveTab(activeTab);

  return (
    <div className="tab-content">
      {currentData.map((item) => {
        const isSelected = currentSelectedItems.some((i) =>
          i === item[id]);

        return (
          <div
            key={item[id] + item.name}
            className={`tab-item ${isSelected ? "selected" : ""}`}
            onClick={() =>
              handleItemClick(item[id]) 
            }
          >
            <span className="tab-item-icon">
              {getIconForActiveTab(activeTab)}
            </span>
            {item.name}
          </div>
        );
      })}
    </div>
  );
};

export default TabContent;
