FROM postgres:14.12

ENV POSTGRES_DB=product_db
ENV POSTGRES_USER=myuser
ENV POSTGRES_PASSWORD=mypassword

COPY Data/DataBase/Migrations/20240512_create_database.sql /docker-entrypoint-initdb.d/

EXPOSE 5432
