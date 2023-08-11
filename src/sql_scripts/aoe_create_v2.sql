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
DO $$ BEGIN IF NOT EXISTS (
    SELECT 1
    FROM pg_type
    WHERE typname = 'skill_type'
) THEN CREATE TYPE skill_type AS ENUM (
    'attack',
    'defend',
    'heal',
    'kick',
    'bash',
    'dash',
    'rally'
);
END IF;
END $$;
CREATE TABLE IF NOT EXISTS continent (
    id INTEGER NOT NULL GENERATED ALWAYS AS IDENTITY,
    name VARCHAR(50) NOT NULL,
    PRIMARY KEY(id)
);
CREATE TABLE IF NOT EXISTS kingdom (
    id INTEGER NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    rank INTEGER NOT NULL,
    continent_id INTEGER REFERENCES continent(id)
);
CREATE TABLE IF NOT EXISTS kingdom_transaction (
    id INTEGER NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    kingdom_id INTEGER NOT NULL REFERENCES kingdom(id) ON DELETE CASCADE,
    type transaction_type NOT NULL,
    wood INTEGER NULL,
    food INTEGER NULL,
    gold INTEGER NULL,
    stone INTEGER NULL
);
CREATE TABLE IF NOT EXISTS technology (
    id INTEGER NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(50),
    description VARCHAR(300),
    research_time INTEGER
);
CREATE TABLE IF NOT EXISTS stat (
    id INTEGER NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    hit_points INTEGER NULL,
    defense_points INTEGER NULL,
    damage_points INTEGER NULL,
    health_points INTEGER NULL
);
CREATE TABLE IF NOT EXISTS unit (
    id INTEGER NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    kingdom_id INTEGER REFERENCES kingdom(id),
    stat_id INTEGER REFERENCES stat(id) ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS skill (
    id INTEGER NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    type skill_type NOT NULL,
    stat_id INTEGER REFERENCES stat(id) ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS equipment (
    id INTEGER NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    stat_id INTEGER REFERENCES stat(id) ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS price (
    id INTEGER NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    wood INTEGER NULL,
    food INTEGER NULL,
    gold INTEGER NULL,
    stone INTEGER NULL,
    technology_id INTEGER REFERENCES technology(id) ON DELETE CASCADE,
    unit_id INTEGER REFERENCES unit(id) ON DELETE CASCADE,
    skill_id INTEGER REFERENCES skill(id) ON DELETE CASCADE,
    equipment_id INTEGER REFERENCES equipment(id) ON DELETE CASCADE,
    CHECK (
        (
            technology_id IS NOT NULL
            AND unit_id IS NULL
            AND skill_id IS NULL
            AND equipment_id IS NULL
        )
        OR (
            technology_id IS NULL
            AND unit_id IS NOT NULL
            AND skill_id IS NULL
            AND equipment_id IS NULL
        )
        OR (
            technology_id IS NULL
            AND unit_id IS NULL
            AND skill_id IS NOT NULL
            AND equipment_id IS NULL
        )
        OR (
            technology_id IS NULL
            AND unit_id IS NULL
            AND skill_id IS NULL
            AND equipment_id IS NOT NULL
        )
    )
);
CREATE TABLE IF NOT EXISTS unit_skill (
    id INTEGER NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    skill_id INTEGER NOT NULL REFERENCES skill(id) ON DELETE CASCADE,
    unit_id INTEGER NOT NULL REFERENCES unit(id) ON DELETE CASCADE,
    kingdom_transaction_id INTEGER NOT NULL REFERENCES kingdom_transaction(id) ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS unit_equipment (
    id INTEGER NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    equipment_id INTEGER NOT NULL REFERENCES equipment(id) ON DELETE CASCADE,
    unit_id INTEGER NOT NULL REFERENCES unit(id) ON DELETE CASCADE,
    kingdom_transaction_id INTEGER NOT NULL REFERENCES kingdom_transaction(id) ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS technology_dependency (
    id INTEGER NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    technology_id INTEGER REFERENCES technology(id),
    is_required BOOLEAN NOT NULL,
    unit_id INTEGER REFERENCES unit(id),
    skill_id INTEGER REFERENCES skill(id),
    equipment_id INTEGER REFERENCES equipment(id),
    CHECK (
        (
            unit_id IS NOT NULL
            AND skill_id IS NULL
            AND equipment_id IS NULL
        )
        OR (
            unit_id IS NULL
            AND skill_id IS NOT NULL
            AND equipment_id IS NULL
        )
        OR (
            unit_id IS NULL
            AND skill_id IS NULL
            AND equipment_id IS NOT NULL
        )
    )
);
CREATE TABLE IF NOT EXISTS kingdom_technology (
    id INTEGER NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    kingdom_id INTEGER NOT NULL REFERENCES kingdom(id) ON DELETE CASCADE,
    technology_id INTEGER NOT NULL REFERENCES technology(id) ON DELETE CASCADE,
    kingdom_transaction_id INTEGER NOT NULL REFERENCES kingdom_transaction(id) ON DELETE CASCADE,
    research_status research_status_type,
    research_start_time TIMESTAMP
);
CREATE TABLE IF NOT EXISTS kingdom_unit (
    id INTEGER NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    kingdom_id INTEGER NOT NULL REFERENCES kingdom(id) ON DELETE CASCADE,
    unit_id INTEGER NOT NULL REFERENCES unit(id) ON DELETE CASCADE,
    kingdom_transaction_id INTEGER NOT NULL REFERENCES kingdom_transaction(id) ON DELETE CASCADE
);
COMMIT;
CREATE OR REPLACE FUNCTION get_kingdom_technologies(kingdomidentifier INTEGER)
RETURNS TABLE (name VARCHAR, technology_name VARCHAR, research_status research_status_type) AS $$
BEGIN
  RETURN QUERY 
  SELECT k.name AS name, t.name AS technology_name, kt.research_status AS research_status
  FROM kingdom k
  JOIN kingdom_technology kt ON k.id = kt.kingdom_id
  JOIN technology t ON kt.technology_id = t.id
  WHERE k.id = kingdomidentifier;
END;
$$ LANGUAGE plpgsql;
