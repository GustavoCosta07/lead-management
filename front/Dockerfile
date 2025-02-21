FROM node:18

WORKDIR /app

COPY package.json package-lock.json ./
RUN npm install

RUN apt-get update && apt-get install -y netcat-openbsd

COPY . .

ENV VITE_PORT=3000

RUN npm test -- --run

CMD ["npm", "run", "dev", "--", "--host", "--port", "3000"]


