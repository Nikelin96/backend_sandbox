FROM postgres
ENV POSTGRES_PASSWORD docker

ENV PORT 5432

EXPOSE ${PORT}

# COPY /sql_scripts/create_database_postgres.sql /docker-entrypoint-initdb.d/
# COPY /sql_scripts/insert_records_postgres.sql /docker-entrypoint-initdb.d/

COPY /sql_scripts/create_database_heroes.sql /docker-entrypoint-initdb.d/