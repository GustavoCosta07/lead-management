interface IconProps {
    size: number;
  }
  
  const SvgMail: React.FC<IconProps> = ({ size}) => (
    <svg
      version="1.1"
      id="Icons"
      xmlns="http://www.w3.org/2000/svg"
      xmlnsXlink="http://www.w3.org/1999/xlink"
      viewBox="0 0 32 32"
      xmlSpace="preserve"
      width={size}
      height={size}
    >
      <style type="text/css">
        {`
          .st0 {
            fill: none;
            stroke: #000000;
            stroke-width: 2;
            stroke-linecap: round;
            stroke-linejoin: round;
            stroke-miterlimit: 10;
          }
          .st1 {
            fill: none;
            stroke: #000000;
            stroke-width: 2;
            stroke-linejoin: round;
            stroke-miterlimit: 10;
          }
        `}
      </style>
      <path
        className="st0"
        d="M25,27H7c-2.2,0-4-1.8-4-4V9c0-2.2,1.8-4,4-4h18c2.2,0,4,1.8,4,4v14C29,25.2,27.2,27,25,27z"
      />
      <polyline className="st0" points="3,10 16,18 29,10" />
    </svg>
  );
  
  const MailIcon: React.FC<IconProps> = ({ size }) => {
    return <SvgMail size={size} />;
  };
  
  export default MailIcon;
  