import React, { useState } from 'react';
import './Tabs.css';

interface TabsProps {
  tabs: string[];
  setActiveTab: (tab: string) => void;
  children: React.ReactNode[];
}

const Tabs: React.FC<TabsProps> = ({ tabs, setActiveTab, children }) => {
  const [activeTabIndex, setActiveTabIndex] = useState(0);

  const handleTabClick = (index: number, tab: string) => {
    setActiveTabIndex(index);
    setActiveTab(tab);
  };

  return (
    <div>
      <div className="tabs">
        {tabs.map((tab, index) => (
          <React.Fragment key={index}>
            <button
              className={`tab-button ${index === activeTabIndex ? 'active' : ''}`}
              onClick={() => handleTabClick(index, tab)}
            >
              {tab}
            </button>
            {index < tabs.length - 1 && <div className="tab-separator"></div>}
          </React.Fragment>
        ))}
      </div>
      {children[activeTabIndex]}
    </div>
  );
};

export default Tabs;
