import { format } from "date-fns";

export const formatDate = (date: string) => {
  const formattedDate = format(date, "MMMM d '@' h:mm a");
  return formattedDate.replace(/\s?(AM|PM)$/i, (match) => match.toLowerCase());
};

export const darkenColor = (color: string | undefined) => {
    if (!color) return;

    if (color[0] === '#') {
      color = color.slice(1);
    }
  
    let r = parseInt(color.substr(0, 2), 16);
    let g = parseInt(color.substr(2, 2), 16);
    let b = parseInt(color.substr(4, 2), 16);
  
    r = Math.max(0, r - r * 0.25);
    g = Math.max(0, g - g * 0.25);
    b = Math.max(0, b - b * 0.25);
  
    const darkenedColor = `#${((1 << 24) | (r << 16) | (g << 8) | b).toString(16).slice(1).toUpperCase()}`;
    return darkenedColor;
  };
  