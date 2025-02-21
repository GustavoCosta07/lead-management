interface IconProps {
  size: number;
}

const SvgLocation: React.FC<IconProps> = ({ size }) => (
  <svg
    width={`${size}px`}
    height={`${size}px`}
    viewBox="0 0 1024 1024"
    xmlns="http://www.w3.org/2000/svg"
  >
    <path
      fill="#000000"
      d="M288 896h448q32 0 32 32t-32 32H288q-32 0-32-32t32-32z"
    />
    <path
      fill="#000000"
      d="M800 416a288 288 0 1 0-576 0c0 118.144 94.528 272.128 288 456.576C705.472 688.128 800 534.144 800 416zM512 960C277.312 746.688 160 565.312 160 416a352 352 0 0 1 704 0c0 149.312-117.312 330.688-352 544z"
    />
    <path
      fill="#000000"
      d="M512 512a96 96 0 1 0 0-192 96 96 0 0 0 0 192zm0 64a160 160 0 1 1 0-320 160 160 0 0 1 0 320z"
    />
  </svg>
);

const LocationIcon: React.FC<IconProps> = ({ size }) => {
  return <SvgLocation size={size} />;
};

export default LocationIcon;
