import React from "react";
import "./Button.css";
import { darkenColor } from "../../shared/utils/format";

interface ButtonProps {
  children: React.ReactNode;
  onClick: () => void;
  className?: string;
  color?: string;
  textColor?: string;
  borderBottom?: boolean;
}

const Button: React.FC<ButtonProps> = ({
  children,
  onClick,
  className,
  color,
  textColor,
  borderBottom
}) => {
  return (
    <button
      onClick={onClick}
      className={`custom-button ${className}`}
      style={{
        backgroundColor: color,
        color: textColor,
        borderBottom: borderBottom ? `2px solid ${darkenColor(color)}` : 'none',
      }}
    >
      {children}
    </button>
  );
};

export default Button;
