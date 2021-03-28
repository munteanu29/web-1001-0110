FROM node:14-alpine as frontendBuilder

WORKDIR /app


COPY package.json package-lock.json ./
RUN npm i

COPY ./ ./
EXPOSE 80

CMD ["npm", "run","prod"]
