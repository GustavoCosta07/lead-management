interface IconProps {
  size: number;
}

const SvgWork: React.FC<IconProps> = ({ size }) => (
  <svg
    fill="#000000"
    version="1.1"
    id="Ebene_1"
    xmlns="http://www.w3.org/2000/svg"
    width={`${size}px`}
    height={`${size}px`}
    viewBox="0 0 64 64"
  >
    <g>
      <path
        d="M62,16H42.727C42.064,13.082,39.791,8,32,8s-10.064,5.082-10.727,8H2c-1.104,0-2,0.896-2,2v36c0,1.104,0.896,2,2,2h60
            c1.104,0,2-0.896,2-2V18C64,16.896,63.104,16,62,16z M32,12c4.335,0,5.98,2.244,6.608,4H25.388C26.018,14.253,27.669,12,32,12z
             M60,20v8H4v-8H60z M29,32h6v2h-6V32z M4,52V32h21v4c0,1.104,0.896,2,2,2h10c1.104,0,2-0.896,2-2v-4h21v20H4z"
      />
    </g>
  </svg>
);

const WorkIcon: React.FC<IconProps> = ({ size }) => {
  return <SvgWork size={size} />;
};

export default WorkIcon;
