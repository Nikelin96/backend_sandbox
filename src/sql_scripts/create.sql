SET default_tablespace = pg_default;

-- CREATE TABLE IF NOT EXISTS arena
-- (
--     id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY,
--     name VARCHAR(50) NOT NULL,
--   	PRIMARY KEY(id)
-- );

-- CREATE TABLE IF NOT EXISTS squad
-- (
--     id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
--     name VARCHAR(50) NOT NULL,
-- 	rank SMALLINT NOT NULL,
-- 	arena_id SMALLINT REFERENCES arena(id)
-- );
CREATE TABLE IF NOT EXISTS stat
(
	id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	hit_points SMALLINT NULL,
	defense_points SMALLINT NULL,
	damage_points SMALLINT NULL,
	health_points SMALLINT NULL
);

DROP TYPE IF EXISTS transaction_type;

CREATE TYPE transaction_type AS ENUM ('income', 'expense');

CREATE TABLE IF NOT EXISTS storage
(
	id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	--type transaction_type NOT NULL,
	wood SMALLINT NULL,
	food SMALLINT NULL,
	gold SMALLINT NULL,
	stone SMALLINT NULL
);

CREATE TABLE IF NOT EXISTS unit
(
	id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	name VARCHAR(50) NOT NULL,
	-- age SMALLINT NOT NULL,
	-- power SMALLINT NOT NULL,
	-- hit_points SMALLINT NOT NULL,
	-- squad_id SMALLINT REFERENCES squad(id),
	stat_id SMALLINT REFERENCES stat(id),
	storage_id SMALLINT REFERENCES storage(id)	
);

CREATE TABLE IF NOT EXISTS market
(
	unit_id SMALLINT REFERENCES unit(id),
	tax SMALLINT NOT NULL
);

CREATE TABLE IF NOT EXISTS university
(
	unit_id SMALLINT REFERENCES unit(id),
	research_time SMALLINT NOT NULL
);


CREATE TABLE IF NOT EXISTS equipment
(
    id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
	-- type VARCHAR(50) NOT NULL,
	unit_id SMALLINT REFERENCES unit(id),
	stat_id SMALLINT REFERENCES stat(id),
	storage_id SMALLINT REFERENCES storage(id)	
);

CREATE TABLE IF NOT EXISTS skill
(
    id SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
	type VARCHAR(50) NOT NULL,
	-- cost SMALLINT NOT NULL,
	unit_id SMALLINT REFERENCES unit(id),
	stat_id SMALLINT REFERENCES stat(id),
	storage_id SMALLINT REFERENCES storage(id)
);