FROM node:14-alpine as frontendBuilder

WORKDIR /app


COPY package.json package-lock.json ./
RUN npm ci --silent

COPY ./ ./

RUN npm run build

# =================== RUNTIME ===================

FROM nginx:alpine

COPY --from=frontendBuilder /app/build /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80

