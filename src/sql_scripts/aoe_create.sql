BEGIN;
SET default_tablespace = pg_default;
DO $$ BEGIN IF NOT EXISTS (
    SELECT 1
    FROM pg_type
    WHERE typname = 'transaction_type'
) THEN CREATE TYPE transaction_type AS ENUM ('income', 'expense');
END IF;
END $$;
DO $$ BEGIN IF NOT EXISTS (
    SELECT 1
    FROM pg_type
    WHERE typname = 'research_status_type'
) THEN CREATE TYPE research_status_type AS ENUM (
    'not started',
    'in progress',
    'completed',
    'aborted'
);
END IF;
END $$;
-- DO $$ BEGIN IF NOT EXISTS (
--     SELECT 1
--     FROM pg_type
--     WHERE typname = 'skill_type'
-- ) THEN CREATE TYPE skill_type AS ENUM (
--     'attack',
--     'defend',
--     'heal',
--     'kick',
--     'bash',
--     'dash',
--     'rally'
-- );
-- END IF;
-- END $$;
CREATE TABLE IF NOT EXISTS continent (
    id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY,
    name VARCHAR(50) NOT NULL,
    PRIMARY KEY(id)
);
CREATE TABLE IF NOT EXISTS kingdom (
    id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    rank SMALLINT NOT NULL,
    continent_id SMALLINT REFERENCES continent(id)
);
CREATE TABLE IF NOT EXISTS kingdom_transaction (
    id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    kingdom_id SMALLINT NOT NULL REFERENCES kingdom(id) ON DELETE CASCADE,
    type transaction_type NOT NULL,
    wood SMALLINT NULL,
    food SMALLINT NULL,
    gold SMALLINT NULL,
    stone SMALLINT NULL
);
CREATE TABLE IF NOT EXISTS technology (
    id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(50),
    description VARCHAR(300),
    research_time SMALLINT
);
CREATE TABLE IF NOT EXISTS stat (
    id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    hit_points SMALLINT NULL,
    defense_points SMALLINT NULL,
    damage_points SMALLINT NULL,
    health_points SMALLINT NULL
);
CREATE TABLE IF NOT EXISTS unit (
    id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    kingdom_id SMALLINT REFERENCES kingdom(id),
    stat_id SMALLINT REFERENCES stat(id) ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS price (
    id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    wood SMALLINT NULL,
    food SMALLINT NULL,
    gold SMALLINT NULL,
    stone SMALLINT NULL,
    technology_id SMALLINT REFERENCES technology(id) ON DELETE CASCADE,
    unit_id SMALLINT REFERENCES unit(id) ON DELETE CASCADE,
    CHECK (
        (
            technology_id IS NOT NULL
            AND unit_id IS NULL
        )
        OR (
            technology_id IS NULL
            AND unit_id IS NOT NULL
        )
    )
);
CREATE TABLE IF NOT EXISTS technology_dependency (
    id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    technology_id SMALLINT REFERENCES technology(id),
    is_required BOOLEAN NOT NULL,
    unit_id SMALLINT REFERENCES unit(id)
);
CREATE TABLE IF NOT EXISTS kingdom_technology (
    id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    kingdom_id SMALLINT NOT NULL REFERENCES kingdom(id) ON DELETE CASCADE,
    technology_id SMALLINT NOT NULL REFERENCES technology(id) ON DELETE CASCADE,
    kingdom_transaction_id SMALLINT NOT NULL REFERENCES kingdom_transaction(id) ON DELETE CASCADE,
    research_status research_status_type,
    research_start_time TIMESTAMP
);
CREATE TABLE IF NOT EXISTS kingdom_unit (
    id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    kingdom_id SMALLINT NOT NULL REFERENCES kingdom(id) ON DELETE CASCADE,
    unit_id SMALLINT NOT NULL REFERENCES unit(id) ON DELETE CASCADE,
    kingdom_transaction_id SMALLINT NOT NULL REFERENCES kingdom_transaction(id) ON DELETE CASCADE
);
COMMIT;