FROM postgres
ENV POSTGRES_PASSWORD docker

ENV PORT 5432

EXPOSE ${PORT}

COPY /sql_scripts/create.sql /docker-entrypoint-initdb.d/
COPY /sql_scripts/insert.sql /docker-entrypoint-initdb.d/