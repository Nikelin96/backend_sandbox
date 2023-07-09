BEGIN;
SET default_tablespace = pg_default;
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
	WHERE typname = 'transaction_type'
) THEN CREATE TYPE transaction_type AS ENUM ('price', 'income', 'expense');
END IF;
END $$;
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
CREATE TABLE IF NOT EXISTS stat (
	id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	hit_points SMALLINT NULL,
	defense_points SMALLINT NULL,
	damage_points SMALLINT NULL,
	health_points SMALLINT NULL
);
CREATE TABLE IF NOT EXISTS storage (
	id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	type transaction_type NOT NULL,
	wood SMALLINT NULL,
	food SMALLINT NULL,
	gold SMALLINT NULL,
	stone SMALLINT NULL
);
CREATE TABLE blueprint (
	id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	name VARCHAR(50),
	description VARCHAR(300),
	research_time SMALLINT,
	storage_id SMALLINT REFERENCES storage(id)
);
CREATE TABLE blueprint_dependency (
	id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	blueprint_id SMALLINT REFERENCES blueprint(id),
	required_blueprint_id SMALLINT REFERENCES blueprint(id),
	is_optional BOOLEAN NOT NULL
);
CREATE TABLE IF NOT EXISTS unit (
	id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	name VARCHAR(50) NOT NULL,
	kingdom_id SMALLINT REFERENCES kingdom(id),
	stat_id SMALLINT REFERENCES stat(id) ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS market (
	unit_id SMALLINT UNIQUE REFERENCES unit(id) ON DELETE CASCADE,
	tax SMALLINT NOT NULL
);
CREATE TABLE IF NOT EXISTS university (
	unit_id SMALLINT UNIQUE REFERENCES unit(id) ON DELETE CASCADE,
	research_time SMALLINT NOT NULL
);
CREATE TABLE IF NOT EXISTS equipment (
	id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	name VARCHAR(50) NOT NULL,
	unit_id SMALLINT REFERENCES unit(id) ON DELETE CASCADE,
	stat_id SMALLINT REFERENCES stat(id) ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS skill (
	id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	name VARCHAR(50) NOT NULL,
	type skill_type NOT NULL,
	unit_id SMALLINT REFERENCES unit(id) ON DELETE CASCADE,
	stat_id SMALLINT REFERENCES stat(id) ON DELETE CASCADE
);
-- Junction tables
CREATE TABLE IF NOT EXISTS unit_storage_transactions (
	unit_id SMALLINT NOT NULL REFERENCES unit(id) ON DELETE CASCADE,
	storage_id SMALLINT NOT NULL REFERENCES storage(id) ON DELETE CASCADE,
	PRIMARY KEY (unit_id, storage_id)
);
CREATE TABLE IF NOT EXISTS kingdom_storage_transactions (
	kingdom_id SMALLINT NOT NULL REFERENCES kingdom(id) ON DELETE CASCADE,
	storage_id SMALLINT NOT NULL REFERENCES storage(id) ON DELETE CASCADE,
	PRIMARY KEY (kingdom_id, storage_id)
);
CREATE TABLE kingdom_blueprint (
	kingdom_id SMALLINT REFERENCES kingdom(id),
	storage_id SMALLINT REFERENCES storage(id),
	blueprint_id SMALLINT REFERENCES blueprint(id),
	FOREIGN KEY (kingdom_id, storage_id) REFERENCES kingdom_storage_transactions(kingdom_id, storage_id),
	research_status research_status_type,
	research_start_time TIMESTAMP,
	PRIMARY KEY (kingdom_id, blueprint_id)
);
COMMIT;