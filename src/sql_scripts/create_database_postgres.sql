SET default_tablespace = pg_default;

CREATE TABLE IF NOT EXISTS arena
(
    id smallint NOT NULL GENERATED ALWAYS AS IDENTITY,
    name varchar(50) NOT NULL,
	PRIMARY KEY(id)
)

CREATE TABLE IF NOT EXISTS squad
(
    id smallint NOT NULL GENERATED ALWAYS AS IDENTITY,
    name varchar(50) NOT NULL,
	rank smallint NOT NULL,
	arena_id smallint,
	PRIMARY KEY(id),
	CONSTRAINT fk_squad_arena
		FOREIGN KEY(arena_id)
			REFERENCES arena(id)
)

CREATE TABLE IF NOT EXISTS hero
(
    id smallint NOT NULL GENERATED ALWAYS AS IDENTITY,
    name varchar(50) NOT NULL,
	age smallint NOT NULL,
	power smallint NOT NULL,
	hit_points smallint NOT NULL,
	squad_id smallint,
	PRIMARY KEY(id),
		CONSTRAINT fk_hero_squad
			FOREIGN KEY(squad_id)
				REFERENCES squad(id)
)

CREATE TABLE IF NOT EXISTS equipment
(
    id smallint NOT NULL GENERATED ALWAYS AS IDENTITY,
    name varchar(50) NOT NULL,
	type varchar(50) NOT NULL,
	defense_points smallint NOT NULL,
	damage_points smallint NOT NULL,
	hit_points smallint NOT NULL,
	hero_id smallint,
	PRIMARY KEY(id),
		CONSTRAINT fk_equipment_hero
			FOREIGN KEY(hero_id)
				REFERENCES hero(id)
)

CREATE TABLE IF NOT EXISTS skill
(
    id smallint NOT NULL GENERATED ALWAYS AS IDENTITY,
    name varchar(50) NOT NULL,
	type varchar(50) NOT NULL,
	defense_points smallint NOT NULL,
	damage_points smallint NOT NULL,
	hit_points smallint NOT NULL,
	healing_points smallint NOT NULL,
	cost smallint NOT NULL,
	hero_id smallint,
	PRIMARY KEY(id),
		CONSTRAINT fk_skill_hero
			FOREIGN KEY(hero_id)
				REFERENCES hero(id)
)