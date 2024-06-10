import React, { useState } from "react";
import Tab from "./Tab";
import "../../App.css";

const Tabs = ({ tabs, children }) => {
  const [activeTab, setActiveTab] = useState(0);

  const handleTabClick = (index) => {
    setActiveTab(index);
  };

  return (
    <div className="tabs-container">
      <div className="tabs">
        {tabs.map((tab, index) => (
          <Tab
            key={index}
            label={tab.label}
            onClick={() => handleTabClick(index)}
            isActive={index === activeTab}
          />
        ))}
      </div>
      <div className="tab-content">{children[activeTab]}</div>
    </div>
  );
};

export default Tabs;
