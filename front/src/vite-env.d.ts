/// <reference types="vite/client" />

import { ReactComponentElement } from "react";

// src/vite-env.d.ts
declare module "*.svg" {
  const content: ReactComponentElement; // Aqui podemos usar `any` para permitir a importação sem complicação
  export default content;
}
